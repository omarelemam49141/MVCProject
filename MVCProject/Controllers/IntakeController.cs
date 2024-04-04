using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCProject.Data;
using MVCProject.Models;
using MVCProject.Repos;

namespace MVCProject.Controllers
{
    public class IntakeController : Controller
    {
        private  IIntakeRepo intakeRepo { get; set; }
        private ITrackRepo trackRepo { get; set; }
        private readonly attendanceDBContext ctx;
       
        public IntakeController(IIntakeRepo _intakeRepo,attendanceDBContext _ctx , ITrackRepo _trackRepo) 
        {
            intakeRepo = _intakeRepo;
            trackRepo = _trackRepo;
            ctx = _ctx;

        }
        public IActionResult Index()
        {
            var intakes = intakeRepo.GetAllIntakes();
            return View(intakes);
        }
        public IActionResult Add() {
            ViewBag.programs =  ctx.Programs.ToList();
            ViewBag.tracks = trackRepo.GetAll();
            return View();
        }
        [HttpPost]
        public IActionResult Add(Intake intake) {

            intakeRepo.AddIntake(intake);
            var intakes = intakeRepo.GetAllIntakes();
            return View("index",intakes);
        
        }
        [HttpGet]
        public IActionResult Edit(int id) {
            ViewBag.programs = ctx.Programs.ToList();
            var intake =  intakeRepo.GetIntakeById(id);
            ViewBag.tracks = trackRepo.GetAll();
            return View("Add" , intake);
        }
        [HttpPost]
        public IActionResult Edit(Intake intake)
        {
            intakeRepo.UpdateIntake(intake);
            var intakes = intakeRepo.GetAllIntakes();
            return View("index", intakes);
        }
        [HttpGet]
        public bool ValidateName(string name, int? Id)
        {
            var res = Id == null || intakeRepo.GetAllIntakes().Any(x => x.Name == name && x.Id != Id);
            return !res;
        }
        [HttpGet]
        public bool ValidateYear(string year)
        {
            return true;
        }
        public IActionResult ViewTracks(int id , int intake)
        {
            var tracks = trackRepo.GetTracksForProgram(id);
            var intakeTracks = intakeRepo.GetIntakeById(intake)?.Tracks.ToList(); 
            ViewBag.tracks = intakeTracks;
            ViewBag.intake = intake;
            return View(tracks);
        }
        [HttpPost]
        public IActionResult SubmitTracks(int intake,List<int> SelectedTracks) {
            intakeRepo.AssignTracksToIntake(intake, SelectedTracks);
            return RedirectToAction("ViewTracks");
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            
            intakeRepo.RemoveIntake(id);
            var intakes = intakeRepo.GetAllIntakes();
            return View("index", intakes);
        }
    }
}
