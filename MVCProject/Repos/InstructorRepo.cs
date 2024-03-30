using MVCProject.Data;
using MVCProject.Models;

namespace MVCProject.Repos
{
    public interface  IInstructorRepo
    {
        public Instructor GetInstructorByEmailAndPassword(string email, string password);
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
    }
}
