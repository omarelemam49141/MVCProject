using System.ComponentModel.DataAnnotations.Schema;

namespace MVCProject.Models
{
    public class StudentMessage
    {
        public int Id { get; set; }
        [ForeignKey("Student")]
        public int StudentID { get; set; }
        public DateTime Date { get; set; }
        public string Content { get; set; }
        public Student Student { get; set; }

        public bool Read { get; set; }
    }
}
