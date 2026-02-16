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
			
			var todoItem = await _context.Todos.FindAsync(request.Id);

		
			if (todoItem == null || todoItem.UserId != request.UserId)
			{
				return false; 
			}

			_context.Todos.Remove(todoItem);

			await _context.SaveChangesAsync(cancellationToken);

			return true; 
		}
	}
}
