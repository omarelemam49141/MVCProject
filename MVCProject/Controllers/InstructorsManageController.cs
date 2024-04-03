using Microsoft.AspNetCore.Mvc;

namespace MVCProject.Controllers
{
    public class InstructorsManageController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
