using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Pspcl.Core.Domain;

namespace Pspcl.DBConnect
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Circle> Circle { get; set; }
        public DbSet<Division> Division { get; set; }
        public DbSet<Login> Login { get; set; }
        public DbSet<Material> Material { get; set; }
        public DbSet<MaterialGroup> MaterialGroup { get; set; }
        public DbSet<MaterialType> MaterialType { get; set; }
        public DbSet<Stock> Stock { get; set; }
        public DbSet<StockBookMaterial> StockBookMaterial { get; set;}
        public DbSet<StockIssueBook> StockIssueBook { get; set; }
        public DbSet<StockMaterial> StockMaterial { get; set; }
        public DbSet<StockMaterialSeries> StockMaterialSeries { get; set;}
        public DbSet<SubDivision> SubDivision { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }



        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            SeedUsers(builder);
        }

        private void SeedUsers(ModelBuilder builder)
        {
            User user = new User()
            {
                Id = "b74ddd14-6340-4840-95c2-db12554843e5",
                UserName = "admin@gmail.com",
                Email = "admin@gmail.com",
                LockoutEnabled = false,
                PhoneNumber = "1234567890",
                IsActive = true,
                EmailConfirmed = true,
                PhoneNumberConfirmed = true

            };

            PasswordHasher<User> passwordHasher = new PasswordHasher<User>();
            user.PasswordHash = passwordHasher.HashPassword(user,"test@123");

            builder.Entity<User>().HasData(user);
        }

    }
}
