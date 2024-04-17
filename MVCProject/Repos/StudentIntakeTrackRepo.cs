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
	}
    public class StudentIntakeTrackRepo : IStudentIntakeTrackRepo
    {
        private attendanceDBContext db;

        public StudentIntakeTrackRepo(attendanceDBContext _db) 
        {
            db = _db;
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
