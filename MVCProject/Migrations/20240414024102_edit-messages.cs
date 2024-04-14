using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVCProject.Migrations
{
    /// <inheritdoc />
    public partial class editmessages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentMessage_Students_StudentID",
                table: "StudentMessage");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentMessage",
                table: "StudentMessage");

            migrationBuilder.RenameTable(
                name: "StudentMessage",
                newName: "StudentMessages");

            migrationBuilder.RenameColumn(
                name: "read",
                table: "StudentMessages",
                newName: "Read");

            migrationBuilder.RenameIndex(
                name: "IX_StudentMessage_StudentID",
                table: "StudentMessages",
                newName: "IX_StudentMessages_StudentID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentMessages",
                table: "StudentMessages",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentMessages_Students_StudentID",
                table: "StudentMessages",
                column: "StudentID",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentMessages_Students_StudentID",
                table: "StudentMessages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentMessages",
                table: "StudentMessages");

            migrationBuilder.RenameTable(
                name: "StudentMessages",
                newName: "StudentMessage");

            migrationBuilder.RenameColumn(
                name: "Read",
                table: "StudentMessage",
                newName: "read");

            migrationBuilder.RenameIndex(
                name: "IX_StudentMessages_StudentID",
                table: "StudentMessage",
                newName: "IX_StudentMessage_StudentID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentMessage",
                table: "StudentMessage",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentMessage_Students_StudentID",
                table: "StudentMessage",
                column: "StudentID",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
