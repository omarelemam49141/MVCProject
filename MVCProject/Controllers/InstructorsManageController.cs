using Microsoft.AspNetCore.Mvc;
using MVCProject.Repos;

namespace MVCProject.Controllers
{
    public class InstructorsManageController : Controller
    {
       private ITrackRepo trackRepo;
        public InstructorsManageController(ITrackRepo _trackRepo) {
            trackRepo = _trackRepo;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public bool RemoveTrackAssignedForInstructor(int id)
        {
            
                var track = trackRepo.GetTrackBySupervisorID(id);

                if (track != null)
                {
                    track.SupervisorID = null;

                    trackRepo.UpdateTrack(track);

                    return true;
                }
                else
                {
                    return false;
                }
         
        }

    }
   

}
