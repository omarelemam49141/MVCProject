using MVCProject.Data;
using MVCProject.Models;

namespace MVCProject.Repos
{
    public interface IStudentRepo
    {
        public Student GetStudentByEmailAndPassword(string email, string password);
    }

    public class StudentRepo: IStudentRepo
    {
        private attendanceDBContext db;

        public StudentRepo(attendanceDBContext _db) { db = _db; }

        public Student GetStudentByEmailAndPassword(string email, string password)
        {
            return db.Students.FirstOrDefault(i => i.Email.ToLower() == email.ToLower()
                                            && i.Password == password);
        }
    }
}
