namespace MVCProject.Repos
{
    using MVCProject.Data;
    using MVCProject.Models;
    using System.Collections.Generic;
    using System.Linq;

    public interface IDepartmentRepo
    {
        void AddDepartment(Department department);
        void RemoveDepartment(int departmentId);
        void UpdateDepartment(Department department);
        Department GetDepartmentById(int departmentId);
        void AssignInstructorToDept(int deptid, int insid);
        void AssignEmployeeToDept(int deptid, int eid);
        IEnumerable<Department> GetAllDepartments();
    }

    public class DepartmentRepo : IDepartmentRepo
    {
        private readonly attendanceDBContext db;

        public DepartmentRepo(attendanceDBContext dbContext)
        {
            db = dbContext;
        }

        public void AddDepartment(Department department)
        {
            db.Departments.Add(department);
            db.SaveChanges();
        }

        public void RemoveDepartment(int departmentId)
        {
            var departmentToRemove = db.Departments.FirstOrDefault(d => d.Id == departmentId);
            if (departmentToRemove != null)
            {
                db.Departments.Remove(departmentToRemove);
                db.SaveChanges();
            }
        }

        public void UpdateDepartment(Department department)
        {
            db.Departments.Update(department);
            db.SaveChanges();
        }

        public Department GetDepartmentById(int departmentId)
        {
            return db.Departments.FirstOrDefault(d => d.Id == departmentId);
        }

        public IEnumerable<Department> GetAllDepartments()
        {
            return db.Departments.ToList();
        }

        public void AssignInstructorToDept(int deptId, int insId)
        {
            var department = db.Departments.FirstOrDefault(d => d.Id == deptId);
            var instructor = db.Instructors.FirstOrDefault(i => i.Id == insId);

            if (department != null && instructor != null)
            {
                department.Instructors.Add(instructor);
                db.SaveChanges();
            }
        }

        public void AssignEmployeeToDept(int deptId, int empId)
        {
            var department = db.Departments.FirstOrDefault(d => d.Id == deptId);
            var employee = db.Employees.FirstOrDefault(e => e.Id == empId);

            if (department != null && employee != null)
            {
                department.Employees.Add(employee);
                db.SaveChanges();
            }
        }
    }
}

