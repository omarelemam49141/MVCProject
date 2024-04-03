using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCProject.Models
{
    public class Track
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [RegularExpression("(Active|Inactive)")]
        public string Status { get; set; }
        [ForeignKey("Program")]
        public int programID { get; set; }
        public _Program Program { get; set; }
        public ICollection<Intake> Intakes { get; set; } = new HashSet<Intake>();
        public List<StudentIntakeTrack> StudentIntakeTracks { get; set; } = new List<StudentIntakeTrack>();
        public ICollection<Schedule> Schedules { get; set; } = new HashSet<Schedule>();
        [ForeignKey("Supervisor")]
        public int? SupervisorID { get; set; }
        public Instructor Supervisor { get; set; }
        public ICollection<Instructor> instructors { get; set; } = new HashSet<Instructor>();

    }
}
