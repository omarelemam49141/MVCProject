using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NuGet.DependencyResolver;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCProject.Models
{
    public class Track
    {
        public int Id { get; set; }
        [Required(AllowEmptyStrings =false ,ErrorMessage ="Name is required") ]
        public string Name { get; set; }
        [RegularExpression("(Active|Inactive)")]
        [Required]
        public string Status { get; set; }
        [ForeignKey("Program")]
        [Required(ErrorMessage ="please select a program")]
        public int programID { get; set; }
        public _Program Program { get; set; }
        public ICollection<Intake> Intakes { get; set; } = new HashSet<Intake>();
        public List<StudentIntakeTrack> StudentIntakeTracks { get; set; } = new List<StudentIntakeTrack>();
        public ICollection<Schedule> Schedules { get; set; } = new HashSet<Schedule>();
        [ForeignKey("Supervisor")]
        [Required(ErrorMessage ="Please Select A Supervisor")]
        [Remote("ValidateInstructor", "Track", AdditionalFields ="Id",ErrorMessage = "This Instructor is already assigned to another track. Make the track available by <a href='/InstructorsManage/MakeTrackAvailable" + "'>clicking here</a>")]
        public int? SupervisorID { get; set; } 
        public Instructor Supervisor { get; set; }
    }
}
