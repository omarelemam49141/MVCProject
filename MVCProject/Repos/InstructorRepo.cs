using MVCProject.Data;
using MVCProject.Models;

namespace MVCProject.Repos
{
    public interface  IInstructorRepo
    {
        public Instructor GetInstructorByEmailAndPassword(string email, string password);
        public Instructor GetInstructorByID(int id);
        public Track GetSuperVisorTrack(int id);
    }

    public class InstructorRepo: IInstructorRepo
    {
        private attendanceDBContext db;

        public InstructorRepo(attendanceDBContext _db) 
        {
            db = _db;
        }

        public Instructor GetInstructorByEmailAndPassword(string email, string password)
        {
            return db.Instructors.FirstOrDefault(i=>i.Email.ToLower() == email.ToLower() 
                                            && i.Password == password);
        }

        public Instructor GetInstructorByID(int id)
        {
            return db.Instructors.FirstOrDefault(i => i.Id == id);
        }

        //Return the track if the instructor is a supervisor
        public Track GetSuperVisorTrack(int id)
        {
            return db.Tracks.FirstOrDefault(t => t.SupervisorID == id);
        }
    }
}
