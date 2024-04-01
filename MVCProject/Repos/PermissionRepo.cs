using Microsoft.EntityFrameworkCore;
using MVCProject.Data;
using MVCProject.Models;

namespace MVCProject.Repos
{
    public interface IPermissionRepo
    {
        public List<Permission> GetPermissionsBySupervisorID(int supervisorID);
        public Permission GetPermissionByID(int id);
        public void UpdatePermissionStatus(Permission p, string status);
    }
    public class PermissionRepo: IPermissionRepo
    {
        private attendanceDBContext db;
        public PermissionRepo(attendanceDBContext _db) { db = _db; }

        public Permission GetPermissionByID(int id)
        {
            return db.Permissions.FirstOrDefault(p => p.Id == id);
        }

        public List<Permission> GetPermissionsBySupervisorID(int supervisorID)
        {
            return db.Permissions.Include(p=>p.Student).Where(p => p.InstructorID == supervisorID).ToList();
        }

        public void UpdatePermissionStatus(Permission p, string status)
        {
            p.Status = status;
            db.SaveChanges();
        }
    }
}
