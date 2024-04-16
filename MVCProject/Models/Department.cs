using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace MVCProject.Models
{
    public class Department
    {
        public int Id { get; set; }
        [Required]
        [Remote("ValidateName","Department",AdditionalFields ="Id", ErrorMessage = "This Department Already Exist")]
        public string Name { get; set; }
        public ICollection<Employee> Employees { get; set; } = new HashSet<Employee>();
        public ICollection<Instructor> Instructors { get; set; } = new HashSet<Instructor>();
    }
}
