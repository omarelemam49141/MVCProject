using MVCProject.Models; // Assuming EmployeeRepo is defined in this namespace
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using MVCProject.Repos;
using MVCProject.Data;

namespace MVCProject.Controllers
{
    public class EmployeesManageController : Controller
    {
        private readonly IEmployeeRepo _employeeRepo;
        private readonly attendanceDBContext ctx;

        public EmployeesManageController(IEmployeeRepo employeeRepo , attendanceDBContext _ctx)
        {
            _employeeRepo = employeeRepo;
            ctx = _ctx;
        }

        public IActionResult Index()
        {
            List<Employee> employees = _employeeRepo.GetAllEmployees();
            return View(employees);
        }

        public IActionResult Add()
        {
            ViewBag.depts = ctx.Departments.ToList();
            return View();
        }
        [HttpPost]
        public IActionResult Add(Employee employee)
        {
            _employeeRepo.AddEmployee(employee);
            return RedirectToAction("Index");

        }
        [HttpGet]
        public bool DeleteEmployee(int id) {
            _employeeRepo.DeleteEmployee(id);
            return true;
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.depts = ctx.Departments.ToList();
            var employee = _employeeRepo.GetEmployeeById(id);
            return View("Add",employee);
        }
        [HttpPost]
        public IActionResult Edit(Employee employee)
        {

            _employeeRepo.UpdateEmployee(employee);
            return RedirectToAction("Index");
        }
    }
}
