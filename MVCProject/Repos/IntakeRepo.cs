using Microsoft.EntityFrameworkCore;
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
            return db.Intakes.Include(p=>p.Program).Include(c=>c.Tracks).FirstOrDefault(i => i.Id == intakeId);
        }

        public IEnumerable<Intake> GetAllIntakes()
        {
            return db.Intakes.Include(a=>a.Program).Include(a=>a.Tracks).ToList();
        }

        public void AssignTracksToIntake(int intakeId, List<int> trackIds)
        {
            // Get the intake object by its ID
            var intake = this.GetIntakeById(intakeId);

            if (intake != null)
            {
                var intakeTrackIds = intake.Tracks.Select(t => t.Id);

                var tracksToRemove = intakeTrackIds.Except(trackIds).ToList();

                var tracksToAdd = trackIds.Except(intakeTrackIds).ToList();

                foreach (var trackId in tracksToRemove)
                {
                    var trackToRemove = intake.Tracks.FirstOrDefault(t => t.Id == trackId);
                    if (trackToRemove != null)
                    {
                        intake.Tracks.Remove(trackToRemove);
                    }
                }

                // Add tracks to the intake
                foreach (var trackId in tracksToAdd)
                {
                    var trackToAdd = db.Tracks.FirstOrDefault(t=>t.Id ==trackId); // Assuming a method to get a track by its ID
                    if (trackToAdd != null)
                    {
                        intake.Tracks.Add(trackToAdd);
                    }
                }
                // Save changes to the database or perform any necessary updates
                db.SaveChanges(); // Assuming a method to save changes in the repository
            }
        }


    }


}

