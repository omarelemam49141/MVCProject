using Microsoft.EntityFrameworkCore;
using MVCProject.Data;
using MVCProject.Models;

namespace MVCProject.Repos
{
    public interface IAttendanceRecordRepo
    {
        public List<DailyAttendanceRecord> GetAttendanceRecords(List<Student> students, DateOnly date);
        public void AddAttendanceRecord(DailyAttendanceRecord attendanceRecord);

	}
    public class AttendanceRecordRepo: IAttendanceRecordRepo
    {
        private attendanceDBContext db;

        public AttendanceRecordRepo(attendanceDBContext _db) 
        {
            db = _db;
        }

        public List<DailyAttendanceRecord> GetAttendanceRecords(List<Student> students, DateOnly date)
        {
            List<int> studentsIDs = students.Select(s=>s.Id).ToList();
            return db
                    .DailyAttendanceRecords
                    .Include(ar=>ar.Student)
                    .Where(
                        ar => studentsIDs.Contains(ar.StdID)
                                && ar.Date == date
                     )
                    .ToList();
        }
        public void AddAttendanceRecord(DailyAttendanceRecord attendanceRecord)
        {
			db.DailyAttendanceRecords.Add(attendanceRecord);
			db.SaveChanges();
		}
    }
}
