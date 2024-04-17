using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCProject.Models
{
    public class Employee
    {
        public int Id { get; set; }
        [Required]
        [Remote("ValidateName", "EmployeesManage" , AdditionalFields ="Id")]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        [Remote("ValidateEmail", "EmployeesManage", AdditionalFields = "Id", ErrorMessage = "Email Alread Exist")]

        public string Email { get; set; }
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$", ErrorMessage = "Password must be at least 8 characters long, contain at least one uppercase letter, one lowercase letter, one digit, and one special character.")]
        public string Password { get; set; }

        [RegularExpression(@"^(010|011|012)\d{8}$", ErrorMessage = "Mobile number must start with 010, 011, or 012 followed by 8 digits.")]
        public string Mobile { get; set; }
        [RegularExpression("(StudentAffairs|Security)")]
        public string Type { get; set; }
        [ForeignKey("Department")]
        public int DeptID { get; set; }
        public Department Department { get; set; }
    }
}
