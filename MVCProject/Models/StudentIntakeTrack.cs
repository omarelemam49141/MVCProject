using System.ComponentModel.DataAnnotations.Schema;

namespace MVCProject.Models
{
    public class StudentIntakeTrack
    {
        [ForeignKey("Student")]
        public int StdID { get; set; }
        [ForeignKey("Intake")]
        public int IntakeID { get; set; }
        [ForeignKey("Track")]
        public int TrackID { get; set; }
        public Student Student { get; set; }
        public Intake Intake { get; set; }
        public Track Track { get; set; }
    }
}
