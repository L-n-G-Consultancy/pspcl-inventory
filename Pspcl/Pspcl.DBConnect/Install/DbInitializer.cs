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
                await CreateDefaultMaterial();
                await CreateDefaultCircle();
                await CreateDefaultDivision();
                await CreateDefaultSubDivision();

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
                        new MaterialGroup() {Name="SP SMART METER",IsActive=true,IsDeleted=false},
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
                        new MaterialType() {Name="SP SMART METER (10-60)",IsActive=true,IsDeleted=false,Rating="10-60",MaterialGroupId=1},
                        new MaterialType() {Name="SP BI-DIR METERS",IsActive=true,IsDeleted=false,Rating="10-60",MaterialGroupId=2},
                        new MaterialType() {Name="SP METER (10-60)",IsActive=true,IsDeleted=false,Rating="10-60",MaterialGroupId=3},
                        new MaterialType() {Name="PLASTIC SEAL",IsActive=true,IsDeleted=false, Rating = "10-60",MaterialGroupId=4},
                        new MaterialType() {Name="POWER PACK UNIT WITH BATTERY",IsActive=true,IsDeleted=false, Rating = "10-60",MaterialGroupId=5},
                        new MaterialType() {Name="SMART METERS (P/P)",IsActive=true,IsDeleted=false, Rating = "3* 10-60",MaterialGroupId=6},
                        new MaterialType() {Name="PP BI-DIR METER (3*10-60)",IsActive=true,IsDeleted=false, Rating = "3* 10-60",MaterialGroupId=7},
                        new MaterialType() {Name="PP METER (3*10-60)",IsActive=true,IsDeleted=false,Rating="3* 10-60",MaterialGroupId=8},
                        new MaterialType() {Name="COMPACT STATICS ON LINE METER TESTING EQUIPMENT (SECURE) FOR S/P METERS",IsActive =true,IsDeleted=false,MaterialGroupId=9},
                        new MaterialType() {Name="SP MCB",IsActive=true,IsDeleted=false,MaterialGroupId=10},
                        new MaterialType() {Name="PP MCB",IsActive=true,IsDeleted=false,MaterialGroupId=10},
                        new MaterialType() {Name="MCB 20 IN 1",IsActive=true,IsDeleted=false,MaterialGroupId=10},
                        new MaterialType() {Name="MCB 6 IN 1",IsActive=true,IsDeleted=false,MaterialGroupId=10},
                        new MaterialType() {Name="MCB (4 IN 1)",IsActive=true,IsDeleted=false,MaterialGroupId=10},
                        new MaterialType() {Name="LTCT MCB",IsActive=true,IsDeleted=false,MaterialGroupId=10},
                        new MaterialType() {Name="MCB (IPDS)",IsActive=true,IsDeleted=false,MaterialGroupId=10},
                        new MaterialType() {Name="LTCT (DT) SET 100/5",IsActive=true,IsDeleted=false,Rating="100/5",MaterialGroupId=11},
                        new MaterialType() {Name="LTCT (DT) SET 200/5",IsActive=true,IsDeleted=false,Rating="200/5",MaterialGroupId=11},
                        new MaterialType() {Name="LTCT (DT) SET 400/5",IsActive=true,IsDeleted=false,Rating="400/5",MaterialGroupId=11},
                        new MaterialType() {Name="LTCT SET (100/5)",IsActive=true,IsDeleted=false,Rating="100/5",MaterialGroupId=12},
                        new MaterialType() {Name="LTCT SET (200/5)",IsActive=true,IsDeleted=false,Rating="200/5",MaterialGroupId=12},
                        new MaterialType() {Name="LTCT SET (400/5)",IsActive=true,IsDeleted=false,Rating="400/5",MaterialGroupId=12},
                        new MaterialType() {Name="LTCT METER (100/5)",IsActive=true,IsDeleted=false,Rating="100/5",MaterialGroupId=13},
                        new MaterialType() {Name="LTCT METER (200/5)",IsActive = true,IsDeleted=false,Rating="200/5",MaterialGroupId=13},
                        new MaterialType() {Name="LT IN BUILT METER (40-200 A)",IsActive=true,IsDeleted = false,Rating="40-200",MaterialGroupId=13},
                        new MaterialType() {Name="LTCT (DT) METERS -/5",IsActive=true,IsDeleted = false,Rating="-5",MaterialGroupId=14},
                        new MaterialType() {Name="HT METER (-/5)",IsActive=true,IsDeleted = false,Rating="-5",MaterialGroupId=15},
                        new MaterialType() {Name="HT METER (S/STN.)",IsActive=true,IsDeleted = false,Rating="-5",MaterialGroupId=15},
                        new MaterialType() {Name="DIGITAL GAUSS METER",IsActive=true,IsDeleted = false,MaterialGroupId=16},
                        new MaterialType() {Name="ELECTRONIC TESTING SET & BENCH",IsActive=true,IsDeleted = false,MaterialGroupId=17},
                        new MaterialType() {Name="DT METER 800/5",IsActive=true,IsDeleted = false,Rating="800/5",MaterialGroupId=18},
                        new MaterialType() {Name="DT SET 800/5",IsActive=true,IsDeleted = false,Rating="800/5",MaterialGroupId=18},
                        new MaterialType() {Name="DT BOX",IsActive=true,IsDeleted = false,MaterialGroupId=19},
                        new MaterialType() {Name="CTPT UNIT 10/5",IsActive=true,IsDeleted = false,Rating="10/5",MaterialGroupId=20},
                        new MaterialType() {Name="CTPT UNIT 20/5",IsActive=true,IsDeleted = false,Rating="20/5",MaterialGroupId=20},
                        new MaterialType() {Name="CTPT UNIT 30/5",IsActive=true,IsDeleted = false,Rating="30/5",MaterialGroupId=20},
                        new MaterialType() {Name="CTPT UNIT 50/5",IsActive=true,IsDeleted = false,Rating="50/5",MaterialGroupId=20},
                        new MaterialType() {Name="CTPT UNIT 75/5",IsActive=true,IsDeleted = false,Rating="75/5",MaterialGroupId=20},
                        new MaterialType() {Name="CTPT UNIT 100/5",IsActive=true,IsDeleted = false,Rating="100/5",MaterialGroupId=20},
                        new MaterialType() {Name="CTPT UNIT 150/5",IsActive=true,IsDeleted = false,Rating="150/5",MaterialGroupId=20},
                        new MaterialType() {Name="CTPT UNIT 200/5",IsActive=true,IsDeleted = false,Rating="200/5",MaterialGroupId=20},
                        new MaterialType() {Name="CTPT UNIT 300/5",IsActive=true,IsDeleted = false,Rating="300/5",MaterialGroupId=20},
                        new MaterialType() {Name="CTPT UNIT 400/5",IsActive=true,IsDeleted = false,Rating="400/5",MaterialGroupId=20},
                        new MaterialType() {Name="CMRI (DMRI)",IsActive=true,IsDeleted = false,MaterialGroupId=21},
                        new MaterialType() {Name="ALUMINIUM CABLE",IsActive=true,IsDeleted = false,MaterialGroupId=22},


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
        public async Task CreateDefaultMaterial()
        {
            try
            {
                var MaterialData = new List<Material>()
                {
                    new Material() {Name="SP SMART METER (10-60)",Code="SPSM/M-185",MaterialTypeId=1,IsActive=true,IsDeleted=false},
                    new Material() {Name="SP SMART METER (10-60)",Code="SPSM/M-190",MaterialTypeId=1,IsActive=true,IsDeleted=false},

                    new Material() {Name="SP BI-DIR METERS",Code="SPN/M159",MaterialTypeId=2,IsActive=true,IsDeleted=false},
                    new Material() {Name="SP BI-DIR METERS",Code="SPN/M160",MaterialTypeId=2,IsActive=true,IsDeleted=false},

                    new Material() {Name="SP METER (10-60)",Code="SPL/M139",MaterialTypeId=3,IsActive=true,IsDeleted=false},
                    new Material() {Name="SP METER (10-60)",Code="SPL/M173",MaterialTypeId=3,IsActive=true,IsDeleted=false},
                    new Material() {Name="SP METER (10-60)",Code="SPG/M172",MaterialTypeId=3,IsActive=true,IsDeleted=false},
                    new Material() {Name="SP METER (10-60)",Code="SPL/M171",MaterialTypeId=3,IsActive=true,IsDeleted=false},

                    new Material() {Name="PLASTIC SEAL",Code="PSPFT",MaterialTypeId=4,IsActive=true,IsDeleted=false},

                    new Material() {Name="POWER PACK UNIT WITH BATTERY",MaterialTypeId=5,IsActive=true,IsDeleted=false},

                    new Material() {Name="SMART METERS (P/P)",Code="PPSM/M148",MaterialTypeId=6,IsActive=true,IsDeleted=false},

                    new Material() {Name="PP BI-DIR METER (3*10-60)",Code="PPN/M151",MaterialTypeId=7,IsActive=true,IsDeleted=false},
                    new Material() {Name="PP BI-DIR METER (3*10-60)",Code="PPN/M177",MaterialTypeId=7,IsActive=true,IsDeleted=false},

                    new Material() {Name="PP METER (3*10-60)",Code="PPF/M153",MaterialTypeId=8,IsActive=true,IsDeleted=false},
                    new Material() {Name="PP METER (3*10-60)",Code="PPG/M135",MaterialTypeId=8,IsActive=true,IsDeleted=false},

                    new Material() {Name="COMPACT STATICS ON LINE METER TESTING EQUIPMENT (SECURE) FOR S/P METERS",Code="E 6500",MaterialTypeId=9 ,IsActive=true,IsDeleted=false},

                    new Material() {Name="SP MCB",Code="SPB",MaterialTypeId=10,IsActive=true,IsDeleted=false},

                    new Material() {Name="PP MCB",Code="PPB/M169",MaterialTypeId=11,IsActive=true,IsDeleted=false},

                    new Material() {Name="MCB 20 IN 1",Code="MPB20",MaterialTypeId=12,IsActive=true,IsDeleted=false},

                    new Material() {Name="MCB 6 IN 1",Code="MPB6",MaterialTypeId=13,IsActive=true,IsDeleted=false},

                    new Material() {Name="MCB (4 IN 1)",Code="MPB4",MaterialTypeId=14,IsActive=true,IsDeleted=false},

                    new Material() {Name="LTCT MCB",Code="LTB",MaterialTypeId=15,IsActive=true,IsDeleted=false},

                    new Material() {Name="MCB (IPDS)",Code="LTB/M136",MaterialTypeId=16,IsActive=true,IsDeleted=false},

                    new Material() {Name="LTCT (DT) SET 100/5",Code="DTS100/M133",MaterialTypeId=17,IsActive=true,IsDeleted=false },
                    new Material() {Name="LTCT (DT) SET 200/5",Code="DTS200/M133",MaterialTypeId=18,IsActive=true,IsDeleted=false },
                    new Material() {Name="LTCT (DT) SET 400/5",Code="DTS400/M133",MaterialTypeId=19,IsActive=true,IsDeleted=false },

                    new Material() {Name="LTCT SET (100/5)",Code="LTS100",MaterialTypeId=20,IsActive=true,IsDeleted=false},
                    new Material() {Name="LTCT SET (200/5)",Code="LTS200",MaterialTypeId=21,IsActive=true,IsDeleted=false},
                    new Material() {Name="LTCT SET (400/5)",Code="LTS400",MaterialTypeId=22,IsActive=true,IsDeleted=false},

                    new Material() {Name="LTCT METER (100/5)",Code="LTM100",MaterialTypeId=23,IsActive=true,IsDeleted=false },
                    new Material() {Name="LTCT METER (200/5)",Code="LTM200",MaterialTypeId=24,IsActive=true,IsDeleted=false },

                    new Material() {Name="LT IN BUILT METER (40-200 A)",Code="LTIM/M137",MaterialTypeId=25,IsActive=true,IsDeleted=false },

                    new Material() {Name="LTCT (DT) METERS -/5",Code="DTM/M146",MaterialTypeId=26,IsActive=true,IsDeleted = false },

                    new Material() {Name="HT METER (-/5)",Code="HTM/M123",MaterialTypeId=27,IsActive=true,IsDeleted = false },
                    new Material() {Name="HT METER (-/5)",Code="HTM/M192",MaterialTypeId=27,IsActive=true,IsDeleted = false },

                    new Material() {Name="HT METER (S/STN.)", Code = "HTMSS", MaterialTypeId = 28, IsActive =true,IsDeleted = false },

                    new Material() {Name="DIGITAL GAUSS METER",Code="E 7005",MaterialTypeId=29,IsActive=true,IsDeleted =false },
                    new Material() {Name="DIGITAL GAUSS METER",Code="E6488 A66",MaterialTypeId=29,IsActive=true,IsDeleted =false },

                    new Material() {Name="ELECTRONIC TESTING SET & BENCH",Code="M 174",MaterialTypeId=30,IsActive=true,IsDeleted=false },

                    new Material() {Name="DT METER 800/5",Code="DTM 800",MaterialTypeId=31,IsActive=true,IsDeleted = false },

                    new Material() {Name="DT SET 800/5",Code="DTC 800",MaterialTypeId=32,IsActive=true,IsDeleted = false },

                    new Material() {Name="DT BOX",Code="DTB/M41",MaterialTypeId=33,IsActive=true,IsDeleted = false},

                    new Material() {Name="CTPT UNIT 10/5",Code="CTPT10/M189",MaterialTypeId=34,IsActive=true,IsDeleted = false },
                    new Material() {Name="CTPT UNIT 10/5",Code="CTPT10/M187",MaterialTypeId=34,IsActive=true,IsDeleted = false },
                    new Material() {Name="CTPT UNIT 10/5",Code="CTPT10/M188",MaterialTypeId=34,IsActive=true,IsDeleted = false },

                    new Material() {Name="CTPT UNIT 20/5",Code="CTPT20/M188",MaterialTypeId=35,IsActive=true,IsDeleted = false },
                    new Material() {Name="CTPT UNIT 20/5",Code="CTPT20/M187",MaterialTypeId=35,IsActive=true,IsDeleted = false },
                    new Material() {Name="CTPT UNIT 20/5",Code="CTPT20/M189",MaterialTypeId=35,IsActive=true,IsDeleted = false },

                    new Material() {Name="CTPT UNIT 30/5",Code="CTPT30/M187",MaterialTypeId=36,IsActive=true,IsDeleted = false },
                    new Material() {Name="CTPT UNIT 30/5",Code="CTPT30/M189",MaterialTypeId=36,IsActive=true,IsDeleted = false },
                    new Material() {Name="CTPT UNIT 30/5",Code="CTPT30/M188",MaterialTypeId=36,IsActive=true,IsDeleted = false },

                    new Material() {Name="CTPT UNIT 50/5",Code="CTPT50/M189",MaterialTypeId=37,IsActive=true,IsDeleted = false },

                    new Material() {Name="CTPT UNIT 75/5",Code="CTPT75/M189",MaterialTypeId=38,IsActive=true,IsDeleted = false },

                    new Material() {Name="CTPT UNIT 100/5",Code="CTPT100/M189",MaterialTypeId=39,IsActive=true,IsDeleted = false },
                    new Material() {Name="CTPT UNIT 100/5",Code="CTPT100/M161",MaterialTypeId=39,IsActive=true,IsDeleted = false },

                    new Material() {Name="CTPT UNIT 150/5",Code="CTPT150/M144",MaterialTypeId=40,IsActive=true,IsDeleted = false },
                    new Material() {Name="CTPT UNIT 150/5",Code="CTPT150/M188",MaterialTypeId=40,IsActive=true,IsDeleted = false },

                    new Material() {Name="CTPT UNIT 200/5",Code="CTPT200/M188",MaterialTypeId=41,IsActive=true,IsDeleted = false },
                    new Material() {Name="CTPT UNIT 200/5",Code="CTPT200/M189",MaterialTypeId=41,IsActive=true,IsDeleted = false },

                    new Material() {Name="CTPT UNIT 300/5",Code="CTPT300/M189",MaterialTypeId=42,IsActive=true,IsDeleted = false },
                    new Material() {Name="CTPT UNIT 300/5",Code="CTPT300/M187",MaterialTypeId=42,IsActive=true,IsDeleted = false },
                    new Material() {Name="CTPT UNIT 300/5",Code="CTPT300/M188",MaterialTypeId=42,IsActive=true,IsDeleted = false },

                    new Material() { Name = "CTPT UNIT 400/5",Code="CTPT400/M189",MaterialTypeId=43, IsActive = true, IsDeleted = false },

                    new Material() {Name="CMRI (DMRI)",Code="CMRI/ENF",MaterialTypeId=44,IsActive=true,IsDeleted = false},
                    new Material() {Name="ALUMINIUM CABLE",Code="LTAC",MaterialTypeId=45,IsActive=true,IsDeleted = false}
                };
                _identityContext.Material.AddRange(MaterialData);
                await _identityContext.SaveChangesAsync();
                _logger.LogInformation("Material data inserted : {@MaterialData}", MaterialData);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception");
            }

        }
        public async Task CreateDefaultCircle()
        {
            try
            {
                var CircleData = new List<Circle>()
                {
                    new Circle(){Name="GURDASPUR",IsActive=true,IsDeleted=false},
                    new Circle(){Name="SUB-URBAN AMRITSAR",IsActive=true,IsDeleted=false},
                    new Circle(){Name="CITY AMRITSAR",IsActive=true,IsDeleted=false},
                    new Circle(){Name="TARN-TARAN",IsActive=true,IsDeleted=false},
                    new Circle(){Name="APDRP",IsActive=true,IsDeleted=false}
                };
                _identityContext.Circle.AddRange(CircleData);
                await _identityContext.SaveChangesAsync();
                _logger.LogInformation("Circle Data inserted : {@CircleData}", CircleData);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception");
            }
        }
        public async Task CreateDefaultDivision()
        {
            try
            {
                var DivisionData = new List<Division>()
                {
                    new Division(){Name="CITY BATALA",IsActive=true,IsDeleted=false,CircleId=1},
                    new Division(){Name="SUB BATALA",IsActive=true,IsDeleted=false,CircleId=1},
                    new Division(){Name="DHARIWAL",IsActive=true,IsDeleted=false,CircleId=1},
                    new Division(){Name="GURDASPUR",IsActive=true,IsDeleted=false,CircleId=1},
                    new Division(){Name="CITY PATHANKOT",IsActive=true,IsDeleted=false,CircleId=1},
                    new Division(){Name="SUB PATHANKOT",IsActive=true,IsDeleted=false,CircleId=1},
                    new Division(){Name="QADIAN",IsActive=true,IsDeleted=false,CircleId=1},

                    new Division(){Name="EAST",IsActive=true,IsDeleted=false,CircleId=2},
                    new Division(){Name="WEST",IsActive=true,IsDeleted=false,CircleId=2},
                    new Division(){Name="SUB ASR",IsActive=true,IsDeleted=false,CircleId=2},
                    new Division(){Name="AJNALA",IsActive=true,IsDeleted=false,CircleId=2},
                    new Division(){Name="JANDIALA",IsActive=true,IsDeleted=false,CircleId=2},

                    new Division(){Name="INDUSTRIAL",IsActive=true,IsDeleted=false,CircleId=3},
                    new Division(){Name="CIVIL LINE",IsActive=true,IsDeleted=false,CircleId=3},
                    new Division(){Name="CITY CENTRE",IsActive=true,IsDeleted=false,CircleId=3},
                    new Division(){Name="HAKIMA GATE",IsActive=true,IsDeleted=false,CircleId=3},

                    new Division(){Name="RAYYA",IsActive=true,IsDeleted=false,CircleId=4},
                    new Division(){Name="CITY TT",IsActive=true,IsDeleted=false,CircleId=4},
                    new Division(){Name="SUB TT",IsActive=true,IsDeleted=false,CircleId=4},
                    new Division(){Name="PATTI",IsActive=true,IsDeleted=false,CircleId=4},
                    new Division(){Name="BHIKHIWIND",IsActive=true,IsDeleted=false,CircleId=4},

                    new Division(){Name="APDRP ASR",IsActive=true,IsDeleted=false,CircleId=5},
                    new Division(){Name="NON APDRP ASR",IsActive=true,IsDeleted=false,CircleId=5},
                    new Division(){Name="APDRP GSP",IsActive=true,IsDeleted=false,CircleId=5},
                };
                _identityContext.Division.AddRange(DivisionData);
                await _identityContext.SaveChangesAsync();
                _logger.LogInformation("Division Data inserted : {@DivisionData}", DivisionData);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception");
            }
        }
        public async Task CreateDefaultSubDivision()
        {
            try
            {
                var SubDivisionData = new List<SubDivision>()
                {
                    new SubDivision(){Name="CITY BATALA",IsActive=true,IsDeleted=false,DivisionId=1},
                    new SubDivision(){Name="SOUTH BATALA",IsActive=true,IsDeleted=false,DivisionId=1},
                    new SubDivision(){Name="EAST BATALA",IsActive=true,IsDeleted=false,DivisionId=1},
                    new SubDivision(){Name="WEST BATALA",IsActive=true,IsDeleted=false,DivisionId=1},
                    new SubDivision(){Name="UDHONWAL",IsActive=true,IsDeleted=false,DivisionId=1},
                    new SubDivision(){Name="PANJ GARAHIAYAN",IsActive=true,IsDeleted=false,DivisionId=1},

                    new SubDivision(){Name="NORTH BATALA",IsActive=true,IsDeleted=false,DivisionId=2},
                    new SubDivision(){Name="MODEL TOWN BATALA",IsActive=true,IsDeleted=false,DivisionId=2},
                    new SubDivision(){Name="K.S MALHI",IsActive=true,IsDeleted=false,DivisionId=2},
                    new SubDivision(){Name="F.G CHURIYAAN CITY",IsActive=true,IsDeleted=false,DivisionId=2},
                    new SubDivision(){Name="ALIWAL",IsActive=true,IsDeleted=false,DivisionId=2},
                    new SubDivision(){Name="D.B NANAK",IsActive=true,IsDeleted=false,DivisionId=2},

                    new SubDivision(){Name="DHARIWAL",IsActive=true,IsDeleted=false,DivisionId=3},
                    new SubDivision(){Name="KALANAUR",IsActive=true,IsDeleted=false,DivisionId=3},
                    new SubDivision(){Name="DEHRIWAL",IsActive=true,IsDeleted=false,DivisionId=3},
                    new SubDivision(){Name="N.M SINGH",IsActive=true,IsDeleted=false,DivisionId=3},

                    new SubDivision(){Name="CITY GSP",IsActive=true,IsDeleted=false,DivisionId=4},
                    new SubDivision(){Name="SUB URBAN GSP",IsActive=true,IsDeleted=false,DivisionId=4},
                    new SubDivision(){Name="JAURA CHHITRAN",IsActive=true,IsDeleted=false,DivisionId=4},
                    new SubDivision(){Name="DURANGLA",IsActive=true,IsDeleted=false,DivisionId=4},
                    new SubDivision(){Name="PURANA CHALLA",IsActive=true,IsDeleted=false,DivisionId=4},
                    new SubDivision(){Name="BEHRAMPUR",IsActive=true,IsDeleted=false,DivisionId=4},
                    new SubDivision(){Name="TIBBER",IsActive=true,IsDeleted=false,DivisionId=4},

                    new SubDivision(){Name="NORTH PTK",IsActive=true,IsDeleted=false,DivisionId=5},
                    new SubDivision(){Name="SOUTH PTK",IsActive=true,IsDeleted=false,DivisionId=5},
                    new SubDivision(){Name="MIRTHAL",IsActive=true,IsDeleted=false,DivisionId=5},
                    new SubDivision(){Name="SARNA",IsActive=true,IsDeleted=false,DivisionId=5},
                    new SubDivision(){Name="SUJANPUR",IsActive=true,IsDeleted=false,DivisionId=5},
                    new SubDivision(){Name="NAROT JAIMAL SINGH",IsActive=true,IsDeleted=false,DivisionId=5},

                    new SubDivision(){Name="EAST PTK",IsActive=true,IsDeleted=false,DivisionId=6},
                    new SubDivision(){Name="DINANAGAR",IsActive=true,IsDeleted=false,DivisionId=6},
                    new SubDivision(){Name="DHAR",IsActive=true,IsDeleted=false,DivisionId=6},
                    new SubDivision(){Name="PANDORI",IsActive=true,IsDeleted=false,DivisionId=6},

                    new SubDivision(){Name="HARCHOWAL",IsActive=true,IsDeleted=false,DivisionId=7},
                    new SubDivision(){Name="KAHNUWAL",IsActive=true,IsDeleted=false,DivisionId=7},
                    new SubDivision(){Name="QADIAN",IsActive=true,IsDeleted=false,DivisionId=7},
                    new SubDivision(){Name="SHRI HARGOBINDPUR",IsActive=true,IsDeleted=false,DivisionId=7},
                    new SubDivision(){Name="GHUMAAN",IsActive=true,IsDeleted=false,DivisionId=7},

                    new SubDivision(){Name="EAST",IsActive=true,IsDeleted=false,DivisionId=8},
                    new SubDivision(){Name="GOPAL NAGAR",IsActive=true,IsDeleted=false,DivisionId=8},
                    new SubDivision(){Name="SOUTH",IsActive=true,IsDeleted=false,DivisionId=8},
                    new SubDivision(){Name="WEST",IsActive=true,IsDeleted=false,DivisionId=8},
                    new SubDivision(){Name="CHHEHARTA",IsActive=true,IsDeleted=false,DivisionId=8},

                    new SubDivision(){Name="CHOGAWAN",IsActive=true,IsDeleted=false,DivisionId=9},
                    new SubDivision(){Name="LOPOKE",IsActive=true,IsDeleted=false,DivisionId=9},
                    new SubDivision(){Name="KHASSA",IsActive=true,IsDeleted=false,DivisionId=9},
                    new SubDivision(){Name="ATTARI",IsActive=true,IsDeleted=false,DivisionId=9},

                    new SubDivision(){Name="HARCHACHINA",IsActive=true,IsDeleted=false,DivisionId=10},
                    new SubDivision(){Name="MAJITHA-1",IsActive=true,IsDeleted=false,DivisionId=10},
                    new SubDivision(){Name="UDHOKE",IsActive=true,IsDeleted=false,DivisionId=10},
                    new SubDivision(){Name="MAJITHA-2",IsActive=true,IsDeleted=false,DivisionId=10},
                    new SubDivision(){Name="KATHUNANGAL",IsActive=true,IsDeleted=false,DivisionId=10},

                    new SubDivision(){Name="AJNALA",IsActive=true,IsDeleted=false,DivisionId=11},
                    new SubDivision(){Name="JASTERWAL",IsActive=true,IsDeleted=false,DivisionId=11},
                    new SubDivision(){Name="RAMDASS",IsActive=true,IsDeleted=false,DivisionId=11},
                    new SubDivision(){Name="FG CHURIYAAN SUB",IsActive=true,IsDeleted=false,DivisionId=11},

                    new SubDivision(){Name="JANDIALA",IsActive=true,IsDeleted=false,DivisionId=12},
                    new SubDivision(){Name="BANDALA",IsActive=true,IsDeleted=false,DivisionId=12},
                    new SubDivision(){Name="TANGRA",IsActive=true,IsDeleted=false,DivisionId=12},
                    new SubDivision(){Name="FATEHPUR-RAJPUTAN",IsActive=true,IsDeleted=false,DivisionId=12},
                    new SubDivision(){Name="KOT MIT SINGH",IsActive=true,IsDeleted=false,DivisionId=12},

                    new SubDivision(){Name="SULTANWIND GATE",IsActive=true,IsDeleted=false,DivisionId=13},
                    new SubDivision(){Name="CHATTIWIND GATE",IsActive=true,IsDeleted=false,DivisionId=13},
                    new SubDivision(){Name="GOLDEN TEMPLE",IsActive=true,IsDeleted=false,DivisionId=13},

                    new SubDivision(){Name="CIVIL LINES",IsActive=true,IsDeleted=false,DivisionId=14},
                    new SubDivision(){Name="LAWRENCE ROAD",IsActive=true,IsDeleted=false,DivisionId=14},
                    new SubDivision(){Name="ISLAMABAAD",IsActive=true,IsDeleted=false,DivisionId=14},

                    new SubDivision(){Name="HUSSAINPURA",IsActive=true,IsDeleted=false,DivisionId=15},
                    new SubDivision(){Name="GHEE MANDI",IsActive=true,IsDeleted=false,DivisionId=15},
                    new SubDivision(){Name="MAAL MANDI",IsActive=true,IsDeleted=false,DivisionId=16},

                    new SubDivision(){Name="HAKIMA GATE",IsActive=true,IsDeleted=false,DivisionId=16},
                    new SubDivision(){Name="TUNDA TALAB",IsActive=true,IsDeleted=false,DivisionId=16},
                    new SubDivision(){Name="DURGIANA MANDIR",IsActive=true,IsDeleted=false,DivisionId=16},

                    new SubDivision(){Name="RAYYA",IsActive=true,IsDeleted=false,DivisionId=17},
                    new SubDivision(){Name="BEAS",IsActive=true,IsDeleted=false,DivisionId=17},
                    new SubDivision(){Name="MEHTA",IsActive=true,IsDeleted=false,DivisionId=17},
                    new SubDivision(){Name="BUTAARI",IsActive=true,IsDeleted=false,DivisionId=17},
                    new SubDivision(){Name="NAGOKE",IsActive=true,IsDeleted=false,DivisionId=17},
                    new SubDivision(){Name="BABA BAKALA",IsActive=true,IsDeleted=false,DivisionId=17},

                    new SubDivision(){Name="SUR SINGH",IsActive=true,IsDeleted=false,DivisionId=18},
                    new SubDivision(){Name="MANOCHAHAL",IsActive=true,IsDeleted=false,DivisionId=18},
                    new SubDivision(){Name="GOHALWAR",IsActive=true,IsDeleted=false,DivisionId=18},
                    new SubDivision(){Name="TAN-TARAN CITY",IsActive=true,IsDeleted=false,DivisionId=18},
                    new SubDivision(){Name="SARAI AMANT KHAN",IsActive=true,IsDeleted=false,DivisionId=18},
                    new SubDivision(){Name="CHUBHAL",IsActive=true,IsDeleted=false,DivisionId=18},

                    new SubDivision(){Name="FATEHABAD",IsActive=true,IsDeleted=false,DivisionId=19},
                    new SubDivision(){Name="TARN TARAN SUB",IsActive=true,IsDeleted=false,DivisionId=19},
                    new SubDivision(){Name="KHADOOR SAHIB",IsActive=true,IsDeleted=false,DivisionId=19},
                    new SubDivision(){Name="NAUSHERA PANUAAN",IsActive=true,IsDeleted=false,DivisionId=19},

                    new SubDivision(){Name="PATTI CITY",IsActive=true,IsDeleted=false,DivisionId=20},
                    new SubDivision(){Name="PATTI SUB",IsActive=true,IsDeleted=false,DivisionId=20},
                    new SubDivision(){Name="SARHALI",IsActive=true,IsDeleted=false,DivisionId=20},
                    new SubDivision(){Name="HARIKE",IsActive=true,IsDeleted=false,DivisionId=20},
                    new SubDivision(){Name="KAIRON",IsActive=true,IsDeleted=false,DivisionId=20},

                    new SubDivision(){Name="BHIKHIWIND",IsActive=true,IsDeleted=false,DivisionId=21},
                    new SubDivision(){Name="KHALRA",IsActive=true,IsDeleted=false,DivisionId=21},
                    new SubDivision(){Name="AMARKOT",IsActive=true,IsDeleted=false,DivisionId=21},
                    new SubDivision(){Name="KHEMKARAN",IsActive=true,IsDeleted=false,DivisionId=21},

                    new SubDivision(){Name="APDRP ASR",IsActive=true,IsDeleted=false,DivisionId=22},

                    new SubDivision(){Name="NON APDRP ASR",IsActive=true,IsDeleted=false,DivisionId=23},

                    new SubDivision(){Name="APDRP GSP",IsActive=true,IsDeleted=false,DivisionId=24},

                    new SubDivision(){Name="APDRP",IsActive=true,IsDeleted=false,DivisionId=25},
                    new SubDivision(){Name="METERING JALANDHAR",IsActive=true,IsDeleted=false,DivisionId=26},


                };
                _identityContext.SubDivision.AddRange(SubDivisionData);
                await _identityContext.SaveChangesAsync();
                _logger.LogInformation("SubDivision Data inserted : {@SubDivisionData}", SubDivisionData);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception");
            }
        }


    }
}
