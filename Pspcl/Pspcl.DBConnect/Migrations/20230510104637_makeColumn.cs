using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pspcl.DBConnect.Migrations
{
    /// <inheritdoc />
    public partial class makeColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Make",
                table: "StockBookMaterial",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Make",
                table: "StockBookMaterial");
        }
    }
}
