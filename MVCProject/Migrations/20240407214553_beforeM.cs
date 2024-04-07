using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVCProject.Migrations
{
    /// <inheritdoc />
    public partial class beforeM : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tracks_Instructors_SupervisorID",
                table: "Tracks");

            migrationBuilder.DropIndex(
                name: "IX_Tracks_SupervisorID",
                table: "Tracks");

            migrationBuilder.AlterColumn<int>(
                name: "SupervisorID",
                table: "Tracks",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Tracks_SupervisorID",
                table: "Tracks",
                column: "SupervisorID",
                unique: true,
                filter: "[SupervisorID] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Tracks_Instructors_SupervisorID",
                table: "Tracks",
                column: "SupervisorID",
                principalTable: "Instructors",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tracks_Instructors_SupervisorID",
                table: "Tracks");

            migrationBuilder.DropIndex(
                name: "IX_Tracks_SupervisorID",
                table: "Tracks");

            migrationBuilder.AlterColumn<int>(
                name: "SupervisorID",
                table: "Tracks",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tracks_SupervisorID",
                table: "Tracks",
                column: "SupervisorID",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Tracks_Instructors_SupervisorID",
                table: "Tracks",
                column: "SupervisorID",
                principalTable: "Instructors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
