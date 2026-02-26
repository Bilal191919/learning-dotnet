using MediatR;
using Stage_7.Application.Common;
using Stage_7.Application.DTOs;

namespace Stage_7.Application.Features.Todos.Queries
{
	public class GetTodosQuery : IRequest<PagedResult<TodoDto>>
	{
		public int UserId { get; set; }
		public int PageNumber { get; set; }
		public int PageSize { get; set; }
		public bool UseOptimized { get; set; }

		// Khali Constructor: Is se CS7036 error hamesha ke liye khatam ho jayega
		public GetTodosQuery() { }
	}
}