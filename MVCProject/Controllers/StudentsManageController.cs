
using Ganss.Excel;
using Microsoft.AspNetCore.Mvc;
using MVCProject.Models;
using MVCProject.Repos;

namespace MVCProject.Controllers
{
    public class StudentsManageController : Controller
    {
        private readonly IStudentRepo studentRepo;
        public StudentsManageController(IStudentRepo _studentRepo) {
            studentRepo = _studentRepo;
        }

        public IActionResult Index()
        {
            var students = studentRepo.GetAllStudents();
            return View(students);
        }
        public IActionResult AddBulk(IFormFile Sheet)
        {
            using(var fs = new FileStream("wwwroot\\Sheets\\"+Sheet.FileName, FileMode.OpenOrCreate)) { 
            Sheet.CopyTo(fs);
            
            }

            var students = new ExcelMapper("wwwroot\\Sheets\\" + Sheet.FileName).Fetch().Select(s => new Student
            {
                Specialization = s.Specialization,
                Name = s.Name,
                Password = s.Password,
                Email = s.Email,
                Mobile = s.Mobile,
                Faculty = s.Faculty,
                GraduationYear = s.GraduationYear,
                StudentDegree = Convert.ToInt32(s.StudentDegree), // Casting double to int
                University = s.University,
            }).ToList();

            studentRepo.AddRangeOfStudents(students);

            return RedirectToAction("Index");
        }
    }
}
