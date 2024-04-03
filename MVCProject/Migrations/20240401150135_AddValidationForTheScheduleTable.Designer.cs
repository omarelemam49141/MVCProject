﻿// <auto-generated />
using System;
using MVCProject.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MVCProject.Migrations
{
    [DbContext(typeof(attendanceDBContext))]
    [Migration("20240401150135_AddValidationForTheScheduleTable")]
    partial class AddValidationForTheScheduleTable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("IntakeTrack", b =>
                {
                    b.Property<int>("IntakesId")
                        .HasColumnType("int");

                    b.Property<int>("TracksId")
                        .HasColumnType("int");

                    b.HasKey("IntakesId", "TracksId");

                    b.HasIndex("TracksId");

                    b.ToTable("IntakeTrack");
                });

            modelBuilder.Entity("MVCProject.Models.DailyAttendanceRecord", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateOnly>("Date")
                        .HasColumnType("date");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("StdID")
                        .HasColumnType("int");

                    b.Property<int>("StudentDegree")
                        .HasColumnType("int");

                    b.Property<TimeOnly>("TimeOfAttendance")
                        .HasColumnType("time");

                    b.Property<TimeOnly>("TimeOfLeave")
                        .HasColumnType("time");

                    b.HasKey("Id");

                    b.HasIndex("StdID");

                    b.ToTable("DailyAttendanceRecords");
                });

            modelBuilder.Entity("MVCProject.Models.Department", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Departments");
                });

            modelBuilder.Entity("MVCProject.Models.Employee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("DepartmentId")
                        .HasColumnType("int");

                    b.Property<int>("DeptID")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Mobile")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("DepartmentId");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("MVCProject.Models.Instructor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("DeptID")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Mobile")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("DeptID");

                    b.ToTable("Instructors");
                });

            modelBuilder.Entity("MVCProject.Models.Intake", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Intakes");
                });

            modelBuilder.Entity("MVCProject.Models.Permission", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateOnly>("Date")
                        .HasColumnType("date");

                    b.Property<int>("InstructorID")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("StdID")
                        .HasColumnType("int");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("InstructorID");

                    b.HasIndex("StdID");

                    b.ToTable("Permissions");
                });

            modelBuilder.Entity("MVCProject.Models.Schedule", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<DateOnly>("Date")
                        .HasColumnType("date");

                    b.Property<TimeOnly>("StartPeriod")
                        .HasColumnType("time");

                    b.Property<int>("TrackID")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("TrackID");

                    b.ToTable("Schedules");
                });

            modelBuilder.Entity("MVCProject.Models.Student", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Faculty")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("GraduationYear")
                        .HasColumnType("datetime2");

                    b.Property<string>("Mobile")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Specialization")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("University")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("MVCProject.Models.StudentIntakeTrack", b =>
                {
                    b.Property<int>("StdID")
                        .HasColumnType("int");

                    b.Property<int>("IntakeID")
                        .HasColumnType("int");

                    b.Property<int>("TrackID")
                        .HasColumnType("int");

                    b.HasKey("StdID", "IntakeID");

                    b.HasIndex("IntakeID");

                    b.HasIndex("TrackID");

                    b.ToTable("StudentIntakeTracks");
                });

            modelBuilder.Entity("MVCProject.Models.StudentMessage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Content")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("StudentID")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("StudentID");

                    b.ToTable("StudentMessage");
                });

            modelBuilder.Entity("MVCProject.Models.Track", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SupervisorID")
                        .HasColumnType("int");

                    b.Property<int>("programID")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SupervisorID")
                        .IsUnique();

                    b.HasIndex("programID");

                    b.ToTable("Tracks");
                });

            modelBuilder.Entity("MVCProject.Models._Program", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Programs");
                });

            modelBuilder.Entity("IntakeTrack", b =>
                {
                    b.HasOne("MVCProject.Models.Intake", null)
                        .WithMany()
                        .HasForeignKey("IntakesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MVCProject.Models.Track", null)
                        .WithMany()
                        .HasForeignKey("TracksId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MVCProject.Models.DailyAttendanceRecord", b =>
                {
                    b.HasOne("MVCProject.Models.Student", "Student")
                        .WithMany("AttendaceRecords")
                        .HasForeignKey("StdID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Student");
                });

            modelBuilder.Entity("MVCProject.Models.Employee", b =>
                {
                    b.HasOne("MVCProject.Models.Department", "Department")
                        .WithMany("Employees")
                        .HasForeignKey("DepartmentId");

                    b.Navigation("Department");
                });

            modelBuilder.Entity("MVCProject.Models.Instructor", b =>
                {
                    b.HasOne("MVCProject.Models.Department", "Department")
                        .WithMany("Instructors")
                        .HasForeignKey("DeptID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Department");
                });

            modelBuilder.Entity("MVCProject.Models.Permission", b =>
                {
                    b.HasOne("MVCProject.Models.Instructor", "Instructor")
                        .WithMany("Permissions")
                        .HasForeignKey("InstructorID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MVCProject.Models.Student", "Student")
                        .WithMany("Permissions")
                        .HasForeignKey("StdID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Instructor");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("MVCProject.Models.Schedule", b =>
                {
                    b.HasOne("MVCProject.Models.Track", "Track")
                        .WithMany("Schedules")
                        .HasForeignKey("TrackID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Track");
                });

            modelBuilder.Entity("MVCProject.Models.StudentIntakeTrack", b =>
                {
                    b.HasOne("MVCProject.Models.Intake", "Intake")
                        .WithMany("StudentIntakeTracks")
                        .HasForeignKey("IntakeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MVCProject.Models.Student", "Student")
                        .WithMany("StudentIntakeTracks")
                        .HasForeignKey("StdID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MVCProject.Models.Track", "Track")
                        .WithMany("StudentIntakeTracks")
                        .HasForeignKey("TrackID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Intake");

                    b.Navigation("Student");

                    b.Navigation("Track");
                });

            modelBuilder.Entity("MVCProject.Models.StudentMessage", b =>
                {
                    b.HasOne("MVCProject.Models.Student", "Student")
                        .WithMany("StudentMessages")
                        .HasForeignKey("StudentID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Student");
                });

            modelBuilder.Entity("MVCProject.Models.Track", b =>
                {
                    b.HasOne("MVCProject.Models.Instructor", "Supervisor")
                        .WithOne("TrackSupervised")
                        .HasForeignKey("MVCProject.Models.Track", "SupervisorID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MVCProject.Models._Program", "Program")
                        .WithMany()
                        .HasForeignKey("programID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Program");

                    b.Navigation("Supervisor");
                });

            modelBuilder.Entity("MVCProject.Models.Department", b =>
                {
                    b.Navigation("Employees");

                    b.Navigation("Instructors");
                });

            modelBuilder.Entity("MVCProject.Models.Instructor", b =>
                {
                    b.Navigation("Permissions");

                    b.Navigation("TrackSupervised");
                });

            modelBuilder.Entity("MVCProject.Models.Intake", b =>
                {
                    b.Navigation("StudentIntakeTracks");
                });

            modelBuilder.Entity("MVCProject.Models.Student", b =>
                {
                    b.Navigation("AttendaceRecords");

                    b.Navigation("Permissions");

                    b.Navigation("StudentIntakeTracks");

                    b.Navigation("StudentMessages");
                });

            modelBuilder.Entity("MVCProject.Models.Track", b =>
                {
                    b.Navigation("Schedules");

                    b.Navigation("StudentIntakeTracks");
                });
#pragma warning restore 612, 618
        }
    }
}
