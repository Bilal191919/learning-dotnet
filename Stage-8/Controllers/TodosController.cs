using Microsoft.AspNetCore.Mvc;
using MediatR;
using Stage_7.Application.Features.Todos.Queries;
using Stage_7.Application.Features.Todos.Commands;
using Stage_7.Application.Common;
using Stage_7.Application.DTOs;

namespace Stage_4.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class TodosController : ControllerBase
	{
		private readonly IMediator _mediator;

		public TodosController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpGet]
		public async Task<ActionResult<PagedResult<TodoDto>>> GetTodos(
			int pageNumber = 1,
			int pageSize = 10,
			bool useOptimized = true)
		{
			var query = new GetTodosQuery(1, pageNumber, pageSize, useOptimized);
			return Ok(await _mediator.Send(query));
		}

		[HttpPost]
		public async Task<IActionResult> CreateTodo(CreateTodoCommand command)
		{
			var result = await _mediator.Send(command);
			return Ok(result);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateTodo(int id, UpdateTodoCommand command)
		{
			if (id != command.Id) return BadRequest();
			await _mediator.Send(command);
			return NoContent();
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteTodo(int id)
		{
			await _mediator.Send(new DeleteTodoCommand { Id = id });
			return NoContent();
		}
	}
}