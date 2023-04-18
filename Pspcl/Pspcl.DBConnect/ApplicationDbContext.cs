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

    }
}
