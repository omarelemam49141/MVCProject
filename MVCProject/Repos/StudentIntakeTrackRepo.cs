using MVCProject.Data;

namespace MVCProject.Repos
{
    public class StudentIntakeTrackRepo
    {
        private attendanceDBContext db;

        public StudentIntakeTrackRepo(attendanceDBContext _db) 
        {
            db = _db;
        }
    }
}
