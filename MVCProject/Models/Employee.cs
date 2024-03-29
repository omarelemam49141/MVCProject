using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCProject.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Mobile { get; set; }
        [RegularExpression("(StudentAffairs|Security)")]
        public string Type { get; set; }
        [ForeignKey("DeptID")]
        public int DeptID { get; set; }
        public Department Department { get; set; }
    }
}
