using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVCProject.Models;
using MVCProject.Repos;

namespace MVCProject.Controllers
{
    [Authorize(Roles = "admin")]
    public class InstructorsManageController : Controller
    {
       private ITrackRepo trackRepo;
       private IInstructorRepo instructorRepo;
       private IDepartmentRepo departmentRepo;
       private IIntakeRepo intakeRepo;
        private IAllDBEmails allDBEmails;
        public InstructorsManageController(ITrackRepo _trackRepo , IInstructorRepo _instructorRepo , IDepartmentRepo _departmentRepo , IIntakeRepo _intakeRepo , IAllDBEmails _allDBEmails) {
            trackRepo = _trackRepo;
            instructorRepo = _instructorRepo;
            departmentRepo = _departmentRepo;
            intakeRepo = _intakeRepo;
            allDBEmails = _allDBEmails;
        }
        public IActionResult Index()
        {
            var instructors = instructorRepo.GetAll();

            return View(instructors);
        }
        public IActionResult Add()
        {
            ViewBag.Departments = departmentRepo.GetAllDepartments();
            ViewBag.Tracks = trackRepo.GetAll();
            ViewBag.Intakes = intakeRepo.GetAllIntakes();
            return View();
        }
        [HttpPost]
        public IActionResult Add(Instructor instructor) {
        
        instructorRepo.AddInstructor(instructor);
            return RedirectToAction("Index");
        }
        [Route("InstructorsManage/ValidateNotSupervisedTrack/{tid}")]
        public bool ValidateNotSupervisedTrack(int tid) {
        
            var track = trackRepo.GetTrackById(tid);
            if(track.Supervisor != null)
            {
                return false;
            }
            return true;


        }
        [Route("InstructorsManage/AssignTrackToInstructor/{track}/{ins}")]
        public bool AssignTrackToInstructor(int track , int ins)
        {
           return trackRepo.AssignTrackSuperViser(track, ins);
            
        }
        [HttpGet]
        public bool RemoveTrackAssignedForInstructor(int id)
        {
            
                var track = trackRepo.GetTrackBySupervisorID(id);

                if (track != null)
                {
                    track.SupervisorForeignKeyID = null;

                    trackRepo.UpdateTrack(track);

                    return true;
                }
                else
                {
                    return false;
                }
         
        }
        [HttpPost]
        public IActionResult Edit(Instructor instructor)
        {
            instructorRepo.UpdateInstructor(0,instructor);
            return RedirectToAction("Index");
        }
        public IActionResult Edit(int id)
        {
           var instructor =  instructorRepo.GetInstructorByID(id);
            ViewBag.Departments = departmentRepo.GetAllDepartments();
            ViewBag.Tracks = trackRepo.GetAll();
            ViewBag.Intakes = intakeRepo.GetAllIntakes();
            return View("Add",instructor);
        }
        [HttpGet]
        public bool ValidateName(string Name, int id)
        {

            return instructorRepo.GetAll().FirstOrDefault(a => a.Name == Name && a.Id != id) == null;
        }
        public bool ValidateEmail(string email, int id)
        {
            email = Uri.UnescapeDataString(email);


            var emails = allDBEmails.GetEmails();

            var s = emails.FirstOrDefault(s => s.email == email);
            if(s == null)
            {
                return true;
            }else
            {
                var student = instructorRepo.GetAll().FirstOrDefault(s=>s.Email == email);
                if(student.Id == id)
                    return true;
                else
                    return false;

            }
        }


    }


}
