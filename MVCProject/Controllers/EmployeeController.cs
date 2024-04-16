using Microsoft.AspNetCore.Mvc;
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
        private readonly IStudentIntakeTrackRepo studentIntakeTrackRepo;

        public EmployeeController(IEmployeeRepo _empRepo
									,IIntakeRepo _intakeRepo
									,IDepartmentRepo _deptRepo
									,IAttendanceRecordRepo _attendanceRecordRepo
									,IInstructorRepo _intsRepo
									,ITrackRepo _trackRepo
									,IStudentRepo _studentRepo
									,IStudentIntakeTrackRepo studentIntakeTrackRepo)
        {
			empRepo = _empRepo;
			intakeRepo = _intakeRepo;
			deptRepo = _deptRepo;
			attendanceRecordRepo = _attendanceRecordRepo;
			instRepo = _intsRepo;
			trackRepo = _trackRepo;
			studentRepo = _studentRepo;
            this.studentIntakeTrackRepo = studentIntakeTrackRepo;
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
        public IActionResult RecordAttendance()
		{
			//send tracks to the view
			ViewBag.Tracks = trackRepo.GetActiveTracks();
			//send students in that track to the view
			ViewBag.Students = studentRepo.GetAllStudents();
            return View();
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

		public IActionResult showAttendanceRecords(string trackName, string intakeName)
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

			ViewBag.Tracks = trackRepo.GetAll();
			ViewBag.Intakes = intakeRepo.GetAllIntakes();

			ViewBag.selectedTrackName = trackName;
			ViewBag.selectedIntakeName = intakeName;

			return View(dailyAttendanceRecords);
		}

		[HttpPost]
		public IActionResult ShowRecordsForDate(DateOnly? date, int? trackId, int? intakeId)
		{
            //get all the tracks and all the intakes
            ViewBag.Tracks = trackRepo.GetAll();
            ViewBag.Intakes = intakeRepo.GetAllIntakes();

            //Validations
            if (trackId == null || intakeId == null) return BadRequest();

            if (date > DateOnly.FromDateTime(DateTime.Today))
            {
                ModelState.AddModelError("", "The date must be less than today's date");
            }
            if (date == null)
            {
                ModelState.AddModelError("", "Choose a date first");
            }
			if(trackId == 0)
			{
                ModelState.AddModelError("", "Choose a track first");
            }
            if (intakeId == 0)
            {
                ModelState.AddModelError("", "Choose an intake first");
            }

            if (!ModelState.IsValid)
            {
                ViewBag.selectedTrackName = "All tracks";
                ViewBag.selectedIntakeName = "All intakes";
                return View("showAttendanceRecords");
            }

            //Get the students in the selected track and the selected intake
            List<StudentIntakeTrack> studentIntakeTracks 
				= studentIntakeTrackRepo.GetByTrackAndIntakeIDs(trackId.Value, intakeId.Value);

			//Fill the list of the students with the selected students
            List<Student> students = new List<Student>();
            foreach (var item in studentIntakeTracks)
            {
				students.Add(item.Student);
            }
            ViewBag.Date = date;
						
			//Get the daily attendance records for these students 
			List<DailyAttendanceRecord> dailyAttendanceRecords =
				attendanceRecordRepo.GetAttendanceRecords(students, date.Value);

			if (dailyAttendanceRecords.Count > 0)
				ViewBag.RecordsDate = dailyAttendanceRecords[0].Date;
			else
				ViewBag.RecordsDate = null;

            ViewBag.selectedTrackName = trackRepo.GetTrackById(trackId.Value).Name;
            ViewBag.selectedIntakeName = intakeRepo.GetIntakeById(intakeId.Value).Name;

            return View("showAttendanceRecords", dailyAttendanceRecords);
		}
	}
}
