using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCProject.Data;
using MVCProject.Models;
using MVCProject.Repos;

namespace MVCProject.Controllers
{
    public class StudentController : Controller
    {
        private readonly IStudentRepo stdRepo;

        public StudentController(IStudentRepo _stdRepo)
        {
            stdRepo = _stdRepo;
        }

        public IActionResult Index(int id)
        {
            ViewBag.StudentId = id;
            Student std = stdRepo.GetStudentByID(id);
            return View(std);
        }

        public IActionResult ShowPermissions(int id)
        {
            ViewBag.Student = stdRepo.GetStudentByID(id);
            ViewBag.StudentId = id;
            var permissions = stdRepo.GetStudentPermissions(id);
            return View(permissions);
        }
        [HttpGet]
        public IActionResult RequestPermission(int id)
        {
            ViewBag.StudentId = id;
            ViewBag.Student = stdRepo.GetStudentByID(id);
            return View(new Permission());
        }

        [HttpPost]
        public IActionResult RequestPermission(Permission permission)
        {
            stdRepo.RequestPermission(permission);
            ViewBag.Message = "Permission Requested Successfully";
            return RedirectToAction("ShowPermissions", new { id = permission.StdID });
        }

        public IActionResult DeletePermission(int permissionId,int stdId) 
        {
            stdRepo.DeletePermission(permissionId);
            ViewBag.Message = "Permission Deleted Successfully";
            return RedirectToAction("ShowPermissions", new { id = stdId });
        }
       

      

        
    }
}
