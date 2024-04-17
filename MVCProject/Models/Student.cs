
﻿using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

﻿using System.ComponentModel.DataAnnotations;

namespace MVCProject.Models
{
    public class Student
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 50 characters")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Name must contain only letters")]
        [Remote("ValidateName", "StudentsManage", AdditionalFields = "Id")]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        [Remote("IsEmailAvailable", "Student",ErrorMessage = "Email is already in use")]
        public string Email { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "Password must be between 6 and 50 characters")]
        public string Password { get; set; }
        [Required]
        //012||011||010||015 + 8 digits
        [RegularExpression(@"^(012|011|010|015)\d{8}$", ErrorMessage = "Mobile must be a valid Egyptian mobile number")] // "Mobile must be a valid Egyptian mobile number
        public string Mobile { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "University must be between 3 and 50 characters")]
        public string University { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Faculty must be between 3 and 50 characters")]
        public string Faculty { get; set; }
        [Required]

        public string Specialization { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Graduation Year")]
        public DateTime GraduationYear { get; set; }
        [Remote("checkStudentDegree", "student", ErrorMessage = "Student degree must be between 0 and 250")]
        [DefaultValue(250)]
        public int StudentDegree { get; set; } = 250;
        public ICollection<DailyAttendanceRecord> AttendaceRecords { get; set; } = new HashSet<DailyAttendanceRecord>();
        public List<StudentIntakeTrack> StudentIntakeTracks { get; set; } = new List<StudentIntakeTrack>();
        public ICollection<Permission> Permissions { get; set; } = new HashSet<Permission>();
        public ICollection<StudentMessage> StudentMessages { get; set; } = new HashSet<StudentMessage>();
    }
}
