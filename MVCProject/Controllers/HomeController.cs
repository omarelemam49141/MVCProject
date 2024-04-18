using Microsoft.AspNetCore.Mvc;
using MVCProject.Models;
using NPOI.SS.Formula.Functions;
using System.Diagnostics;
using System.Security.Claims;

namespace MVCProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {

            if(!User.Identity.IsAuthenticated)
                    return RedirectToAction("Login","Account");
            else
            {
                var Id = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
                if (User.IsInRole("admin"))
                {
                    return RedirectToAction("index", "admin");
                }else if (User.IsInRole("instructor") || User.IsInRole("supervisor"))
                {
                    return RedirectToAction("index", "instructor", new { id = Id });

                }else
                {
                    RedirectToAction("index", "employee", new { id = Id });
                }
                return View();
            }

        }

        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
