using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MVCProject.Models;
using MVCProject.Repos;

namespace MVCProject.Controllers
{
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
            var employee = empRepo.GetEmployeeById(id);
			return View(employee);
		}
        public IActionResult Edit(int id)
        {
            //if(id == null) return BadRequest();
            //var employee = empRepo.GetEmployeeById(id.Value);
            var employee = empRepo.GetEmployeeById(id);
			ViewBag.EmployeeId = id;
            return View(employee);
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
						record.StudentDegree = 0;
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









		/*-----------------------------------------------------------------------------------------*/



		[HttpPost]
        public IActionResult Edit(int? id, Employee emp)
        {
            if (id == null) return BadRequest();
            Employee employee = empRepo.GetEmployeeById(id.Value);
            if (employee == null) return NotFound();
            employee.Name = emp.Name;
            employee.Email = emp.Email;
            employee.Password = emp.Password;
            employee.Mobile = emp.Mobile;
            employee.Type = emp.Type;
            empRepo.UpdateEmployee(employee);
			
            return RedirectToAction("Index", new { id = employee.Id });
        }
        /*[HttpPost]
        public IActionResult Edit(Employee emp)
        {
			empRepo.UpdateEmployee(emp);
			return RedirectToAction("Index");
		}*/

        /*
         public IActionResult Edit(int? id)
        {
            if(id == null) return BadRequest();
            Employee emp = empRepo.GetEmployeeById(id.Value);
            if(emp == null) return NotFound();
            ViewBag.EmployeeId = id;
            return View(emp);

		}

		[HttpPost]
        public IActionResult Edit(int? id, Employee emp)
        {
			if(id == null) return BadRequest();
			Employee employee = empRepo.GetEmployeeById(id.Value);
			if(employee == null) return NotFound();
			employee.Name = emp.Name;
			employee.Email = emp.Email;
			employee.Password = emp.Password;
			employee.Mobile = emp.Mobile;
            employee.Type = emp.Type;
			empRepo.UpdateEmployee(employee);
			return RedirectToAction("Index", new { id = employee.Id });
		}
         */





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
			//List<StudentIntakeTrack> studentsHasTrackIntake = studentIntakeTrackRepo.getAllStudents();

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
            //Student student = studentRepo.GetStudentById(id);
			studentIntakeTrackRepo.AddStudentIntakeTrack(id, intakeId, trackId);
            return RedirectToAction("ManageStudents");
        }
	}
}
