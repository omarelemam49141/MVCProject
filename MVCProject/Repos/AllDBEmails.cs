namespace MVCProject.Repos
{
    public record EmailsIds(int id, string email);
    public interface IAllDBEmails
    {
        public List<EmailsIds> GetEmails();
    }
    public class AllDBEmails:IAllDBEmails
    {
        private readonly IEmployeeRepo employeeRepo;
        private readonly IInstructorRepo instructorRepo;
        private readonly IStudentRepo studentRepo;

         public AllDBEmails(IEmployeeRepo _employeeRepo , IInstructorRepo _instructorRepo , IStudentRepo _studentRepo) {
                employeeRepo = _employeeRepo;
                instructorRepo = _instructorRepo;
                studentRepo = _studentRepo;
        
          }
        
        public List<EmailsIds> GetEmails()
        {
            var employeeEmails = employeeRepo.GetAllEmployees().Select(e => new EmailsIds(e.Id, e.Email));
            var studentEmails = studentRepo.GetAllStudents().Select(s => new EmailsIds(s.Id, s.Email));
            var instructorEmails = instructorRepo.GetAll().Select(i => new EmailsIds(i.Id, i.Email));

            var allEmails = employeeEmails
                .Union(studentEmails)
                .Union(instructorEmails)
                .ToList();

            return allEmails;
        }

    }
}
