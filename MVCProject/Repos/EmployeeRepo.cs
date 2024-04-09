using Microsoft.EntityFrameworkCore;
using MVCProject.Data;
using MVCProject.Models;

namespace MVCProject.Repos
{
    public interface IEmployeeRepo
    {
        public Employee GetEmployeeByEmailAndPassword(string email, string password);
        Employee GetEmployeeById(int id);
        List<Employee> GetAllEmployees();
        void AddEmployee(Employee employee);
        void UpdateEmployee(Employee employee);
        void DeleteEmployee(int id);
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
        public List<Employee> GetAllEmployees()
        {
            return db.Employees.Include(d=>d.Department).ToList();
        }

        public void AddEmployee(Employee employee)
        {
            db.Employees.Add(employee);
            db.SaveChanges();
        }

        public void UpdateEmployee(Employee employee)
        {
            db.Employees.Update(employee);
            db.SaveChanges();
        }

        public void DeleteEmployee(int id)
        {
            var employee = db.Employees.FirstOrDefault(e=>e.Id==id);
            if (employee != null)
            {
                db.Employees.Remove(employee);
                db.SaveChanges();
            }
        }
        public Employee? GetEmployeeById(int id)
        {
            Employee? employee = db.Employees.Include(e=>e.Department).FirstOrDefault(e => e.Id == id);
            if (employee != null)
                return employee;
            return null;
        }

    }
}
