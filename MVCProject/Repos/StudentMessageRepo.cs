using MVCProject.Data;
using MVCProject.Models;

namespace MVCProject.Repos
{
    public interface IStudentMessageRepo
    {
        public void CreateNewMessage(int stdID, DateTime date, string content);
        public List<StudentMessage> GetStudentMessages(int stdId);

        public int GetUnreadMessagesCount(int stdId);
        public void MarkMessageAsRead(int messageId);

        public void MarkAllMessagesAsRead(int stdId);
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
            db.StudentMessages.Add(message);
            db.SaveChanges();
        }

        public List<StudentMessage> GetStudentMessages(int stdId)
        {
            return db.StudentMessages.Where(m => m.StudentID == stdId).OrderByDescending(m => m.Date).ToList();
        }

        public int GetUnreadMessagesCount(int stdId)
        {
            return db.StudentMessages.Count(m => m.StudentID == stdId && m.Read == false);
        }

        public void MarkMessageAsRead(int messageId)
        {
            StudentMessage message = db.StudentMessages.Find(messageId);
            message.Read = true;
            db.SaveChanges();
        }

        public void MarkAllMessagesAsRead(int stdId)
        {
            List<StudentMessage> messages = db.StudentMessages.Where(m => m.StudentID == stdId).ToList();
            foreach (var message in messages)
            {
                message.Read = true;
            }
            db.SaveChanges();
        }
    }
}
