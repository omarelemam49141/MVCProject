using Microsoft.AspNetCore.Mvc;
using MVCProject.Models;
using MVCProject.Repos;

namespace MVCProject.Controllers
{
    public class ScheduleController : Controller
    {
        private ITrackRepo trackRepo;
        private IScheduleRepo scheduleRepo;
        private IInstructorRepo instRepo;

        public ScheduleController(ITrackRepo _trackRepo,
                                  IScheduleRepo _scheduleRepo,
                                  IInstructorRepo _instRepo) 
        {
            trackRepo = _trackRepo;
            scheduleRepo = _scheduleRepo;
            instRepo = _instRepo;
        }
        public IActionResult Index(int? id)
        {
            if (id == null)
                return BadRequest();
            ViewBag.instructor = instRepo.GetInstructorByID(id.Value);
            ViewBag.InstructorId = id;
            //get the track of this supervisor
            Track supervisorTrack = trackRepo.GetTrackBySupervisorID(id.Value);
            //get all schedules for this track
            if (supervisorTrack == null)
                return NotFound();
            else
            {
                //send the shedules to display them in the view
                List<Schedule> trackSchedules = scheduleRepo.GetTrackSchedules(supervisorTrack.Id);
                ViewBag.trackName = supervisorTrack.Name;
                return View(trackSchedules);
            }
        }

        public IActionResult Create(int? instID)
        {
            if (instID == null)
                return BadRequest();
            ViewBag.instructor = instRepo.GetInstructorByID(instID.Value);
            ViewBag.InstructorId = instID;
            return View();
        }
    }
}
