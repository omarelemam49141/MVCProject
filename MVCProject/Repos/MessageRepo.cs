using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using MVCProject.Data;
using MVCProject.Models;


namespace MVCProject.Repos
{
	public interface IMessageRepo
	{
		public void AddMessage(Message message);

	}
	public class MessageRepo : IMessageRepo
	{
		private attendanceDBContext db;

		public MessageRepo(attendanceDBContext _db)
		{
			db = _db;
		}

		public void AddMessage(Message message)
		{
			
		}
	}
}
