using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVCProject.Migrations
{
    /// <inheritdoc />
    public partial class afterM : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Programs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Programs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Mobile = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    University = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Faculty = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Specialization = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GraduationYear = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StudentDegree = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mobile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeptID = table.Column<int>(type: "int", nullable: false),
                    DepartmentId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employees_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Intakes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProgramId = table.Column<int>(type: "int", nullable: false),
                    Year = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Intakes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Intakes_Programs_ProgramId",
                        column: x => x.ProgramId,
                        principalTable: "Programs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "DailyAttendanceRecords",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    StdID = table.Column<int>(type: "int", nullable: false),
                    TimeOfAttendance = table.Column<TimeOnly>(type: "time", nullable: false),
                    TimeOfLeave = table.Column<TimeOnly>(type: "time", nullable: false),
                    StudentDegree = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DailyAttendanceRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DailyAttendanceRecords_Students_StdID",
                        column: x => x.StdID,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "StudentMessage",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentID = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentMessage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentMessage_Students_StudentID",
                        column: x => x.StudentID,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Instructors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mobile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeptID = table.Column<int>(type: "int", nullable: false),
                    TrackSupervisedId = table.Column<int>(type: "int", nullable: true),
                    TrackID = table.Column<int>(type: "int", nullable: false),
                    IntakeID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instructors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Instructors_Departments_DeptID",
                        column: x => x.DeptID,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Instructors_Intakes_IntakeID",
                        column: x => x.IntakeID,
                        principalTable: "Intakes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Permissions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    StdID = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InstructorID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Permissions_Instructors_InstructorID",
                        column: x => x.InstructorID,
                        principalTable: "Instructors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Permissions_Students_StdID",
                        column: x => x.StdID,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Tracks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    programID = table.Column<int>(type: "int", nullable: false),
                    SupervisorID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tracks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tracks_Instructors_SupervisorID",
                        column: x => x.SupervisorID,
                        principalTable: "Instructors",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Tracks_Programs_programID",
                        column: x => x.programID,
                        principalTable: "Programs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "IntakeTrack",
                columns: table => new
                {
                    IntakesId = table.Column<int>(type: "int", nullable: false),
                    TracksId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IntakeTrack", x => new { x.IntakesId, x.TracksId });
                    table.ForeignKey(
                        name: "FK_IntakeTrack_Intakes_IntakesId",
                        column: x => x.IntakesId,
                        principalTable: "Intakes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_IntakeTrack_Tracks_TracksId",
                        column: x => x.TracksId,
                        principalTable: "Tracks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Schedules",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    StartPeriod = table.Column<TimeOnly>(type: "time", nullable: false),
                    TrackID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedules", x => x.id);
                    table.ForeignKey(
                        name: "FK_Schedules_Tracks_TrackID",
                        column: x => x.TrackID,
                        principalTable: "Tracks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "StudentIntakeTracks",
                columns: table => new
                {
                    StdID = table.Column<int>(type: "int", nullable: false),
                    IntakeID = table.Column<int>(type: "int", nullable: false),
                    TrackID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentIntakeTracks", x => new { x.StdID, x.IntakeID });
                    table.ForeignKey(
                        name: "FK_StudentIntakeTracks_Intakes_IntakeID",
                        column: x => x.IntakeID,
                        principalTable: "Intakes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_StudentIntakeTracks_Students_StdID",
                        column: x => x.StdID,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_StudentIntakeTracks_Tracks_TrackID",
                        column: x => x.TrackID,
                        principalTable: "Tracks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DailyAttendanceRecords_StdID",
                table: "DailyAttendanceRecords",
                column: "StdID");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_DepartmentId",
                table: "Employees",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Instructors_DeptID",
                table: "Instructors",
                column: "DeptID");

            migrationBuilder.CreateIndex(
                name: "IX_Instructors_IntakeID",
                table: "Instructors",
                column: "IntakeID");

            migrationBuilder.CreateIndex(
                name: "IX_Instructors_TrackID",
                table: "Instructors",
                column: "TrackID");

            migrationBuilder.CreateIndex(
                name: "IX_Instructors_TrackSupervisedId",
                table: "Instructors",
                column: "TrackSupervisedId");

            migrationBuilder.CreateIndex(
                name: "IX_Intakes_ProgramId",
                table: "Intakes",
                column: "ProgramId");

            migrationBuilder.CreateIndex(
                name: "IX_IntakeTrack_TracksId",
                table: "IntakeTrack",
                column: "TracksId");

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_InstructorID",
                table: "Permissions",
                column: "InstructorID");

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_StdID",
                table: "Permissions",
                column: "StdID");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_TrackID",
                table: "Schedules",
                column: "TrackID");

            migrationBuilder.CreateIndex(
                name: "IX_StudentIntakeTracks_IntakeID",
                table: "StudentIntakeTracks",
                column: "IntakeID");

            migrationBuilder.CreateIndex(
                name: "IX_StudentIntakeTracks_TrackID",
                table: "StudentIntakeTracks",
                column: "TrackID");

            migrationBuilder.CreateIndex(
                name: "IX_StudentMessage_StudentID",
                table: "StudentMessage",
                column: "StudentID");

            migrationBuilder.CreateIndex(
                name: "IX_Tracks_programID",
                table: "Tracks",
                column: "programID");

            migrationBuilder.CreateIndex(
                name: "IX_Tracks_SupervisorID",
                table: "Tracks",
                column: "SupervisorID",
                unique: true,
                filter: "[SupervisorID] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Instructors_Tracks_TrackID",
                table: "Instructors",
                column: "TrackID",
                principalTable: "Tracks",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Instructors_Tracks_TrackSupervisedId",
                table: "Instructors",
                column: "TrackSupervisedId",
                principalTable: "Tracks",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Instructors_Departments_DeptID",
                table: "Instructors");

            migrationBuilder.DropForeignKey(
                name: "FK_Instructors_Intakes_IntakeID",
                table: "Instructors");

            migrationBuilder.DropForeignKey(
                name: "FK_Instructors_Tracks_TrackID",
                table: "Instructors");

            migrationBuilder.DropForeignKey(
                name: "FK_Instructors_Tracks_TrackSupervisedId",
                table: "Instructors");

            migrationBuilder.DropTable(
                name: "DailyAttendanceRecords");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "IntakeTrack");

            migrationBuilder.DropTable(
                name: "Permissions");

            migrationBuilder.DropTable(
                name: "Schedules");

            migrationBuilder.DropTable(
                name: "StudentIntakeTracks");

            migrationBuilder.DropTable(
                name: "StudentMessage");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropTable(
                name: "Intakes");

            migrationBuilder.DropTable(
                name: "Tracks");

            migrationBuilder.DropTable(
                name: "Instructors");

            migrationBuilder.DropTable(
                name: "Programs");
        }
    }
}
