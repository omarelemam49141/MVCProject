using Microsoft.EntityFrameworkCore;
using MVCProject.Data;
using MVCProject.Models;

namespace MVCProject.Repos
{
	public interface IDailyAttendanceRepo
	{
		public void AddRecordAttendance(DailyAttendanceRecord record);
        public List<DailyAttendanceRecord> getTodayRecords();
		public List<DailyAttendanceRecord> getRecordsByDate(DateOnly date, List<int> stds);


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
			
			db.DailyAttendanceRecords.Update(record);
			db.SaveChanges();
		}
		public List<DailyAttendanceRecord> getTodayRecords()
		{
            return db.DailyAttendanceRecords.Where(r => r.Date == DateOnly.FromDateTime(DateTime.Now.Date)).ToList();
        }
		public List<DailyAttendanceRecord> getRecordsByDate(DateOnly date , List<int> stds)
		{

			return db.DailyAttendanceRecords.Include(s=>s.Student).Where(r => r.Date == date && stds.Contains(r.StdID)).ToList();
		}
    }
}
