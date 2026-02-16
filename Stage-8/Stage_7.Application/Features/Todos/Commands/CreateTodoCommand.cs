using MediatR;

namespace Stage_7.Application.Features.Todos.Commands
{
	public class CreateTodoCommand : IRequest<int>
	{
		public string Title { get; set; }
		public int UserId { get; set; }
	}
}