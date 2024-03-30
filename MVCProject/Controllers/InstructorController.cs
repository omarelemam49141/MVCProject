using Microsoft.AspNetCore.Mvc;
using MVCProject.Models;
using MVCProject.Repos;

namespace MVCProject.Controllers
{
    public class InstructorController : Controller
    {
        private IInstructorRepo instRepo;

        public InstructorController(IInstructorRepo _instRepo) { instRepo = _instRepo; }
        public IActionResult Index(int id)
        {
            Instructor inst = instRepo.GetInstructorByID(id); 
            return View(inst);
        }
    }
}
