using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using MVCProject.Models;
using MVCProject.Repos;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MVCProject.Controllers
{
    public class InstructorController : Controller
    {
        private IInstructorRepo instRepo;
        private IPermissionRepo permissionRepo;
        private IStudentMessageRepo stdMsgRepo;
        private ITrackRepo trackRepo;
        private IStudentRepo studentRepo;
        private IAttendanceRecordRepo attendanceRecordRepo;

        public InstructorController(IInstructorRepo _instRepo,
                                    IPermissionRepo _permissionRepo,
                                    IStudentMessageRepo _stdMsgRepo,
                                    ITrackRepo _trackRepo,
                                    IStudentRepo _studentRepo,
                                    IAttendanceRecordRepo _attendanceRecordRepo) 
        {
            instRepo = _instRepo; 
            permissionRepo = _permissionRepo;
            stdMsgRepo = _stdMsgRepo;
            trackRepo = _trackRepo;
            studentRepo = _studentRepo;
            attendanceRecordRepo = _attendanceRecordRepo;
        }
        public IActionResult Index(int id)
        {
            ViewBag.InstructorId = id;
            Instructor inst = instRepo.GetInstructorByID(id); 
            return View(inst);
        }

        public IActionResult ShowPermissions(int id, string? message)
        {
            ViewBag.instructor = instRepo.GetInstructorByID(id);
            ViewBag.InstructorId = id;
            if (message != null)
            {
                ViewBag.Message = message;
            }
            else
            {
                ViewBag.Message = null;
            }
            var permissions = permissionRepo.GetPermissionsBySupervisorID(id);
            return View(permissions);
        }
        public void DealWithPermission(int permissionId, string permissionStatus)
        {
            //get the permission by the stdId and the date
            Permission p = permissionRepo.GetPermissionByID(permissionId);
            //check if it is not null then
            if (p != null)
            {
                //set its status to be accepted
                permissionRepo.UpdatePermissionStatus(p, permissionStatus);
                //send a message to the student
                string messageContent = $"Your permission for day {p.Date} has been {permissionStatus}";
                stdMsgRepo.CreateNewMessage(p.StdID, DateTime.Now, messageContent);
            }
        }
        public IActionResult AcceptPermission(int permissionId, int id)
        {
            DealWithPermission(permissionId, "Accepted");
            //redirect to the showPermissions
            string acceptedMessage = "Permission has been accepted!";
            return RedirectToAction("showPermissions", "instructor", new {id=id, message= acceptedMessage });
        }

        public IActionResult RefusePermission(int permissionId, int id)
        {
            DealWithPermission(permissionId, "Denied");
            //redirect to the showPermissions
            string denialMessage = "Permission has been denied!";
            return RedirectToAction("showPermissions", "instructor", new { id = id, message= denialMessage });
        }

        public IActionResult showStudentsDegrees(int? id)
        {
            if (id == null) return BadRequest();
            Instructor instructor = instRepo.GetInstructorByID(id.Value);
            if (instructor == null) return NotFound();

            //get the instructor track
            Track track = trackRepo.GetTrackById(instructor.TrackID);
            if (track == null) return NotFound();
            //get all the students who are in the instructor track
            List<Student> students = studentRepo.GetTrackStudents(track.Id);

            ViewBag.instructor = instructor;
            ViewBag.InstructorId = id;
            ViewBag.Track = track;

            return View(students);
        }

        public IActionResult showAttendanceRecords(int? id)
        {
            if (id == null) return BadRequest();
            Instructor instructor = instRepo.GetInstructorByID(id.Value);
            if (instructor == null) return NotFound();

            //get the instructor track
            Track track = trackRepo.GetTrackById(instructor.TrackID);
            if (track == null) return NotFound();
            //get all the students who are in the instructor track
            List<Student> students = studentRepo.GetTrackStudents(track.Id);
            //Get the daily attendance records for these students 
            List<DailyAttendanceRecord> dailyAttendanceRecords =
                attendanceRecordRepo.GetAttendanceRecords(students, DateOnly.FromDateTime(DateTime.Today));

            ViewBag.instructor = instructor;
            ViewBag.InstructorId = id;
            ViewBag.Track = track;
            ViewBag.Date = DateOnly.FromDateTime(DateTime.Today);
            if (dailyAttendanceRecords.Count > 0)
                ViewBag.RecordsDate = dailyAttendanceRecords[0].Date;
            else
                ViewBag.RecordsDate = null;


            return View(dailyAttendanceRecords);
        }

        [HttpPost]
        public IActionResult ShowRecordsForDate(int? instID, DateOnly? date)
        {
            if (instID == null) return BadRequest();
            //check if the date is correct
            Instructor instructor = instRepo.GetInstructorByID(instID.Value);
            if (instructor == null) return NotFound();

            //get the instructor track
            Track track = trackRepo.GetTrackById(instructor.TrackID);
            if (track == null) return NotFound();
            //get all the students who are in the instructor track
            List<Student> students = studentRepo.GetTrackStudents(track.Id);

            ViewBag.instructor = instructor;
            ViewBag.InstructorId = instID;
            ViewBag.Track = track;
            ViewBag.Date = date;


            if (date > DateOnly.FromDateTime(DateTime.Today))
            {
                ModelState.AddModelError("", "The date must be less than today's date");
                return View("showAttendanceRecords");
            }
            if (date == null)
            {
                ModelState.AddModelError("", "Choose a date first");
                return View("showAttendanceRecords");
            }
            //Get the daily attendance records for these students 
            List<DailyAttendanceRecord> dailyAttendanceRecords =
                attendanceRecordRepo.GetAttendanceRecords(students, date.Value);

            if (dailyAttendanceRecords.Count > 0)
                ViewBag.RecordsDate = dailyAttendanceRecords[0].Date;
            else
                ViewBag.RecordsDate = null;

            return View("showAttendanceRecords", dailyAttendanceRecords);
        }
    }
}
