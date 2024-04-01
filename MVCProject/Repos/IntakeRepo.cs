using MVCProject.Data;
using MVCProject.Models;
namespace MVCProject.Repos
{

    public interface IIntakeRepo
    {
        void AddIntake(Intake intake);
        void RemoveIntake(int intakeId);
        void UpdateIntake(Intake intake);
        Intake GetIntakeById(int intakeId);
        IEnumerable<Intake> GetAllIntakes();
        void AssignTracksToIntake(int intakeId, List<int> trackIds);
    }

    public class IntakeRepo : IIntakeRepo
    {
        private readonly attendanceDBContext db;

        public IntakeRepo(attendanceDBContext dbContext)
        {
            db = dbContext;
        }

        public void AddIntake(Intake intake)
        {
            db.Intakes.Add(intake);
            db.SaveChanges();
        }

        public void RemoveIntake(int intakeId)
        {
            var intakeToRemove = db.Intakes.FirstOrDefault(i => i.Id == intakeId);
            if (intakeToRemove != null)
            {
                db.Intakes.Remove(intakeToRemove);
                db.SaveChanges();
            }
        }

        public void UpdateIntake(Intake intake)
        {
            db.Intakes.Update(intake);
            db.SaveChanges();
        }

        public Intake GetIntakeById(int intakeId)
        {
            return db.Intakes.FirstOrDefault(i => i.Id == intakeId);
        }

        public IEnumerable<Intake> GetAllIntakes()
        {
            return db.Intakes.ToList();
        }

        public void AssignTracksToIntake(int intakeId, List<int> trackIds)
        {
            var intake = db.Intakes.FirstOrDefault(i => i.Id == intakeId);
            if (intake != null)
            {
                intake.Intakes.Clear(); // Clear existing tracks to avoid duplicates
                var tracksToAdd = db.Tracks.Where(t => trackIds.Contains(t.Id)).ToList();
                foreach (var track in tracksToAdd)
                {
                    intake.Intakes.Add(track);
                }
                db.SaveChanges();
            }
        }
    }


}

