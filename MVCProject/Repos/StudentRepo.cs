using MVCProject.Data;
using MVCProject.Models;

namespace MVCProject.Repos
{
    public interface IStudentRepo
    {
        public Student GetStudentByEmailAndPassword(string email, string password);
        public void RegisterStudent(Student std);

        public Student GetStudentByID(int id);

        public IEnumerable<Permission> GetStudentPermissions(int id);

        public void RequestPermission(Permission permission);

        public void DeletePermission(int permissionId);

        public bool StudentExists(int id);
    }

    public class StudentRepo : IStudentRepo
    {
        private attendanceDBContext db;

        public StudentRepo(attendanceDBContext _db) { db = _db; }

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
                throw new Exception("Error in saving student");
            }
        }

        public Student GetStudentByID(int id)
        {
            return db.Students.Find(id);
        }

        public IEnumerable<Permission> GetStudentPermissions(int id)
        {
            return db.Permissions.Where(p => p.StdID == id).OrderByDescending(p => p.Id).ToList();
        }


        public void RequestPermission(Permission permission)
        {

            try
            {
                db.Permissions.Add(permission);
                db.SaveChanges();


            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }



        public void DeletePermission(int permissionId)
        {
            try
            {
                Permission permission = db.Permissions.Find(permissionId);
                db.Permissions.Remove(permission);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public bool StudentExists(int id) { return db.Students.Any(e => e.Id == id); }
    }
}
