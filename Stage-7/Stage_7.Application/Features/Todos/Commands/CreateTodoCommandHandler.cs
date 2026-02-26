using AutoMapper;
using MediatR;
using Stage_7.Domain;

namespace Stage_7.Application.Features.Todos.Commands;

public class CreateTodoCommandHandler : IRequestHandler<CreateTodoCommand, int>
{
	private readonly IAppDbContext _context;
	private readonly IMapper _mapper;

	public CreateTodoCommandHandler(IAppDbContext context, IMapper mapper)
	{
		_context = context;
		_mapper = mapper;
	}

	public async Task<int> Handle(CreateTodoCommand request, CancellationToken cancellationToken)
	{
		var todoItem = _mapper.Map<TodoItem>(request);
		_context.Todos.Add(todoItem);
		await _context.SaveChangesAsync(cancellationToken);
		return todoItem.Id;
	}
}