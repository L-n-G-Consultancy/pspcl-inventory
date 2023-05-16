using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pspcl.DBConnect.Migrations
{
    /// <inheritdoc />
    public partial class DeletedSrNoFromAndTo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SerialNumberFrom",
                table: "StockBookMaterial");

            migrationBuilder.DropColumn(
                name: "SerialNumberTo",
                table: "StockBookMaterial");

            migrationBuilder.AddColumn<DateTime>(
                name: "SrNoDate",
                table: "StockIssueBook",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SrNoDate",
                table: "StockIssueBook");

            migrationBuilder.AddColumn<int>(
                name: "SerialNumberFrom",
                table: "StockBookMaterial",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SerialNumberTo",
                table: "StockBookMaterial",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
