using MediatR;

namespace Stage_7.Application.Features.Todos.Commands
{
	public class UpdateTodoCommand : IRequest
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public bool IsCompleted { get; set; }
	}
}