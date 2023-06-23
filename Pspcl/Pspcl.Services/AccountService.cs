using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Pspcl.Core.Domain;
using Pspcl.DBConnect;
using Pspcl.DBConnect.Install;
using Pspcl.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pspcl.Services
{
    public class AccountService:IAccountService
    {
        private readonly ApplicationDbContext _dbcontext;
        private readonly UserManager<User> _userManager;
        private readonly ILogger<DbInitializer> _logger;

        public AccountService(ApplicationDbContext dbContext, UserManager<User> userManager, ILogger<DbInitializer> logger)
        {
            _dbcontext = dbContext;
            _userManager = userManager;
            _logger = logger;
        }
        public async Task AssignAdminRole(User newUser, string assigningRole)
        {
            try
            {
                await _userManager.AddToRoleAsync(newUser, assigningRole);
                _logger.LogInformation("Role Assigned : {@newUser}", assigningRole);
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Some error occured",ex);
            }
            
           
        }
    }
}
