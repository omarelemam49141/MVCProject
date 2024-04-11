using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCProject.Models
{
    public class Instructor
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required")]
        [StringLength(50, ErrorMessage = "Name must be between 1 and 50 characters", MinimumLength = 1)]
        [Remote("ValidateName" , "InstructorsManage" , AdditionalFields = "Id")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; }
        [StringLength(50, ErrorMessage = "Password must be between 8 and 50 characters", MinimumLength = 8)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$", ErrorMessage = "Password must contain at least one uppercase letter, one lowercase letter, one digit, and one special character")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Mobile is required")]
        [RegularExpression(@"^(010|011|012|015)\d{8}$", ErrorMessage = "Invalid mobile number format")]
        public string Mobile { get; set; }
        [ForeignKey("Department")]
        [Required(ErrorMessage = "Department ID is required")]
        public int DeptID { get; set; }
        public Department Department { get; set; }
        public ICollection<Permission> Permissions { get; set; } = new HashSet<Permission>();
        public Track TrackSupervised { get; set; }
        [ForeignKey("InstructorTrack")]
        [Required(ErrorMessage = "Track ID is required")]

        public int TrackID { get; set; }
        public Track InstructorTrack { get; set; }
        [ForeignKey("InstructorIntake")]
        [Required(ErrorMessage = "Intake ID is required")]

        public int IntakeID { get; set; }
        public Intake InstructorIntake { get; set; }
    }
}
