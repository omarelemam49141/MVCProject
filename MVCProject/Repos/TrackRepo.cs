using MVCProject.Data;
using MVCProject.Models;

namespace MVCProject.Repos
{
    public interface ITrackRepo
    {
        public Track GetTrackBySupervisorID(int supervisorId); 
    }
    public class TrackRepo: ITrackRepo
    {
        private attendanceDBContext db;

        public TrackRepo(attendanceDBContext _db) { db = _db; }

        public Track GetTrackBySupervisorID(int supervisorId)
        {
            return db.Tracks.FirstOrDefault(t=>t.SupervisorID==supervisorId);
        }
    }
}
