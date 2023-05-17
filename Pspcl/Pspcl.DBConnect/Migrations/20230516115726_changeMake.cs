using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pspcl.DBConnect.Migrations
{
    /// <inheritdoc />
    public partial class changeMake : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StockIssueBook_Circle_CircleId",
                table: "StockIssueBook");

            migrationBuilder.DropIndex(
                name: "IX_StockIssueBook_CircleId",
                table: "StockIssueBook");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateIndex(
                name: "IX_StockIssueBook_CircleId",
                table: "StockIssueBook",
                column: "CircleId");

            migrationBuilder.AddForeignKey(
                name: "FK_StockIssueBook_Circle_CircleId",
                table: "StockIssueBook",
                column: "CircleId",
                principalTable: "Circle",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
