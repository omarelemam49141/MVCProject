using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVCProject.Migrations
{
    /// <inheritdoc />
    public partial class yearndept2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Intakes_ProgramId",
                table: "Intakes");

            migrationBuilder.CreateIndex(
                name: "IX_Intakes_ProgramId",
                table: "Intakes",
                column: "ProgramId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Intakes_ProgramId",
                table: "Intakes");

            migrationBuilder.CreateIndex(
                name: "IX_Intakes_ProgramId",
                table: "Intakes",
                column: "ProgramId",
                unique: true);
        }
    }
}
