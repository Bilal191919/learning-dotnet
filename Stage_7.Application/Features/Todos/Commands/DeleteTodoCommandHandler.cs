using MediatR;
using Stage_7.Domain;

namespace Stage_7.Application.Features.Todos.Commands
{
	public class DeleteTodoCommandHandler : IRequestHandler<DeleteTodoCommand, bool>
	{
		private readonly IAppDbContext _context;

		public DeleteTodoCommandHandler(IAppDbContext context)
		{
			_context = context;
		}

		public async Task<bool> Handle(DeleteTodoCommand request, CancellationToken cancellationToken)
		{
			// 1. Item dhoonda
			var todoItem = await _context.TodoItems.FindAsync(request.Id);

			// 2. Check kiya: Kya item hai? Kya ye isi user ka hai?
			if (todoItem == null || todoItem.UserId != request.UserId)
			{
				return false; // Fail
			}

			// 3. Delete kiya
			_context.TodoItems.Remove(todoItem);

			// 4. Save kiya
			await _context.SaveChangesAsync(cancellationToken);

			return true; // Success
		}
	}
}
