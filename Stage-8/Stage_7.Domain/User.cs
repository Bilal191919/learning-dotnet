using System.Collections.Generic; 

namespace Stage_7.Domain
{
	public class User
	{
		public int Id { get; set; }
		public string Username { get; set; } = string.Empty;
		public string Email { get; set; } = string.Empty;
		public string PasswordHash { get; set; } = string.Empty;

		// 👇 Ye nayi line hai (User ke paas bohot saaray TodoItems honge)
		public List<TodoItem> TodoItems { get; set; } = new List<TodoItem>();
	}
}