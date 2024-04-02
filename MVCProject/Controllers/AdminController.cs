using Microsoft.AspNetCore.Mvc;

namespace MVCProject.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {

            return View();
        }
    }
}
