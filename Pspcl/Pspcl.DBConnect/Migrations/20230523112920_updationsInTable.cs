using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pspcl.DBConnect.Migrations
{
    /// <inheritdoc />
    public partial class updationsInTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("UPDATE Division SET LocationCode = 325 WHERE Id = 1");
            migrationBuilder.Sql("UPDATE Division SET LocationCode = 326 WHERE Id = 2");
            migrationBuilder.Sql("UPDATE Division SET LocationCode = 324 WHERE Id = 3");
            migrationBuilder.Sql("UPDATE Division SET LocationCode = 323 WHERE Id = 4");
            migrationBuilder.Sql("UPDATE Division SET LocationCode = 321 WHERE Id = 5");
            migrationBuilder.Sql("UPDATE Division SET LocationCode = 322 WHERE Id = 6");
            migrationBuilder.Sql("UPDATE Division SET LocationCode = 327 WHERE Id = 7");
            migrationBuilder.Sql("UPDATE Division SET LocationCode = 331 WHERE Id = 8");
            migrationBuilder.Sql("UPDATE Division SET LocationCode = 332 WHERE Id = 9");
            migrationBuilder.Sql("UPDATE Division SET LocationCode = 333 WHERE Id = 10");
            migrationBuilder.Sql("UPDATE Division SET LocationCode = 334 WHERE Id = 11");
            migrationBuilder.Sql("UPDATE Division SET LocationCode = 343 WHERE Id = 12");
            migrationBuilder.Sql("UPDATE Division SET LocationCode = 490 WHERE Id = 13");
            migrationBuilder.Sql("UPDATE Division SET LocationCode = 490 WHERE Id = 14");
            migrationBuilder.Sql("UPDATE Division SET LocationCode = 490 WHERE Id = 15");
            migrationBuilder.Sql("UPDATE Division SET LocationCode = 490 WHERE Id = 16");
            migrationBuilder.Sql("UPDATE Division SET LocationCode = 344 WHERE Id = 17");
            migrationBuilder.Sql("UPDATE Division SET LocationCode = 341 WHERE Id = 18");
            migrationBuilder.Sql("UPDATE Division SET LocationCode = 342 WHERE Id = 19");
            migrationBuilder.Sql("UPDATE Division SET LocationCode = 345 WHERE Id = 20");
            migrationBuilder.Sql("UPDATE Division SET LocationCode = 346 WHERE Id = 21");
            migrationBuilder.Sql("UPDATE Division SET LocationCode = 813 WHERE Id = 22");
            migrationBuilder.Sql("UPDATE Division SET LocationCode = 813 WHERE Id = 23");
            migrationBuilder.Sql("UPDATE Division SET LocationCode = 813 WHERE Id = 24");
            

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
