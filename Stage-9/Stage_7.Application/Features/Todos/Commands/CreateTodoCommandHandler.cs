using MediatR;
using Stage_7.Domain;
using Stage_7.Application.Features.Todos.Commands;

namespace Stage_7.Application.Features.Todos.Handlers
{
	public class CreateTodoCommandHandler : IRequestHandler<CreateTodoCommand, int>
	{
		private readonly IAppDbContext _context;

		public CreateTodoCommandHandler(IAppDbContext context)
		{
			_context = context;
		}

		public async Task<int> Handle(CreateTodoCommand request, CancellationToken cancellationToken)
		{
			var todo = new TodoItem
			{
				Title = request.Title,
				IsCompleted = false,
				UserId = request.UserId
			};

			_context.Todos.Add(todo);
			await _context.SaveChangesAsync(cancellationToken);

			return todo.Id;
		}
	}
}