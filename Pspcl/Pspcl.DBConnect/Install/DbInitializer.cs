using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Pspcl.Core.Domain;

namespace Pspcl.DBConnect.Install

{
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
                await CreateDefaultMaterialGroup();
                await CreateDefaultMaterialType();

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

        public async Task CreateDefaultMaterialGroup()
        {
            try
            {
                if (!_identityContext.MaterialGroup.Any())
                {
                    var MaterialGroupData = new List<MaterialGroup>()
                    {
                        new MaterialGroup() {Name="SP SMART METER",IsActive=true,IsDeleted=false },
                        new MaterialGroup() {Name="SP NET METER",IsActive=true,IsDeleted=false},
                        new MaterialGroup() {Name="SP METER",IsActive=true,IsDeleted=false},
                        new MaterialGroup() {Name="SEAL",IsActive=true,IsDeleted=false},
                        new MaterialGroup() {Name="PPU",IsActive=true,IsDeleted=false},
                        new MaterialGroup() {Name="PP SMART METER",IsActive=true,IsDeleted=false},
                        new MaterialGroup() {Name="PP NET METER",IsActive=true,IsDeleted=false},
                        new MaterialGroup() {Name="PP METER",IsActive=true,IsDeleted=false},
                        new MaterialGroup() {Name="METER TESTER",IsActive=true,IsDeleted=false},
                        new MaterialGroup() {Name="MCB",IsActive =true,IsDeleted=false},
                        new MaterialGroup() {Name="LTCT(DT) SET",IsActive=true,IsDeleted=false},
                        new MaterialGroup() {Name="LTCT SET",IsActive=true,IsDeleted=false},
                        new MaterialGroup() {Name="LTCT METER",IsActive=true,IsDeleted=false},
                        new MaterialGroup() {Name="LTCT (DT) METER",IsActive=true,IsDeleted=false},
                        new MaterialGroup() {Name="HT METER",IsActive=true,IsDeleted=false},
                        new MaterialGroup() {Name="GAUSS METER",IsActive=true,IsDeleted=false},
                        new MaterialGroup() {Name="ELECTRONIC TEST BENCH",IsActive=true,IsDeleted=false},
                        new MaterialGroup() {Name="DT METER",IsActive=true,IsDeleted=false},
                        new MaterialGroup() {Name="DT BOX",IsActive=true,IsDeleted=false},
                        new MaterialGroup() {Name="CTPT UNIT",IsActive=true,IsDeleted=false},
                        new MaterialGroup() {Name="CMRI",IsActive=true,IsDeleted=false},
                        new MaterialGroup() {Name="ALUMINIUM ACAB CABLE",IsActive=true,IsDeleted=false},
                    };

                    _identityContext.MaterialGroup.AddRange(MaterialGroupData);
                    await _identityContext.SaveChangesAsync();
                    _logger.LogInformation("Material Group data inserted : {@MaterialGroupData}", MaterialGroupData);

                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception");
            }

        }
        public async Task CreateDefaultMaterialType()
        {
            try
            {
                if (!_identityContext.MaterialType.Any())
                {
                    var MaterialTypeData = new List<MaterialType>()
                    {
                        new MaterialType() {Name="SP SMART METER (10-60)",IsActive=true,IsDeleted=false,Rating="10-60"},
                        new MaterialType() {Name="SP BI-DIR METERS",IsActive=true,IsDeleted=false,Rating="10-60"},
                        new MaterialType() {Name="SP METER (10-60)",IsActive=true,IsDeleted=false,Rating="10-60"},
                        new MaterialType() {Name="PLASTIC SEAL",IsActive=true,IsDeleted=false, Rating = "10-60"},
                        new MaterialType() {Name="POWER PACK UNIT WITH BATTERY",IsActive=true,IsDeleted=false, Rating = "10-60"},
                        new MaterialType() {Name="SMART METERS (P/P)",IsActive=true,IsDeleted=false, Rating = "3* 10-60"},
                        new MaterialType() {Name="PP BI-DIR METER (3*10-60)",IsActive=true,IsDeleted=false, Rating = "3* 10-60"},
                        new MaterialType() {Name="PP METER (3*10-60)",IsActive=true,IsDeleted=false,Rating="3* 10-60"},
                        new MaterialType() {Name="COMPACT STATICS ON LINE METER TESTING EQUIPMENT (SECURE) FOR S/P METERS",IsActive =true,IsDeleted=false},
                        new MaterialType() {Name="SP MCB",IsActive=true,IsDeleted=false},
                        new MaterialType() {Name="PP MCB",IsActive=true,IsDeleted=false},
                        new MaterialType() {Name="MCB 20 IN 1",IsActive=true,IsDeleted=false},
                        new MaterialType() {Name="MCB 6 IN 1",IsActive=true,IsDeleted=false},
                        new MaterialType() {Name="MCB (4 IN 1)",IsActive=true,IsDeleted=false},
                        new MaterialType() {Name="LTCT MCB",IsActive=true,IsDeleted=false},
                        new MaterialType() {Name="MCB (IPDS)",IsActive=true,IsDeleted=false},
                        new MaterialType() {Name="LTCT (DT) SET 100/5",IsActive=true,IsDeleted=false,Rating="100/5"},
                        new MaterialType() {Name="LTCT (DT) SET 200/5",IsActive=true,IsDeleted=false,Rating="200/5"},
                        new MaterialType() {Name="LTCT (DT) SET 400/5",IsActive=true,IsDeleted=false,Rating="400/5"},
                        new MaterialType() {Name="LTCT SET (100/5)",IsActive=true,IsDeleted=false,Rating="100/5"},
                        new MaterialType() {Name="LTCT SET (200/5)",IsActive=true,IsDeleted=false,Rating="200/5"},
                        new MaterialType() {Name="LTCT SET (400/5)",IsActive=true,IsDeleted=false,Rating="400/5"},
                        new MaterialType() {Name="LTCT METER (100/5)",IsActive=true,IsDeleted=false,Rating="100/5"},
                        new MaterialType() {Name="LTCT METER (200/5)",IsActive = true,IsDeleted=false,Rating="200/5"},
                        new MaterialType() {Name="LT IN BUILT METER (40-200 A)",IsActive=true,IsDeleted = false,Rating="40-200"},
                        new MaterialType() {Name="LTCT (DT) METERS -/5",IsActive=true,IsDeleted = false,Rating="-5"},
                        new MaterialType() {Name="HT METER (-/5)",IsActive=true,IsDeleted = false,Rating="-5"},
                        new MaterialType() {Name="HT METER (S/STN.)",IsActive=true,IsDeleted = false,Rating="-5"},
                        new MaterialType() {Name="DIGITAL GAUSS METER",IsActive=true,IsDeleted = false},
                        new MaterialType() {Name="ELECTRONIC TESTING SET & BENCH",IsActive=true,IsDeleted = false},
                        new MaterialType() {Name="DT METER 800/5",IsActive=true,IsDeleted = false,Rating="800/5"},
                        new MaterialType() {Name="DT SET 800/5",IsActive=true,IsDeleted = false,Rating="800/5"},
                        new MaterialType() {Name="DT BOX",IsActive=true,IsDeleted = false},
                        new MaterialType() {Name="CTPT UNIT 10/5",IsActive=true,IsDeleted = false,Rating="10/5"},
                        new MaterialType() {Name="CTPT UNIT 20/5",IsActive=true,IsDeleted = false,Rating="20/5"},
                        new MaterialType() {Name="CTPT UNIT 30/5",IsActive=true,IsDeleted = false,Rating="30/5"},
                        new MaterialType() {Name="CTPT UNIT 50/5",IsActive=true,IsDeleted = false,Rating="50/5"},
                        new MaterialType() {Name="CTPT UNIT 75/5",IsActive=true,IsDeleted = false,Rating="75/5"},
                        new MaterialType() {Name="CTPT UNIT 100/5",IsActive=true,IsDeleted = false,Rating="100/5"},
                        new MaterialType() {Name="CTPT UNIT 150/5",IsActive=true,IsDeleted = false,Rating="150/5"},
                        new MaterialType() {Name="CTPT UNIT 200/5",IsActive=true,IsDeleted = false,Rating="200/5"},
                        new MaterialType() {Name="CTPT UNIT 300/5",IsActive=true,IsDeleted = false,Rating="300/5"},
                        new MaterialType() {Name="CTPT UNIT 400/5",IsActive=true,IsDeleted = false,Rating="400/5"},
                        new MaterialType() {Name="CMRI (DMRI)",IsActive=true,IsDeleted = false},
                        new MaterialType() {Name="ALUMINIUM CABLE",IsActive=true,IsDeleted = false},


                    };

                    _identityContext.MaterialType.AddRange(MaterialTypeData);
                    await _identityContext.SaveChangesAsync();
                    _logger.LogInformation("Material Type data inserted : {@MaterialTypeData}", MaterialTypeData);

                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception");
            }
        }
    }
}
