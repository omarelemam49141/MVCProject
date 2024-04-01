using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCProject.Models
{
    public class Permission
    {
        public int Id { get; set; }
        public DateOnly Date { get; set; }
        [ForeignKey("Student")]
        public int StdID { get; set; }
        public Student Student { get; set; }
        [RegularExpression("(Absent|Late)")]
        public string Type { get; set; }
        [RegularExpression("(Pending|Accepted|Denied)")]
        public string Status { get; set; }
        [ForeignKey("Instructor")]
        public int InstructorID { get; set; }
        public Instructor Instructor { get; set; }
    }
}
