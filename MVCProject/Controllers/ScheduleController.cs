using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVCProject.Models;
using MVCProject.Repos;

namespace MVCProject.Controllers
{
    [Authorize(Roles = "supervisor")]
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

        [HttpGet]
        public IActionResult Create(int? instID)
        {
            if (instID == null)
                return BadRequest();
            ViewBag.instructor = instRepo.GetInstructorByID(instID.Value);
            ViewBag.InstructorId = instID;
            //get the track of this supervisor
            Track supervisorTrack = trackRepo.GetTrackBySupervisorID(instID.Value);
            ViewBag.trackName = supervisorTrack?.Name;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(int? instID, Schedule schedule)
        {
            if (instID == null)
                return BadRequest();
            ViewBag.instructor = instRepo.GetInstructorByID(instID.Value);
            ViewBag.InstructorId = instID;
            //get the track of this supervisor
            Track supervisorTrack = trackRepo.GetTrackBySupervisorID(instID.Value);
            ViewBag.trackName = supervisorTrack?.Name;
            if(!ModelState.IsValid)
            {
                return View(schedule);
            }
            else
            {
                //make sure there is no schedule with the same date
                Schedule duplicatedSchedule = scheduleRepo.GetScheduleByDate(schedule.Date);
                if (duplicatedSchedule != null)
                {
                    ModelState.AddModelError("", "A schedule with the same date already exists");
                    return View(schedule);
                }
                //add the new schedule and return to the index
                schedule.TrackID = supervisorTrack.Id;
                scheduleRepo.CreateSchedule(schedule);
                return RedirectToAction("Index", new { id = instID});
            }
        }

        public IActionResult Delete(int? id, int? instID)
        {
            if (instID == null || id == null)
                return BadRequest();
            ViewBag.instructor = instRepo.GetInstructorByID(instID.Value);
            ViewBag.InstructorId = instID;
            //get the track of this supervisor
            Track supervisorTrack = trackRepo.GetTrackBySupervisorID(instID.Value);
            ViewBag.trackName = supervisorTrack?.Name;

            Schedule scheduleToDelete = scheduleRepo.GetScheduleByID(id.Value);
            if (scheduleToDelete == null) return NotFound();
            
            scheduleRepo.DeleteSchedule(scheduleToDelete);

            return RedirectToAction("index", new { id = instID });
        }

        [HttpGet]
        public IActionResult Edit(int? id, int? instID)
        {
            if (instID == null || id == null)
                return BadRequest();
            ViewBag.instructor = instRepo.GetInstructorByID(instID.Value);
            ViewBag.InstructorId = instID;
            //get the track of this supervisor
            Track supervisorTrack = trackRepo.GetTrackBySupervisorID(instID.Value);
            ViewBag.trackName = supervisorTrack?.Name;

            Schedule scheduleToEdit = scheduleRepo.GetScheduleByID(id.Value);
            if (scheduleToEdit == null) return NotFound();

            return View(scheduleToEdit);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int? instID, Schedule schedule)
        {
            if (instID == null)
                return BadRequest();
            ViewBag.instructor = instRepo.GetInstructorByID(instID.Value);
            ViewBag.InstructorId = instID;
            //get the track of this supervisor
            Track supervisorTrack = trackRepo.GetTrackBySupervisorID(instID.Value);
            ViewBag.trackName = supervisorTrack?.Name;

            schedule.TrackID = supervisorTrack.Id;
            scheduleRepo.UpdateSchedule(schedule);
            ViewBag.SuccessMessage = "Schedulae time updated successfully!";

            return View(schedule);
        }
    }
}