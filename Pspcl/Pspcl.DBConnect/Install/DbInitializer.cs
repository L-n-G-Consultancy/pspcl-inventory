using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Pspcl.Core.Domain;

namespace Pspcl.DBConnect.Install

{
    /// <summary>
    /// Install
    /// </summary>
    public class DbInitializer : IDbInitializer
    {
        private readonly RoleManager<Role> _roleManager;
        private readonly UserManager<User> _userManager;
        private readonly ILogger<DbInitializer> _logger;
        private readonly ApplicationDbContext _identityContext;
        private readonly IServiceScopeFactory _scopeFactory;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="roleManager"></param>
        /// <param name="userManager"></param>
        /// <param name="logger"></param>
        /// <param name="identityContext"></param>
        /// <param name="scopeFactory"></param>
        public DbInitializer(RoleManager<Role> roleManager, UserManager<User> userManager,
            ILogger<DbInitializer> logger, ApplicationDbContext identityContext, IServiceScopeFactory scopeFactory)
        {
            _roleManager = roleManager;
            _logger = logger;
            _identityContext = identityContext;
            _scopeFactory = scopeFactory;
            _userManager = userManager;
        }

        /// <summary>
        /// 
        /// </summary>
        public async Task Initialize()
        {
            //add admin user
            var checkDefaultDataInstalled = await _userManager.FindByEmailAsync("admin@gmail.com");
            if (checkDefaultDataInstalled == null)
            {
                await CreateRoles();
                await CreateDefaultAdminUser();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public async Task CreateRoles()
        {
            var roles = new List<string> { "InventoryManager" };
            _logger.LogInformation("Roles List {@roles}", roles);
            foreach (var role in roles)
            {
                try
                {
                    var roleExits = await _roleManager.RoleExistsAsync(role);
                    _logger.LogInformation("Roles Existence {@roles}", roleExits);

                    if (!roleExits)
                        await _roleManager.CreateAsync(new Role { Name = role });
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Exception");
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<User> CreateDefaultAdminUser()
        {
            try
            {
                var defaultAdmin = new User()
                {
                    Id = 0,
                    UserName = "admin@gmail.com",
                    Email = "admin@gmail.com",
                    LockoutEnabled = false,
                    PhoneNumber = "1234567890",
                    IsActive = true,
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    AccessFailedCount = 0,
                    IsDeleted = false,

                };
                var userCreateResponse = await _userManager.CreateAsync(defaultAdmin, "admin");
                _logger.LogInformation("Super Admin Created : {@defaultAdmin}", defaultAdmin);

                // Assign Default User Role
                //await _userManager.AddToRoleAsync(defaultAdmin, "InventoryManager");
                //_logger.LogInformation("Role Assigned : {@defaultAdmin}", "InventoryManager");
                return defaultAdmin;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception");
                return null;
            }
        }
    }
}
