using Microsoft.EntityFrameworkCore;
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


        public List<Student> GetAllStudents();

        public Student GetStudentById(int id);

        public List<Student> GetStudentsByIntakeTrack(int IntakeId, int TrackId);

        public bool AddStudent(Student student);

        public bool UpdateStudent(Student student);

        public bool DeleteStudent(int id);

        public bool AddStudentsFromExcel(List<Student> student);

        public int GetInstructorIdByStudentId(int id);

        public int GetTrackIdByStudentId(int id);

        public IEnumerable<DailyAttendanceRecord> GetDailyAttendanceRecordsByStudentId(int id,int numberOfDays,DateOnly startDate);
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

        public bool AddStudent(Student student)
        {
            db.Students.Add(student);
            try
            {
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
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
            if (std != null)
            {
                db.Students.Remove(std);
                try
                {
                    db.SaveChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
            return false;

        }

        public List<Student> GetAllStudents()
        {
            var stds = db.Students.ToList();
            return stds;
        }



        public Student GetStudentById(int id)
        {
            var std = db.Students.FirstOrDefault(s => s.Id == id);
            return std;
        }

        public List<Student> GetStudentsByIntakeTrack(int IntakeId, int TrackId)
        {
            var stds = db.StudentIntakeTracks.Where(I => I.IntakeID == IntakeId && I.TrackID == TrackId).Include(t => t.Student).Select(a => a.Student).ToList();
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
            catch (Exception ex)
            {
                return false;
            }
        }

        public int GetInstructorIdByStudentId(int id)
        {
            try
            {

            var supervisorId = db.StudentIntakeTracks.Include(sit => sit.Track).FirstOrDefault(sit => sit.StdID == id).Track
                .SupervisorID;
            return supervisorId;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public int GetTrackIdByStudentId(int id)
        {
            try
            {
                var trackId = db.StudentIntakeTracks.Where(sit=>sit.StdID == id).OrderByDescending(sit=>sit.IntakeID).FirstOrDefault().TrackID;
                return trackId;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public IEnumerable<DailyAttendanceRecord> GetDailyAttendanceRecordsByStudentId(int id, int numberOfDays, DateOnly startDate)
        {
            return db.DailyAttendanceRecords.Where(d => d.StdID == id && d.Date <= startDate).OrderByDescending(d=>d.Date).Take(numberOfDays);
        }

        bool IStudentRepo.AddStudentsFromExcel(List<Student> student)
        {
            throw new NotImplementedException();
        }
    }
}
