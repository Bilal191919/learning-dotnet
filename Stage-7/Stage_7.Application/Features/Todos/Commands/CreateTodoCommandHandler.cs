using MediatR;
using Stage_7.Domain;

namespace Stage_7.Application.Features.Todos.Commands;

public class CreateTodoCommandHandler : IRequestHandler<CreateTodoCommand, int>
{
	private readonly IAppDbContext _context;

	public CreateTodoCommandHandler(IAppDbContext context)
	{
		_context = context;
	}

	public async Task<int> Handle(CreateTodoCommand request, CancellationToken cancellationToken)
	{
		var todoItem = new TodoItem
		{
			Title = request.Title,
			UserId = request.UserId,
			IsCompleted = false
		};

		_context.Todos.Add(todoItem);
		await _context.SaveChangesAsync(cancellationToken);

		return todoItem.Id;
	}
}