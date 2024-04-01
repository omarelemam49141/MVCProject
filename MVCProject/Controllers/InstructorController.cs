using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using MVCProject.Models;
using MVCProject.Repos;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MVCProject.Controllers
{
    public class InstructorController : Controller
    {
        private IInstructorRepo instRepo;
        private IPermissionRepo permissionRepo;
        private IStudentMessageRepo stdMsgRepo;

        public InstructorController(IInstructorRepo _instRepo,
                                    IPermissionRepo _permissionRepo,
                                    IStudentMessageRepo _stdMsgRepo) 
        {
            instRepo = _instRepo; 
            permissionRepo = _permissionRepo;
            stdMsgRepo = _stdMsgRepo;
        }
        public IActionResult Index(int id)
        {
            ViewBag.InstructorId = id;
            Instructor inst = instRepo.GetInstructorByID(id); 
            return View(inst);
        }

        public IActionResult ShowPermissions(int id, string? message)
        {
            ViewBag.instructor = instRepo.GetInstructorByID(id);
            ViewBag.InstructorId = id;
            if (message != null)
            {
                ViewBag.Message = message;
            }
            else
            {
                ViewBag.Message = null;
            }
            var permissions = permissionRepo.GetPermissionsBySupervisorID(id);
            return View(permissions);
        }
        public void DealWithPermission(int permissionId, string permissionStatus)
        {
            //get the permission by the stdId and the date
            Permission p = permissionRepo.GetPermissionByID(permissionId);
            //check if it is not null then
            if (p != null)
            {
                //set its status to be accepted
                permissionRepo.UpdatePermissionStatus(p, permissionStatus);
                //send a message to the student
                string messageContent = $"Your permission for day {p.Date} has been {permissionStatus}";
                stdMsgRepo.CreateNewMessage(p.StdID, DateTime.Now, messageContent);
            }
        }
        public IActionResult AcceptPermission(int permissionId, int id)
        {
            DealWithPermission(permissionId, "Accepted");
            //redirect to the showPermissions
            string acceptedMessage = "Permission has been accepted!";
            return RedirectToAction("showPermissions", "instructor", new {id=id, message= acceptedMessage });
        }

        public IActionResult RefusePermission(int permissionId, int id)
        {
            DealWithPermission(permissionId, "Denied");
            //redirect to the showPermissions
            string denialMessage = "Permission has been denied!";
            return RedirectToAction("showPermissions", "instructor", new { id = id, message= denialMessage });
        }
    }
}
