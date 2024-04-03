using Microsoft.AspNetCore.Mvc;
using MVCProject.Repos;

namespace MVCProject.Controllers
{
    public class IntakeController : Controller
    {
        public IIntakeRepo intakeRepo { get; set; }
        public IntakeController(IIntakeRepo _intakeRepo)
        {
            intakeRepo = _intakeRepo; 

        }
        public IActionResult Index()
        {
            var intakes = intakeRepo.GetAllIntakes();
            return View(intakes);
        }
        public IActionResult Add() { 
            return View();
        }
    }
}
