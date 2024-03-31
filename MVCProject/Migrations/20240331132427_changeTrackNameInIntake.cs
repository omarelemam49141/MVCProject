using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVCProject.Migrations
{
    /// <inheritdoc />
    public partial class changeTrackNameInIntake : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IntakeTrack_Tracks_IntakesId1",
                table: "IntakeTrack");

            migrationBuilder.RenameColumn(
                name: "IntakesId1",
                table: "IntakeTrack",
                newName: "TracksId");

            migrationBuilder.RenameIndex(
                name: "IX_IntakeTrack_IntakesId1",
                table: "IntakeTrack",
                newName: "IX_IntakeTrack_TracksId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "GraduationYear",
                table: "Students",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AddForeignKey(
                name: "FK_IntakeTrack_Tracks_TracksId",
                table: "IntakeTrack",
                column: "TracksId",
                principalTable: "Tracks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IntakeTrack_Tracks_TracksId",
                table: "IntakeTrack");

            migrationBuilder.RenameColumn(
                name: "TracksId",
                table: "IntakeTrack",
                newName: "IntakesId1");

            migrationBuilder.RenameIndex(
                name: "IX_IntakeTrack_TracksId",
                table: "IntakeTrack",
                newName: "IX_IntakeTrack_IntakesId1");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "GraduationYear",
                table: "Students",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddForeignKey(
                name: "FK_IntakeTrack_Tracks_IntakesId1",
                table: "IntakeTrack",
                column: "IntakesId1",
                principalTable: "Tracks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
