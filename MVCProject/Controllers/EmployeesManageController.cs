using MVCProject.Models; // Assuming EmployeeRepo is defined in this namespace
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using MVCProject.Repos;
using MVCProject.Data;
using Microsoft.AspNetCore.Authorization;

namespace MVCProject.Controllers
{
    [Authorize(Roles = "admin")]
    public class EmployeesManageController : Controller
    {
        private readonly IEmployeeRepo _employeeRepo;
        private readonly attendanceDBContext ctx;
        private readonly IAllDBEmails allDBEmails;

        public EmployeesManageController(IEmployeeRepo employeeRepo , attendanceDBContext _ctx, IAllDBEmails _allDBEmails)
        {
            _employeeRepo = employeeRepo;
            ctx = _ctx;
            allDBEmails = _allDBEmails;
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
        [HttpGet]
        public bool ValidateName(string Name, int id)
        {

            return _employeeRepo.GetAllEmployees().FirstOrDefault(a => a.Name == Name && a.Id != id) == null;
        }
        [HttpPost]
        public IActionResult Edit(Employee employee)
        {

            _employeeRepo.UpdateEmployee(employee);
            return RedirectToAction("Index");
        }
        public bool ValidateEmail(string email, int id)
        {
            email = Uri.UnescapeDataString(email);


            var emails = allDBEmails.GetEmails();

            var s = emails.FirstOrDefault(s => s.email == email);
            if (s == null)
            {
                return true;
            }
            else
            {
                var student = _employeeRepo.GetAllEmployees().FirstOrDefault(s => s.Email == email);
                if (student.Id == id)
                    return true;
                else
                    return false;

            }
        }
    }
}
