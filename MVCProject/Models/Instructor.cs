using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCProject.Models
{
    public class Instructor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Mobile { get; set; }
        [ForeignKey("Department")]
        [Display(Name="Department")]
        public int DeptID { get; set; }
        public Department Department { get; set; }
        public ICollection<Permission> Permissions { get; set; } = new HashSet<Permission>();
        public Track TrackSupervised { get; set; }
        [ForeignKey("InstructorTrack")]
        [Display(Name = "Track")]
        public int TrackID { get; set; }
        public Track InstructorTrack { get; set; }
        [ForeignKey("InstructorIntake")]
        [Display(Name = "Intake")]
        public int IntakeID { get; set; }
        public Intake InstructorIntake { get; set; }
    }
}
