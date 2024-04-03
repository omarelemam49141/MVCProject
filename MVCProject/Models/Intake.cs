using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCProject.Models
{
    public class Intake
    {
        public int Id { get; set; }
        [Required]
       // [Remote("ValidateName","Intake" , AdditionalFields = "Id", ErrorMessage ="This Name Already Exist")]
        public string Name { get; set; }
        public ICollection<Track> Tracks { get; set; } = new HashSet<Track>();
        public List<StudentIntakeTrack> StudentIntakeTracks { get; set; } = new List<StudentIntakeTrack>();
        public _Program Program { get; set; }
        public int ProgramId { get; set; }
        [Required]
        [Remote("ValidateYear" , "Intake")]
       public DateTime Year { get; set; }

  
    }
}
