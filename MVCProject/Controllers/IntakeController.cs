﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCProject.Data;
using MVCProject.Models;
using MVCProject.Repos;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MVCProject.Controllers
{
    [Authorize(Roles = "admin")]
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

            return intakeRepo.GetAllIntakes().FirstOrDefault(a => a.Name == name && a.Id != Id) == null;
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
            int progid = intakeRepo.GetIntakeById(intake).ProgramId;
            return RedirectToAction("ViewTracks",new {intake , id = progid});
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            try
            {
                intakeRepo.RemoveIntake(id);
                var intakes = intakeRepo.GetAllIntakes();
                return View("index", intakes);
            }
            catch(Exception ex)
            {

                ViewBag.reasons = new List<string>()
                {
                    "This Intake Already has Some Records So this will Affect the Other tables"
                };
                return View("AdminError");
            }
         
        }
        public IActionResult intakeTracksJson(int id)
        {
            var options = new JsonSerializerOptions()
            {
                ReferenceHandler = ReferenceHandler.IgnoreCycles,
            };
            return Json(intakeRepo.GetIntakeById(id).Tracks.Where(t=>t.Status == "Active"),options);
        }
    }
}
