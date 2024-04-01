namespace MVCProject.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; } 
        public string Password { get; set; }
        public string Mobile { get; set; }
        public string University { get; set; }
        public string Faculty { get; set; }
        public string Specialization { get; set; }
        public DateTime GraduationYear { get; set; }
        public ICollection<DailyAttendanceRecord> AttendaceRecords { get; set; } = new HashSet<DailyAttendanceRecord>();
        public List<StudentIntakeTrack> StudentIntakeTracks { get; set; } = new List<StudentIntakeTrack>();
        public ICollection<Permission> Permissions { get; set; } = new HashSet<Permission>();
        public ICollection<StudentMessage> StudentMessages { get; set; } = new HashSet<StudentMessage>();
    }
}
