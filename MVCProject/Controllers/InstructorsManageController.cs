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
        public InstructorsManageController(ITrackRepo _trackRepo , IInstructorRepo _instructorRepo , IDepartmentRepo _departmentRepo , IIntakeRepo _intakeRepo) {
            trackRepo = _trackRepo;
            instructorRepo = _instructorRepo;
            departmentRepo = _departmentRepo;
            intakeRepo = _intakeRepo;

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

    }
   

}
