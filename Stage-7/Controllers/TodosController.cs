using MediatR;
using Microsoft.AspNetCore.Mvc;
using Stage_7.Application.Features.Todos.Commands;
using Stage_7.Application.Features.Todos.Queries;
using Stage_7.Domain;

namespace Stage_4.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TodoController : ControllerBase
{
	private readonly IMediator _mediator;

	public TodoController(IMediator mediator)
	{
		_mediator = mediator;
	}

	[HttpPost]
	public async Task<ActionResult<int>> Create(CreateTodoCommand command)
	{
		return await _mediator.Send(command);
	}

	[HttpGet]
	public async Task<ActionResult<List<TodoItem>>> GetAll([FromQuery] int userId)
	{
		return await _mediator.Send(new GetTodosQuery(userId));
	}

	[HttpPut("{id}")]
	public async Task<ActionResult> Update(int id, UpdateTodoCommand command)
	{
		if (id != command.Id)
		{
			return BadRequest();
		}

		await _mediator.Send(command);
		return NoContent();
	}

	[HttpDelete("{id}")]
	public async Task<ActionResult> Delete(int id)
	{
		await _mediator.Send(new DeleteTodoCommand { Id = id });
		return NoContent();
	}
}