using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MVCProject.Models;
using MVCProject.Repos;
using System.Security.Claims;

namespace MVCProject.Controllers
{
	[Authorize(Roles = "employee")]
    public class EmployeeController : Controller
    {

        private IEmployeeRepo empRepo;
        private IIntakeRepo intakeRepo;
        private IDepartmentRepo deptRepo;
		private IAttendanceRecordRepo attendanceRecordRepo;
		private IInstructorRepo	instRepo;
		private ITrackRepo trackRepo;
		private IStudentRepo studentRepo;
		private IStudentIntakeTrackRepo studentIntakeTrackRepo;
		private IPermissionRepo permissionRepo;
		private IDailyAttendanceRepo dailyAttendanceRepo;

		public EmployeeController(IDailyAttendanceRepo _dailyAttendanceRepo ,IPermissionRepo _permissionRepo,
			IEmployeeRepo _empRepo, IIntakeRepo _intakeRepo, 
			IDepartmentRepo _deptRepo, IAttendanceRecordRepo _attendanceRecordRepo,
			IInstructorRepo _intsRepo, ITrackRepo _trackRepo,
			IStudentRepo _studentRepo,
			IStudentIntakeTrackRepo _studentIntakeTrackRepo)
        {
			empRepo = _empRepo;
			intakeRepo = _intakeRepo;
			deptRepo = _deptRepo;
			attendanceRecordRepo = _attendanceRecordRepo;
			instRepo = _intsRepo;
			trackRepo = _trackRepo;
			studentRepo = _studentRepo;
			studentIntakeTrackRepo = _studentIntakeTrackRepo;
	        permissionRepo = _permissionRepo;
			dailyAttendanceRepo = _dailyAttendanceRepo;
		}

        public IActionResult Index(int id)
		{
			ViewBag.EmployeeId = id;
			return View();
		}

		public IActionResult ShowProfile()
		{
		
			var emp = empRepo.GetEmployeeById(Int32.Parse(User.Claims.FirstOrDefault(e => e.Type == ClaimTypes.NameIdentifier).Value));
			return View(emp);
		}

		/********************************* record attendance **************************************/
		[Route("Employee/RecordAttendance/{Tid?}/{Iid?}/{date?}")]
		public IActionResult RecordAttendance(int? Tid, int? Iid , DateOnly date)
		{
			
			
			if(Tid != null && Iid != null)
			{

                ViewBag.track = Tid;
				ViewBag.intake = Iid;
				var students = studentRepo.GetStudentsByIntakeTrack(Iid.Value, Tid.Value);
				ViewBag.Tracks = trackRepo.GetActiveTracks();
				ViewBag.Intakes = intakeRepo.GetAllIntakes();
				var attendanceRecords =  dailyAttendanceRepo.getRecordsByDate(date, students.Select(s=>s.Id).ToList());
				if(attendanceRecords.Count == 0)
				{
					foreach (var student in students)
					{
						DailyAttendanceRecord record = new DailyAttendanceRecord();
						record.StdID = student.Id;
						record.Date = date;
						record.Status = "Absent";
						record.StudentDegree = 25;
						record.Date = DateOnly.FromDateTime(DateTime.UtcNow.Date);
						dailyAttendanceRepo.AddRecordAttendance(record);
					}
					attendanceRecords = dailyAttendanceRepo.getRecordsByDate(date, students.Select(s => s.Id).ToList());
				}
				ViewBag.attendanceRecords = attendanceRecords;
				ViewBag.Date = date;

				////////////////

				return View();
			}
			ViewBag.Tracks = trackRepo.GetActiveTracks();
			ViewBag.Intakes = intakeRepo.GetAllIntakes();
            return View();
        }
		[HttpPost]
		public IActionResult SaveRecords(DailyAttendanceRecord record , int intake , int track)
		{
			
			var permission =  permissionRepo.GetPermissionByStd(record.StdID);
			if (record.Status == "Absent" || record.Status == "Late")
			{
				if (permission == null)
				{
					record.StudentDegree = 25;
				}
				else
				{
					if (permission.Status == "Accepted")
					{
						record.StudentDegree = 5;
					}
					else
					{
						record.StudentDegree = 15;
					}

				}
			}
			else
			{
				record.StudentDegree = 0;
			}
			record.TimeOfAttendance = TimeOnly.FromDateTime(DateTime.Now);
			dailyAttendanceRepo.AddRecordAttendance(record);
			var std = studentRepo.GetStudentById(record.StdID);
			std.StudentDegree -= record.StudentDegree;
			studentRepo.UpdateStudent(std);
			return Redirect(Url.Action("RecordAttendance" , "Employee") +   "?Tid="+track + "&Iid=" + intake+ "&date=" +record.Date.ToString());

		}


        /*---------------------------------------------------------------------------------------------------*/

        public IActionResult showStudentsDegrees()
		{
			//get all the students who are in the instructor track
			List<Student> students = studentRepo.GetAllStudents();
			return View(students);
		}

		public IActionResult showAttendanceRecords()
		{
			
			//get all the students who are in the instructor track
			List<Student> students = studentRepo.GetAllStudents();
			//Get the daily attendance records for these students 
			List<DailyAttendanceRecord> dailyAttendanceRecords =
				attendanceRecordRepo.GetAttendanceRecords(students, DateOnly.FromDateTime(DateTime.Today));

			ViewBag.Date = DateOnly.FromDateTime(DateTime.Today);
			if (dailyAttendanceRecords.Count > 0)
				ViewBag.RecordsDate = dailyAttendanceRecords[0].Date;
			else
				ViewBag.RecordsDate = null;


			return View(dailyAttendanceRecords);
		}

		[HttpPost]
		public IActionResult ShowRecordsForDate(DateOnly? date)
		{
			//get all the students who are in the instructor track
			List<Student> students = studentRepo.GetAllStudents();
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



		public IActionResult ManageStudents()
		{
			List<Student> allStudents = studentRepo.GetAllStudents();

			List<Student> studentsHasTrackIntake = studentIntakeTrackRepo.getAllStudents().Select(s => s.Student).ToList();

			List<Student> studentsHasNoTrackOrIntake = allStudents.Except(studentsHasTrackIntake).ToList();

/*			ViewBag.Intakes = intakeRepo.GetAllIntakes();
			ViewBag.Tracks = trackRepo.GetActiveTracks();*/
			return View(studentsHasNoTrackOrIntake);
		}

		public IActionResult Manage(int id)
		{
			Student student = studentRepo.GetStudentById(id);
			ViewBag.Intakes = intakeRepo.GetAllIntakes();
			ViewBag.Tracks = trackRepo.GetActiveTracks();
			return View(student);
		}

		[HttpPost]

		public IActionResult Manage(int id, int intakeId, int trackId)
		{
			studentIntakeTrackRepo.AddStudentIntakeTrack(id, intakeId, trackId);
			return RedirectToAction("ManageStudents");
		}

		/*[HttpPost]
		public IActionResult Manage(int id, int intakeId, int trackId)
		{
			studentIntakeTrackRepo.AddStudentIntakeTrack(id, intakeId, trackId);
			var message = new Message
			{
				StudentId = id,
				// Add any additional information you need for the message
				// For example:
				Content = $"A new student has been added with ID: {id}"
			};

			// Save the message to the database
			// Assuming you have a DbContext instance called dbContext
			dbContext.Messages.Add(message);
			dbContext.SaveChanges();
			return RedirectToAction("ManageStudents");
		}*/
	}
}
