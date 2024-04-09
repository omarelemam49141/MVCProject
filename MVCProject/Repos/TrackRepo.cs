using Microsoft.EntityFrameworkCore;
using MVCProject.Data;
using MVCProject.Models;

namespace MVCProject.Repos
{
    public interface ITrackRepo
    {
        public void AddTrack(Track t);
        public void RemoveTrack(int tid);
        public Track DeactivateTrack(int tid);
        public void UpdateTrack(Track t);
        public Track GetTrackById(int tid);
        public Track GetTrackBySupervisorID(int supervisorId);
        public Track GetTrackBySupervisor(int sid);
        public List<Track> GetTracksForProgram(int pid);
        public bool AssignTrackSuperViser(int tid, int insid);
        public List<Track> GetAll();

        

    }
    public class TrackRepo : ITrackRepo
    {
        private attendanceDBContext db;
        public TrackRepo(attendanceDBContext _db)
        {
            db = _db;
        }
        public void RemoveTrack(int trackId)
        {
            var trackToRemove = db.Tracks.Find(trackId);
            if (trackToRemove != null)
            {
                db.Tracks.Remove(trackToRemove);
                db.SaveChanges();
            }
        }



        public void AddTrack(Track track)
        {
            db.Tracks.Add(track);
            db.SaveChanges();
        }

        public Track DeactivateTrack(int trackId)
        {
            var track = db.Tracks.FirstOrDefault(t => t.Id == trackId);
            if (track != null)
            {
                track.Status = "Inactive";
                db.SaveChanges();
            }
            return track;
        }

        public Track GetTrackById(int trackId)
        {
            return db.Tracks.Include(s=>s.Supervisor).FirstOrDefault(t => t.Id == trackId);
        }


        public Track GetTrackBySupervisor(int supervisorId)
        {
            return db.Tracks.Include(p => p.Program).FirstOrDefault(t => t.SupervisorID == supervisorId);
        }


        public void UpdateTrack(Track track)
        {
            db.Tracks.Update(track);
            db.SaveChanges();

        }


        public Track GetTrackBySupervisorID(int supervisorId)
        {
            return db.Tracks.Include(p=>p.Program).FirstOrDefault(t => t.SupervisorID == supervisorId);

        }

        public List<Track> GetAll()
        {
            return db.Tracks.Include(p=>p.Program).ToList();
        }

        public List<Track> GetTracksForProgram(int pid)
        {
            return db.Tracks.Include(p=>p.Program).Where(t => t.programID == pid && t.Status =="Active").ToList();
        }
        public bool AssignTrackSuperViser(int tid, int insid)
        {
            try
            {
                GetTrackById(tid).SupervisorID = insid;
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
