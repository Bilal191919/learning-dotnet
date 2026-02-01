using MediatR;

namespace Stage_7.Application.Features.Todos.Commands
{
	// Ye Command batai gi ke delete hua (true) ya nahi (false)
	public class DeleteTodoCommand : IRequest<bool>
	{
		public int Id { get; set; }
		public int UserId { get; set; } // Security: Sirf apna todo delete kar sake
	}
}
