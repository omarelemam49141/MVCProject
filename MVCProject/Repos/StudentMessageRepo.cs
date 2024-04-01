using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using MVCProject.Data;
using MVCProject.Models;

namespace MVCProject.Repos
{
    public interface IStudentMessageRepo
    {
        public void CreateNewMessage(int stdID, DateTime date, string content);
    }
    public class StudentMessageRepo : IStudentMessageRepo
    {
        private attendanceDBContext db;

        public StudentMessageRepo(attendanceDBContext _db) { db = _db; }
        public void CreateNewMessage(int stdID, DateTime date, string content)
        {
            StudentMessage message = new StudentMessage();
            message.StudentID = stdID;
            message.Date = date;
            message.Content = content;
            db.SaveChanges();
        }
    }
}
