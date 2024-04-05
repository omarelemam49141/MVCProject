using Microsoft.EntityFrameworkCore;
using MVCProject.Data;
using MVCProject.Models;
using System.Linq;

namespace MVCProject.Repos
{
    public interface  IInstructorRepo
    {
        public Instructor GetInstructorByEmailAndPassword(string email, string password);
        public Instructor GetInstructorByID(int id);
        public Instructor GetInstructorByIDWithTrackIntakeDept(int id);
        public Track GetSuperVisorTrack(int id);
        public List<Instructor> GetAll();
        public void AddInstructor(Instructor instructor);

        public void DeleteInstructor(int id);

        public void UpdateInstructor(int id, Instructor instructor);

        public void AssignTrackToInstructor(int id, int trackid);
    }

    public class InstructorRepo: IInstructorRepo
    {
        private attendanceDBContext db;

        public InstructorRepo(attendanceDBContext _db) 
        {
            db = _db;
        }

        public void AddInstructor(Instructor instructor)
        {
            db.Instructors.Add(instructor);
            db.SaveChanges();
        }

        public void AssignTrackToInstructor(int id, int trackid)
        {
            var ins = db.Instructors.FirstOrDefault(i => i.Id == id);
            var track =  db.Tracks.FirstOrDefault(t=>t.Id == trackid);
            track.Supervisor = ins;
            db.SaveChanges();
        }

        public void DeleteInstructor(int id)
        {
            var ins = db.Instructors.FirstOrDefault(i => i.Id == id);
            db.Instructors.Remove(ins);
            db.SaveChanges();
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

        public void UpdateInstructor(int id, Instructor instructor)
        {
            instructor.Id = id;
            db.Instructors.Update(instructor);
            db.SaveChanges();
        }
        public List<Instructor> GetAll()
        {
            return db.Instructors.Include(p=>p.TrackSupervised).ToList();   
        }

        public Instructor GetInstructorByIDWithTrackIntakeDept(int id)
        {
            return db
                    .Instructors
                    .Include(i=>i.InstructorTrack)
                    .Include(i=>i.Department)
                    .Include(i=>i.InstructorIntake)
                    .FirstOrDefault(i=>i.Id==id);
        }
    }
}
