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

        public EmployeeController(IEmployeeRepo _empRepo, IIntakeRepo _intakeRepo, IDepartmentRepo _deptRepo, IAttendanceRecordRepo _attendanceRecordRepo, IInstructorRepo _intsRepo, ITrackRepo _trackRepo, IStudentRepo _studentRepo)
        {
			empRepo = _empRepo;
			intakeRepo = _intakeRepo;
			deptRepo = _deptRepo;
			attendanceRecordRepo = _attendanceRecordRepo;
			instRepo = _intsRepo;
			trackRepo = _trackRepo;
			studentRepo = _studentRepo;
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


	}
}
