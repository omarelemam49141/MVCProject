using System.ComponentModel.DataAnnotations.Schema;

namespace MVCProject.Models
{
    public class Schedule
    {
        public int id { get; set; }
        public DateOnly Date { get; set; }
        public TimeOnly StartPeriod { get; set; }
        [ForeignKey("Track")]
        public int TrackID { get; set; }
        public Track Track { get; set; }
    }
}
