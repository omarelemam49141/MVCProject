using Microsoft.EntityFrameworkCore;
using MVCProject.Data;
using MVCProject.Models;

namespace MVCProject.Repos
{
    public interface IScheduleRepo
    {
        public List<Schedule> GetTrackSchedules(int trackID);
    }
    public class ScheduleRepo: IScheduleRepo
    {
        private attendanceDBContext db;

        public ScheduleRepo(attendanceDBContext _db) { db = _db; }

        public List<Schedule> GetTrackSchedules(int trackID)
        {
            return db.Schedules.Where(s=>s.TrackID==trackID).ToList();
        }
    }
}
