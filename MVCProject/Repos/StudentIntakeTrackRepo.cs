using Microsoft.EntityFrameworkCore;
using MVCProject.Data;
using MVCProject.Models;

namespace MVCProject.Repos
{
    public interface IStudentIntakeTrackRepo
    {
        List<StudentIntakeTrack> GetByTrackAndIntakeIDs(int trackID, int intakeID);
    }
    public class StudentIntakeTrackRepo: IStudentIntakeTrackRepo
    {
        private attendanceDBContext db;

        public StudentIntakeTrackRepo(attendanceDBContext _db) 
        {
            db = _db;
        }

        public List<StudentIntakeTrack> GetByTrackAndIntakeIDs(int trackID, int intakeID)
        {
            return db
                    .StudentIntakeTracks
                    .Include(sit=>sit.Student)
                    .Where(sit=>sit.IntakeID==intakeID && sit.TrackID==trackID)
                    .ToList();
        }
    }
}
