using System.ComponentModel.DataAnnotations;

namespace MVCProject.ViewModels
{
    public class LoggedInUser
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
