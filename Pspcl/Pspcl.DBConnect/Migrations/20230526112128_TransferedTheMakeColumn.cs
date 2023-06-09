using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pspcl.DBConnect.Migrations
{
    /// <inheritdoc />
    public partial class TransferedTheMakeColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Make",
                table: "StockIssueBook");

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

            migrationBuilder.AddColumn<string>(
                name: "Make",
                table: "StockIssueBook",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
