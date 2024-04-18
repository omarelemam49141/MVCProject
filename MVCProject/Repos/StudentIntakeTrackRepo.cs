using Microsoft.EntityFrameworkCore;
using MVCProject.Data;
using MVCProject.Models;

namespace MVCProject.Repos
{

   public interface IStudentIntakeTrackRepo
    {
        //get all student in the table studentIntakeTrack
        public List<StudentIntakeTrack> getAllStudents();
        void AddStudentIntakeTrack(int stdID, int IntakeID, int TrackID);
        List<StudentIntakeTrack> GetByTrackAndIntakeIDs(int trackID, int intakeID);
	}
    public class StudentIntakeTrackRepo : IStudentIntakeTrackRepo
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

        public void AddStudentIntakeTrack(int _StdID, int _IntakeID, int _TrackID)
        {
            db.StudentIntakeTracks.Add(new StudentIntakeTrack { StdID = _StdID, IntakeID = _IntakeID, TrackID = _TrackID });
            db.SaveChanges();
        }

        public List<StudentIntakeTrack> getAllStudents()
        {
            return db.StudentIntakeTracks.Include(s => s.Student).ToList();
		}
    }
}
