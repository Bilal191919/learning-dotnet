using MediatR;
using Stage_7.Application.Features.Todos.Commands;

namespace Stage_7.Application.Features.Todos.Handlers
{
	public class UpdateTodoCommandHandler : IRequestHandler<UpdateTodoCommand>
	{
		private readonly IAppDbContext _context;

		public UpdateTodoCommandHandler(IAppDbContext context)
		{
			_context = context;
		}

		public async Task Handle(UpdateTodoCommand request, CancellationToken cancellationToken)
		{
			var todo = await _context.Todos.FindAsync(new object[] { request.Id }, cancellationToken);

			if (todo == null)
			{
				return;
			}

			todo.Title = request.Title;
			todo.IsCompleted = request.IsCompleted;

			await _context.SaveChangesAsync(cancellationToken);
		}
	}
}