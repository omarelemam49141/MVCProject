using MVCProject.Data;
using MVCProject.Models;

namespace MVCProject.Repos
{
	public interface IDailyAttendanceRepo
	{
		public void AddRecordAttendance(DailyAttendanceRecord record);
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
	}
}
