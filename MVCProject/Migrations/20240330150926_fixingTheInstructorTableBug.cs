using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVCProject.Migrations
{
    /// <inheritdoc />
    public partial class fixingTheInstructorTableBug : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Instructors_Departments_DepartmentId",
                table: "Instructors");

            migrationBuilder.DropIndex(
                name: "IX_Instructors_DepartmentId",
                table: "Instructors");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "Instructors");

            migrationBuilder.CreateIndex(
                name: "IX_Instructors_DeptID",
                table: "Instructors",
                column: "DeptID");

            migrationBuilder.AddForeignKey(
                name: "FK_Instructors_Departments_DeptID",
                table: "Instructors",
                column: "DeptID",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Instructors_Departments_DeptID",
                table: "Instructors");

            migrationBuilder.DropIndex(
                name: "IX_Instructors_DeptID",
                table: "Instructors");

            migrationBuilder.AddColumn<int>(
                name: "DepartmentId",
                table: "Instructors",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Instructors_DepartmentId",
                table: "Instructors",
                column: "DepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Instructors_Departments_DepartmentId",
                table: "Instructors",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }
    }
}
