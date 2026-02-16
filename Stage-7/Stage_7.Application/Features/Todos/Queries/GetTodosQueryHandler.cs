using MediatR;
using Microsoft.EntityFrameworkCore;
using Stage_7.Domain;

namespace Stage_7.Application.Features.Todos.Queries;

public class GetTodosQueryHandler : IRequestHandler<GetTodosQuery, List<TodoItem>>
{
	private readonly IAppDbContext _context;

	public GetTodosQueryHandler(IAppDbContext context)
	{
		_context = context;
	}

	public async Task<List<TodoItem>> Handle(GetTodosQuery request, CancellationToken cancellationToken)
	{
		return await _context.Todos
			.Where(x => x.UserId == request.UserId)
			.ToListAsync(cancellationToken);
	}
}