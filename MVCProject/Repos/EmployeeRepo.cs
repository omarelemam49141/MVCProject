using MVCProject.Data;
using MVCProject.Models;

namespace MVCProject.Repos
{
    public interface IEmployeeRepo
    {
        public Employee GetEmployeeByEmailAndPassword(string email, string password);
    }

    public class EmployeeRepo: IEmployeeRepo
    {
        private attendanceDBContext db;

        public EmployeeRepo(attendanceDBContext _db) 
        {
            db = _db;
        }

        public Employee GetEmployeeByEmailAndPassword(string email, string password)
        {
            return db.Employees.FirstOrDefault(i => i.Email.ToLower() == email.ToLower()
                                            && i.Password == password);
        }
    }
}
