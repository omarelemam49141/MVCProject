using Microsoft.AspNetCore.Mvc;

namespace MVCProject.Controllers
{
    public class StudentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult checkStudentDegree(int StudentDegree)
        {
            if (StudentDegree > 0 && StudentDegree < 251) return Json(true);
            else return Json(false);
        }
    }
}
