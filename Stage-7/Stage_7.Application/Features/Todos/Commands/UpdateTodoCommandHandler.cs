using MediatR;
using Stage_7.Domain;

namespace Stage_7.Application.Features.Todos.Commands
{
	public class UpdateTodoCommandHandler : IRequestHandler<UpdateTodoCommand, bool>
	{
		private readonly IAppDbContext _context;

		public UpdateTodoCommandHandler(IAppDbContext context)
		{
			_context = context;
		}

		public async Task<bool> Handle(UpdateTodoCommand request, CancellationToken cancellationToken)
		{
			// 1. Purana record dhoonda
			var todoItem = await _context.Todos.FindAsync(request.Id);

			// 2. Check kiya ke record hai bhi ya nahi, aur kya ye usi user ka hai?
			if (todoItem == null || todoItem.UserId != request.UserId)
			{
				return false; // Fail
			}

			// 3. Update kiya
			todoItem.Title = request.Title;
			todoItem.IsCompleted = request.IsCompleted;

			// 4. Save kiya
			await _context.SaveChangesAsync(cancellationToken);

			return true; // Success
		}
	}
}
