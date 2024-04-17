﻿using System;
using System.Collections.Generic;
using System.Linq;
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

        public IActionResult Index(int id)
        {
            ViewBag.StudentId = id;
            Student std = stdRepo.GetStudentByID(id);
            ViewBag.UnreadMessagesCount = studentMessageRepo.GetUnreadMessagesCount(id);

            return View(std);
        }
        public IActionResult checkStudentDegree(int StudentDegree)
        {
            if (StudentDegree > 0 && StudentDegree < 251) return Json(true);
            else return Json(false);
        }

        public IActionResult ShowPermissions(int id)
        {
            ViewBag.StudentId = id;
            ViewBag.Student = stdRepo.GetStudentByID(id);
            ViewBag.UnreadMessagesCount = studentMessageRepo.GetUnreadMessagesCount(id);

            var permissions = stdRepo.GetStudentPermissions(id);
            return View(permissions);
        }
        [HttpGet]
        public IActionResult RequestPermission(int id)
        {
            ViewBag.StudentId = id;
            ViewBag.Student = stdRepo.GetStudentByID(id);
            ViewBag.UnreadMessagesCount = studentMessageRepo.GetUnreadMessagesCount(id);
            return View(new Permission());
        }

        [HttpPost]
        public IActionResult RequestPermission([Bind("Date,Type,Status,StdID")] Permission permission)
        {
            try
            {

                permission.InstructorID = stdRepo.GetInstructorIdByStudentId(permission.StdID);
                stdRepo.RequestPermission(permission);
                ViewBag.Message = "Permission Requested Successfully";
                return RedirectToAction("ShowPermissions", new { id = permission.StdID });
            }
            catch (Exception e)
            {
                ViewBag.Message = "Error in Requesting Permission";
                return View(permission);
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




        public IActionResult ShowSchedule(int id)
        {

            ViewBag.Student = stdRepo.GetStudentByID(id);
            ViewBag.StudentId = id;
            int trackId = stdRepo.GetTrackIdByStudentId(id);
            ViewBag.Track = trackRepo.GetTrackById(trackId);
            ViewBag.UnreadMessagesCount = studentMessageRepo.GetUnreadMessagesCount(id);

            var schedules = scheduleRepo.GetSchedulesByDateAndTrack(DateOnly.FromDateTime(DateTime.Now), stdRepo.GetTrackIdByStudentId(id));
            return View(schedules);


        }

        public IActionResult ShowAttendanceRecords(int id, DateOnly? startDate = null, int numberOfDays = 7)
        {
            ViewBag.Student = stdRepo.GetStudentByID(id);
            ViewBag.StudentId = id;
            ViewBag.UnreadMessagesCount = studentMessageRepo.GetUnreadMessagesCount(id);


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

        public IActionResult ShowProfile(int id, string? message = null)
        {
            Student std = stdRepo.GetStudentByID(id);
            ViewBag.StudentId = id;
            ViewBag.Student = std;
            ViewBag.Message = message;

            ViewBag.UnreadMessagesCount = studentMessageRepo.GetUnreadMessagesCount(id);


            return View(std);
        }

        [HttpPost]
        public IActionResult UpdateProfile([Bind("Id,Name,Password,Mobile")] Student std)
        {


            try
            {
                if (std.Name == null || std.Password == null || std.Mobile == null)
                {
                    ViewBag.Message = "Please Fill All Fields";
                    return View("ShowProfile", std);
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
                Console.WriteLine(e);
                ViewBag.Message = "Error in Updating Profile";
                ViewBag.StudentId = std.Id;
                ViewBag.Student = std;
                ViewBag.UnreadMessagesCount = studentMessageRepo.GetUnreadMessagesCount(std.Id);
                return View("ShowProfile", std);
            }
        }

        public IActionResult ShowMessages(int id)
        {
            ViewBag.StudentId = id;
            ViewBag.Student = stdRepo.GetStudentByID(id);
            ViewBag.UnreadMessagesCount = studentMessageRepo.GetUnreadMessagesCount(id);
            var messages = studentMessageRepo.GetStudentMessages(id);
            return View(messages);
        }

        public IActionResult MarkAllMessagesAsRead(int id)
        {
            studentMessageRepo.MarkAllMessagesAsRead(id);
            var messages = studentMessageRepo.GetStudentMessages(id);
            return PartialView("MessagesPartial",messages);
        }
        [AllowAnonymous]
        public JsonResult IsEmailAvailable(string email)
        {
            return Json(!stdRepo.IsEmailInUse(email));
        }





    }
}
