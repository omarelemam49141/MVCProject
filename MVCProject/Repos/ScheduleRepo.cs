using Microsoft.EntityFrameworkCore;
using MVCProject.Data;
using MVCProject.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MVCProject.Repos
{
    public interface IScheduleRepo
    {
        public List<Schedule> GetTrackSchedules(int trackID);
        public void CreateSchedule(Schedule schedule);
        public Schedule GetScheduleByDate(DateOnly date);
        public Schedule GetScheduleByID(int id);
        public void DeleteSchedule(Schedule schedule);
        public void UpdateSchedule(Schedule schedule);

        public IEnumerable<Schedule> GetSchedulesByDateAndTrack(DateOnly date, int trackId);
    }
    public class ScheduleRepo : IScheduleRepo
    {
        private attendanceDBContext db;

        public ScheduleRepo(attendanceDBContext _db) { db = _db; }

        public void CreateSchedule(Schedule schedule)
        {
            db.Schedules.Add(schedule);
            db.SaveChanges();
        }

        public void DeleteSchedule(Schedule schedule)
        {
            db.Schedules.Remove(schedule);
            db.SaveChanges();
        }

        public Schedule GetScheduleByDate(DateOnly date)
        {
            return db.Schedules.FirstOrDefault(s => s.Date == date);
        }

        public Schedule GetScheduleByID(int id)
        {
            return db.Schedules.FirstOrDefault(s => s.id == id);
        }

        public List<Schedule> GetTrackSchedules(int trackID)
        {
            return db.Schedules.Where(s => s.TrackID == trackID).ToList();
        }

        public void UpdateSchedule(Schedule schedule)
        {
            db.Schedules.Update(schedule);
            db.SaveChanges();
        }

        public IEnumerable<Schedule> GetSchedulesByDateAndTrack(DateOnly date, int trackId)
        {
            return db.Schedules.Where(s => s.TrackID == trackId && s.Date >= date).Take(7);
        }
    }
}
