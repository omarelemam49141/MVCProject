using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCProject.Models
{
    public class Schedule
    {
        public int id { get; set; }
        [Required(ErrorMessage ="The date is required")]
        public DateOnly Date { get; set; }
        [Required(ErrorMessage ="The start period is required")]
        public TimeOnly StartPeriod { get; set; }
        [ForeignKey("Track")]
        public int TrackID { get; set; }
        public Track Track { get; set; }
    }
}
