using Microsoft.EntityFrameworkCore;
using MVCProject.Models;

namespace MVCProject.Data
{
    public class attendanceDBContext: DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<DailyAttendanceRecord> DailyAttendanceRecords { get; set; }
        public DbSet<Intake> Intakes { get; set; }
        public DbSet<Track> Tracks { get; set; }
        public DbSet<_Program> Programs { get; set; }
        public DbSet<StudentIntakeTrack> StudentIntakeTracks { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Permission> Permissions { get; set; }



        public attendanceDBContext(DbContextOptions<attendanceDBContext> options) : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StudentIntakeTrack>(entity =>
            {
                entity.HasKey(e => new { e.StdID, e.IntakeID });
            });
            modelBuilder.Entity<Track>(entity =>
            {

                entity.HasOne(t => t.Supervisor)
                    .WithOne()
                    .HasForeignKey<Track>(t => t.SupervisorID)
                    .IsRequired(false);
                modelBuilder.Entity<Track>()
                 .Property(t => t.SupervisorID)
                  .IsRequired(false);
               });

            base.OnModelCreating(modelBuilder);
        }
    }
}
