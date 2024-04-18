using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCProject.Data;
using MVCProject.Models;
using MVCProject.Repos;

namespace MVCProject.Controllers
{
    [Authorize(Roles = "student")]
    public class StudentController : Controller
    {
        private readonly IStudentRepo stdRepo;
        private readonly IScheduleRepo scheduleRepo;
        private readonly ITrackRepo trackRepo;
        private readonly IStudentMessageRepo studentMessageRepo;
        public StudentController(IStudentRepo _stdRepo, IScheduleRepo _scheduleRepo, ITrackRepo _trackRepo, IStudentMessageRepo _studentMessageRepo)
        {
            stdRepo = _stdRepo;
            scheduleRepo = _scheduleRepo;
            trackRepo = _trackRepo;
            studentMessageRepo = _studentMessageRepo;
        }

        public IActionResult Index()
        {
            try
            {

                int id = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                ViewBag.StudentId = id;
                Student std = stdRepo.GetStudentByID(id);
                ViewBag.UnreadMessagesCount = studentMessageRepo.GetUnreadMessagesCount(id);
                ViewBag.IsStudentEnrolledInTrack = stdRepo.IsStudentEnrolledInTrack(id);

                return View(std);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return RedirectToAction("Login", "Account");
            }
        }
        public IActionResult checkStudentDegree(int StudentDegree)
        {
            if (StudentDegree > 0 && StudentDegree < 251) return Json(true);
            else return Json(false);
        }

        public IActionResult ShowPermissions()
        {
            try
            {

                int id = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

                ViewBag.StudentId = id;
                ViewBag.Student = stdRepo.GetStudentByID(id);
                ViewBag.UnreadMessagesCount = studentMessageRepo.GetUnreadMessagesCount(id);

                ViewBag.IsStudentEnrolledInTrack = stdRepo.IsStudentEnrolledInTrack(id);

                var permissions = stdRepo.GetStudentPermissions(id);
                return View(permissions);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return RedirectToAction("Login", "Account");
            }
        }
        [HttpGet]
        public IActionResult RequestPermission(string? message)
        {
            try
            {
                int id = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                ViewBag.StudentId = id;
                ViewBag.Student = stdRepo.GetStudentByID(id);
                ViewBag.UnreadMessagesCount = studentMessageRepo.GetUnreadMessagesCount(id);
                ViewBag.IsStudentEnrolledInTrack = stdRepo.IsStudentEnrolledInTrack(id);
                ViewBag.Message = message;

                return View(new Permission());
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return RedirectToAction("RequestPermission", new { message = "error requesting a permission" });
            }
        }

        [HttpPost]
        public IActionResult RequestPermission([Bind("Date,Type,Status,StdID")] Permission permission)
        {
            try
            {

                permission.InstructorID = stdRepo.GetInstructorIdByStudentId(permission.StdID);
                stdRepo.RequestPermission(permission);
                return RedirectToAction("ShowPermissions");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return RedirectToAction("RequestPermission", new { message = "error requesting a permission" });

            }
        }


        public IActionResult DeletePermission(int id)
        {
            try
            {
                stdRepo.DeletePermission(id);
                return Json(true);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Json(false);
            }
        }

        public IActionResult GetPermissions(int id)
        {
            var permissions = stdRepo.GetStudentPermissions(id);
            return PartialView("PermissionsTableBody", permissions);
        }




        public IActionResult ShowSchedule()
        {
            try
            {
                int id = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

                ViewBag.StudentId = id;
                ViewBag.Student = stdRepo.GetStudentByID(id);
                int trackId = stdRepo.GetTrackIdByStudentId(id);
                ViewBag.Track = trackRepo.GetTrackById(trackId);
                ViewBag.UnreadMessagesCount = studentMessageRepo.GetUnreadMessagesCount(id);
                var schedules = scheduleRepo.GetSchedulesByDateAndTrack(DateOnly.FromDateTime(DateTime.Now), stdRepo.GetTrackIdByStudentId(id));
                return View(schedules);
            }
            catch (Exception)
            {
                return RedirectToAction("Login", "Account");

            }


        }

        public IActionResult ShowAttendanceRecords(DateOnly? startDate = null, int numberOfDays = 7)
        {
            int id = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            ViewBag.StudentId = id;
            ViewBag.Student = stdRepo.GetStudentByID(id);
            ViewBag.UnreadMessagesCount = studentMessageRepo.GetUnreadMessagesCount(id);
            ViewBag.IsStudentEnrolledInTrack = stdRepo.IsStudentEnrolledInTrack(id);


            if (startDate == null)
            {
                startDate = DateOnly.FromDateTime(DateTime.Now.AddDays(-1));
            }

            var dailyAttendances = stdRepo.GetDailyAttendanceRecordsByStudentId(id, numberOfDays, startDate.Value);

            return View(dailyAttendances);

        }

        public IActionResult GetAttendanceRecordsPartial(int id, DateOnly startDate, int numberOfDays)
        {

            var dailyAttendances = stdRepo.GetDailyAttendanceRecordsByStudentId(id, numberOfDays, startDate);

            return PartialView("AttendanceRecordsTableBody", dailyAttendances);

        }

        public IActionResult ShowProfile(string? message = null)
        {
            int id = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            ViewBag.StudentId = id;
            Student std = stdRepo.GetStudentByID(id);
            ViewBag.Student = std;
            ViewBag.Message = message;
            ViewBag.UnreadMessagesCount = studentMessageRepo.GetUnreadMessagesCount(id);
            ViewBag.IsStudentEnrolledInTrack = stdRepo.IsStudentEnrolledInTrack(id);
            return View(std);
        }

        [HttpPost]
        public IActionResult UpdateProfile([Bind("Id,Name,Password,Mobile")] Student std)
        {


            try
            {
                if (std.Name == null || std.Password == null || std.Mobile == null)
                {
                    return RedirectToAction("ShowProfile", new { message = "error Please fill all the fields" });
                }


                Student student = stdRepo.GetStudentByID(std.Id);
                student.Name = std.Name;
                student.Password = std.Password;
                student.Mobile = std.Mobile;



                stdRepo.UpdateStudent(student);
                return RedirectToAction("ShowProfile", new { id = std.Id, message = "Profile Updated Successfully" });
            }
            catch (Exception e)
            {
                return RedirectToAction("ShowProfile", new { message = "error updating profile" });
            }
        }

        public IActionResult ShowMessages()
        {
            int id = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            ViewBag.StudentId = id;
            ViewBag.Student = stdRepo.GetStudentByID(id);
            ViewBag.UnreadMessagesCount = studentMessageRepo.GetUnreadMessagesCount(id);
            ViewBag.IsStudentEnrolledInTrack = stdRepo.IsStudentEnrolledInTrack(id);
            var messages = studentMessageRepo.GetStudentMessages(id);
            return View(messages);
        }

        public IActionResult MarkAllMessagesAsRead(int id)
        {
            studentMessageRepo.MarkAllMessagesAsRead(id);
            var messages = studentMessageRepo.GetStudentMessages(id);
            return PartialView("MessagesPartial", messages);
        }
        [AllowAnonymous]
        public JsonResult IsEmailAvailable(string email)
        {
            return Json(!stdRepo.IsEmailInUse(email));
        }





    }
}
