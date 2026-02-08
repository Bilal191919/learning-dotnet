using MediatR;
using Stage_7.Application.Features.Todos.Commands;

namespace Stage_7.Application.Features.Todos.Handlers
{
	public class DeleteTodoCommandHandler : IRequestHandler<DeleteTodoCommand>
	{
		private readonly IAppDbContext _context;

		public DeleteTodoCommandHandler(IAppDbContext context)
		{
			_context = context;
		}

		public async Task Handle(DeleteTodoCommand request, CancellationToken cancellationToken)
		{
			var todo = await _context.Todos.FindAsync(new object[] { request.Id }, cancellationToken);

			if (todo != null)
			{
				_context.Todos.Remove(todo);
				await _context.SaveChangesAsync(cancellationToken);
			}
		}
	}
}