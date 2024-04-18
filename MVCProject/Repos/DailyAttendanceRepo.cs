using MVCProject.Data;
using MVCProject.Models;

namespace MVCProject.Repos
{
	public interface IDailyAttendanceRepo
	{
		public void AddRecordAttendance(DailyAttendanceRecord record);
        public List<DailyAttendanceRecord> getTodayRecords();

    }
	public class DailyAttendanceRepo : IDailyAttendanceRepo
	{
		private attendanceDBContext db;
		public DailyAttendanceRepo(attendanceDBContext _db)
		{
			db = _db;
		}
		public void AddRecordAttendance(DailyAttendanceRecord record)
		{
			
			db.DailyAttendanceRecords.Add(record);
			db.SaveChanges();
		}
		public List<DailyAttendanceRecord> getTodayRecords()
		{
            return db.DailyAttendanceRecords.Where(r => r.Date == DateOnly.FromDateTime(DateTime.Now.Date)).ToList();
        }
    }
}
