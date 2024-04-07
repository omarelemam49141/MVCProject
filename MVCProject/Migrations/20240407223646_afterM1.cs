using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVCProject.Migrations
{
    /// <inheritdoc />
    public partial class afterM1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Instructors_Tracks_TrackSupervisedId",
                table: "Instructors");

            migrationBuilder.DropIndex(
                name: "IX_Instructors_TrackSupervisedId",
                table: "Instructors");

            migrationBuilder.DropColumn(
                name: "TrackSupervisedId",
                table: "Instructors");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TrackSupervisedId",
                table: "Instructors",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Instructors_TrackSupervisedId",
                table: "Instructors",
                column: "TrackSupervisedId");

            migrationBuilder.AddForeignKey(
                name: "FK_Instructors_Tracks_TrackSupervisedId",
                table: "Instructors",
                column: "TrackSupervisedId",
                principalTable: "Tracks",
                principalColumn: "Id");
        }
    }
}
