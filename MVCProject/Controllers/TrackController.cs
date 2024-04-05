using Microsoft.AspNetCore.Mvc;
using MVCProject.Data;
using MVCProject.Models;
using MVCProject.Repos;

namespace MVCProject.Controllers
{
    public class TrackController : Controller
    {
        private ITrackRepo trackrepo;
        private attendanceDBContext ctx;
        private IInstructorRepo instructorRepo;
        public TrackController(ITrackRepo _trackrepo,IInstructorRepo _instructorRepo, attendanceDBContext _ctx)
        {
            trackrepo = _trackrepo;
              ctx = _ctx;
            instructorRepo = _instructorRepo;

        }
        public IActionResult Index()
        {
            var tracks = trackrepo.GetAll();
          
            return View(tracks);
        }
        public IActionResult Add()
        {
            ViewBag.programs = ctx.Programs.ToList();
            ViewBag.instructors = instructorRepo.GetAll();
            return View();
        }
        [HttpPost]
        public IActionResult Add(Track track) {
            var t = trackrepo.GetTrackBySupervisorID((int)track.SupervisorID);
            
            trackrepo.AddTrack(track);
            return RedirectToAction("Index");

        }
        public IActionResult Activate(int id)
        {
            var track = trackrepo.GetTrackById(id);
            if (track == null)
            {
                return NotFound();
            }

            track.Status = "Active";
            trackrepo.UpdateTrack(track);

            return RedirectToAction("Index");
        }
        public IActionResult Deactivate(int id)
        {
            var track = trackrepo.GetTrackById(id);
            if (track == null)
            {
                return NotFound();
            }

            track.Status = "Inactive";
            trackrepo.UpdateTrack(track);

            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var track = trackrepo.GetTrackById(id);
            if (track == null)
            {
                return NotFound();
            }
            ViewBag.programs = ctx.Programs.ToList();
            ViewBag.instructors = instructorRepo.GetAll();

            return View("Add",track);
        }

        [HttpPost]
        public IActionResult Edit(Track track)
        {
          
            trackrepo.UpdateTrack(track);

            return RedirectToAction("Index");
        }

        public bool ValidateInstructor(int SupervisorID, int Id)
        {
            var existingTrack = trackrepo.GetTrackBySupervisorID(SupervisorID);


            return existingTrack == null || existingTrack.Id == Id;
        }
    }
}
