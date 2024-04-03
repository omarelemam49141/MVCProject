namespace MVCProject.Models
{
    public class Intake
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Track> Tracks { get; set; } = new HashSet<Track>();
        public List<StudentIntakeTrack> StudentIntakeTracks { get; set; } = new List<StudentIntakeTrack>();
        public ICollection<Instructor> instructors { get; set; } = new HashSet<Instructor>();
    }
}
