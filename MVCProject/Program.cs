using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

using MVCProject.Data;
using MVCProject.Repos;

namespace MVCProject
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<attendanceDBContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("con1")
                )
            );
            builder.Services.AddScoped<IInstructorRepo, InstructorRepo>();
            builder.Services.AddScoped<IStudentRepo, StudentRepo>();
            builder.Services.AddScoped<IEmployeeRepo, EmployeeRepo>();
            builder.Services.AddScoped<IPermissionRepo, PermissionRepo>();
            builder.Services.AddScoped<IStudentMessageRepo, StudentMessageRepo>();
            builder.Services.AddScoped<ITrackRepo, TrackRepo>();
            builder.Services.AddScoped<IScheduleRepo, ScheduleRepo>();
            builder.Services.AddScoped<IIntakeRepo, IntakeRepo>();
            builder.Services.AddScoped<IAttendanceRecordRepo, AttendanceRecordRepo>();
            builder.Services.AddScoped<IDepartmentRepo, DepartmentRepo>();

            builder.Services.AddAuthentication().AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
            {
                options.ExpireTimeSpan = TimeSpan.FromDays(1); // Set the expiration time to one day
            });
   


            var app = builder.Build();
            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapControllers();
            app.Run();
        }
    }
}
