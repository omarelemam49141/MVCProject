using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCProject.Models
{
    public class DailyAttendanceRecord
    {
        public DateOnly Date { get; set; }
        [ForeignKey("Student")]
        public int StdID { get; set; }
        public Student Student { get; set; }
        public TimeOnly TimeOfAttendance { get; set; }
        public TimeOnly TimeOfLeave { get; set; }
        public int StudentDegree { get; set; }
        [RegularExpression("(Attend|Absent)")]
        public string Status { get; set; }
    }
}
