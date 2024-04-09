using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVCProject.Migrations
{
    /// <inheritdoc />
    public partial class M3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "read",
                table: "StudentMessage",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "Year",
                table: "Intakes",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "read",
                table: "StudentMessage");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Year",
                table: "Intakes",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");
        }
    }
}
