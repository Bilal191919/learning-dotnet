using MediatR;
using Stage_7.Domain;

namespace Stage_7.Application.Features.Todos.Queries
{

	public class GetTodosQuery : IRequest<List<TodoItem>>
	{
		public int UserId { get; set; }

		public GetTodosQuery(int userId)
		{
			UserId = userId;
		}
	}
}
