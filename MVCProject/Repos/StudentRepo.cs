using Microsoft.EntityFrameworkCore;
using MVCProject.Data;
using MVCProject.Models;

namespace MVCProject.Repos
{
    public interface IStudentRepo
    {
        public Student GetStudentByEmailAndPassword(string email, string password);

        public void RegisterStudent(Student std);

        public List<Student> GetAllStudents();

        public Student GetStudentById(int id);

        public List<Student> GetStudentsByIntakeTrack(int IntakeId, int TrackId);

        public bool AddStudent(Student student);

        public bool UpdateStudent(Student student);

        public bool DeleteStudent(int id);

        public bool AddStudentsFromExcel(List<Student> student);
    }

    public class StudentRepo : IStudentRepo
    {
        private attendanceDBContext db;

        public StudentRepo(attendanceDBContext _db) { db = _db; }

        public bool AddStudent(Student student)
        {
            db.Students.Add(student);
            try
            {
                db.SaveChanges();
                return true;
            }catch (Exception ex)
            {
                return false;
            }
          
        }

        public void AddStudentsFromExcel(List<Student> student)
        {
            throw new NotImplementedException();
        }

        public bool DeleteStudent(int id)
        {
            var std = db.Students.FirstOrDefault(s => s.Id == id);
                if(std != null)
                {
                         db.Students.Remove(std);
                        try
                        {
                            db.SaveChanges();
                            return true;
                        }catch(Exception ex)
                        {
                            return false;
                        }
                }
                return false;
                    
        }

        public List<Student> GetAllStudents()
        {
            var stds =db.Students.ToList();
            return stds;
        }

        public Student GetStudentByEmailAndPassword(string email, string password)
        {
            return db.Students.FirstOrDefault(i => i.Email.ToLower() == email.ToLower()
                                            && i.Password == password);
        }
        public void RegisterStudent(Student std)
        {
            db.Students.Add(std);
            db.SaveChanges();
            if (std.Id == 0)
            {
                throw new System.Exception("Error in saving student");
            }
        }

        public Student GetStudentById(int id)
        {
            var std = db.Students.FirstOrDefault(s => s.Id == id);
            return std;
        }

        public List<Student> GetStudentsByIntakeTrack(int IntakeId, int TrackId)
        {
            var stds = db.StudentIntakeTracks.Where(I => I.IntakeID == IntakeId && I.TrackID == TrackId).Include(t => t.Student).Select(a=>a.Student). ToList();
            return stds;
        }

        public bool UpdateStudent(Student student)
        {
            db.Students.Update(student);
            try
            {
                db.SaveChanges();
                return true;
            }
            catch(Exception ex) 
            {
                return false;
            }
        }

        bool IStudentRepo.AddStudentsFromExcel(List<Student> student)
        {
            throw new NotImplementedException();
        }
    }
}
