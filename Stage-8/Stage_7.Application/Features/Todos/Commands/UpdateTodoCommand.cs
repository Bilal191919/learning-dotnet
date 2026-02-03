using MediatR;

namespace Stage_7.Application.Features.Todos.Commands
{
	// Ye Command 'bool' (True/False) wapis kare gi taake pata chale update hua ya nahi
	public class UpdateTodoCommand : IRequest<bool>
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public bool IsCompleted { get; set; }
		public int UserId { get; set; } // Security ke liye zaroori hai
	}
}