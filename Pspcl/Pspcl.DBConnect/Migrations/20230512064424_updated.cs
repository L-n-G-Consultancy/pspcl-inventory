using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pspcl.DBConnect.Migrations
{
    /// <inheritdoc />
    public partial class updated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "StockMaterialSeries",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<string>(
                name: "GrnNumber",
                table: "Stock",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "Make",
                table: "Stock",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StockMaterialSeries",
                table: "StockMaterialSeries",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_StockMaterialSeries",
                table: "StockMaterialSeries");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "StockMaterialSeries");

            migrationBuilder.DropColumn(
                name: "Make",
                table: "Stock");

            migrationBuilder.AlterColumn<int>(
                name: "GrnNumber",
                table: "Stock",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
