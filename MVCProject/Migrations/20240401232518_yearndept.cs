using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVCProject.Migrations
{
    /// <inheritdoc />
    public partial class yearndept : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProgramId",
                table: "Intakes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "Year",
                table: "Intakes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_Intakes_ProgramId",
                table: "Intakes",
                column: "ProgramId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Intakes_Programs_ProgramId",
                table: "Intakes",
                column: "ProgramId",
                principalTable: "Programs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Intakes_Programs_ProgramId",
                table: "Intakes");

            migrationBuilder.DropIndex(
                name: "IX_Intakes_ProgramId",
                table: "Intakes");

            migrationBuilder.DropColumn(
                name: "ProgramId",
                table: "Intakes");

            migrationBuilder.DropColumn(
                name: "Year",
                table: "Intakes");
        }
    }
}
