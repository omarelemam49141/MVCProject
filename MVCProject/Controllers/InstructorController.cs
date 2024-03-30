using Microsoft.AspNetCore.Mvc;

namespace MVCProject.Controllers
{
    public class InstructorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
