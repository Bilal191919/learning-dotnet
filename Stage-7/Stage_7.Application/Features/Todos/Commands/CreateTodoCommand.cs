using MediatR;
using Stage_7.Domain;

namespace Stage_7.Application.Features.Todos.Commands
{
	
	public class CreateTodoCommand : IRequest<TodoItem>
	{
		public string Title { get; set; }
		public bool IsCompleted { get; set; }
		public int UserId { get; set; } 
	}
}