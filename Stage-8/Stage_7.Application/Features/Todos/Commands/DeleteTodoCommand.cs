using MediatR;

namespace Stage_7.Application.Features.Todos.Commands
{
	public class DeleteTodoCommand : IRequest
	{
		public int Id { get; set; }
	}
}