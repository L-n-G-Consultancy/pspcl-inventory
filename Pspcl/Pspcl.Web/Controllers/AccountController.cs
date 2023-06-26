﻿using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Pspcl.Core.Domain;
using Pspcl.Services.Models;


namespace Pspcl.Web.Controllers
{

    public class AccountController : Controller
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly ILogger<AccountController> _logger;
        private readonly IMapper _mapper;


        public AccountController(SignInManager<User> signInManager, ILogger<AccountController> logger, UserManager<User> userManager, IMapper mapper)            
        {
            _signInManager = signInManager;
            _logger = logger;
            _userManager = userManager;
            _mapper = mapper;
            
        }

        [HttpGet]
        public async Task<IActionResult> Login(string returnUrl = null)
        {
            var model = new Pspcl.ViewModels.LoginModel();
            model.ReturnUrl = returnUrl ?? Url.Content("~/");
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);
            //model.ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Login(Pspcl.ViewModels.LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.UserName);
                if (user == null)
                {
                    ModelState.AddModelError(nameof(model.UserName), "User does not exists!");
                    return View(model);
                }

                var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, false, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    // Store Login Date
                    //user.LastLoginDate = DateTime.UtcNow;
                    await _userManager.UpdateAsync(user);

                    _logger.LogInformation($"User '{user.Email}' logged in.");
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return View(model);
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }

        [HttpGet]
        [Authorize(Roles = "SuperAdmin,InventoryManager")]
        public IActionResult AddUser(AddUserModel user)
        {
            
            return View(user);
        }


        [HttpPost]
        [Authorize(Roles = "SuperAdmin,InventoryManager")]
        public async Task<IActionResult> AddUser(AddUserModel user, string choosenUserRole)
        {
            if (choosenUserRole == "Super-Admin") { choosenUserRole = "SuperAdmin"; }
            else if (choosenUserRole== "Inventory-Manager") { choosenUserRole = "InventoryManager"; }
            

            if (ModelState.IsValid)
            {
                // Check that password and confirmPassword match
                if (user.Password != user.ConfirmPassword)
                {
                    ModelState.AddModelError("", "The password and confirm password fields do not match.");
                    ViewBag.PasswordMismatchError = "The password and confirm password fields do not match.";
                    if (choosenUserRole == "SuperAdmin") { choosenUserRole = "Super-Admin"; }
                    else if (choosenUserRole == "InventoryManager") { choosenUserRole = "Inventory-Manager"; }
                    TempData["choosenRole"] = choosenUserRole;
                    return View(user);
                }




                // Create the new user object
                var newUser = new AddUserModel
                {
                    UserName = user.Email,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    IsActive = true,
                    EmailConfirmed = true,
                    AccessFailedCount = 0,
                    IsDeleted = false,
                    CreatedOn = DateTime.Now,
                    ModifiedOn = DateTime.Now,
                    LastLoginTime = DateTime.Now
                };
                 var entityUser = _mapper.Map<User>(newUser);

                // Add the user to the database
                var result = await _userManager.CreateAsync(entityUser, user.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(entityUser, choosenUserRole);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }

            return View(user);
        }


    }
}
