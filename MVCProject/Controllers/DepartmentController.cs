using Microsoft.AspNetCore.Mvc;
using MVCProject.Models;
using MVCProject.Repos;
namespace MVCProject.Controllers
{

    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepo _departmentRepo;

        public DepartmentController(IDepartmentRepo departmentRepo)
        {
            _departmentRepo = departmentRepo;
        }

        public IActionResult Index()
        {
            var departments = _departmentRepo.GetAllDepartments();
            return View(departments);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(Department department)
        {
            _departmentRepo.AddDepartment(department);
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var department = _departmentRepo.GetDepartmentById(id);
            if (department == null)
            {
                return NotFound();
            }
            return View("Add",department);
        }

        [HttpPost]
        public IActionResult Edit(Department department)
        {
            _departmentRepo.UpdateDepartment(department);
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            try
            {
                _departmentRepo.RemoveDepartment(id);
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                ViewBag.reasons = new List<string>()
                {
                    "This Department Already has Some Records So this will Affect the Other tables"
                };
                return View("AdminError");
            }
            
        }

        public bool ValidateName(string Name,int Id)

        {
            var dept = _departmentRepo.GetAllDepartments().FirstOrDefault(d => d.Name == Name);
            if (dept == null)
            {
                return true;
            }else if(dept != null && dept.Id == Id)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

}
