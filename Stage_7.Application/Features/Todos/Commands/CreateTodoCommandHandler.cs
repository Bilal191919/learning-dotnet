using MediatR;
using Stage_7.Domain;

namespace Stage_7.Application.Features.Todos.Commands
{
	public class CreateTodoCommandHandler : IRequestHandler<CreateTodoCommand, TodoItem>
	{
		private readonly IAppDbContext _context;

		public CreateTodoCommandHandler(IAppDbContext context)
		{
			_context = context;
		}

		public async Task<TodoItem> Handle(CreateTodoCommand request, CancellationToken cancellationToken)
		{
			// 1. Naya object banaya
			var newTodo = new TodoItem
			{
				Title = request.Title,
				IsCompleted = request.IsCompleted,
				UserId = request.UserId
			};

			
			_context.TodoItems.Add(newTodo);

			
			await _context.SaveChangesAsync(cancellationToken);

		
			return newTodo;
		}
	}
}