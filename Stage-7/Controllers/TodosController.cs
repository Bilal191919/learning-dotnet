using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stage_7.Application.Features.Todos.Commands;
using Stage_7.Application.Features.Todos.Queries;
using Stage_7.Domain;

namespace Stage_7.WebAPI.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class TodoController : ControllerBase
{
	private readonly IMediator _mediator;

	public TodoController(IMediator mediator)
	{
		_mediator = mediator;
	}

	[HttpGet]
	public async Task<IActionResult> Get()
	{
		var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
		if (string.IsNullOrEmpty(userIdString)) return Unauthorized();

		int userId = int.Parse(userIdString);
		var result = await _mediator.Send(new GetTodosQuery(userId));
		return Ok(result);
	}

	[HttpPost]
	public async Task<IActionResult> Create([FromBody] CreateTodoRequest request)
	{
		var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
		if (string.IsNullOrEmpty(userIdString)) return Unauthorized();

		int userId = int.Parse(userIdString);
		var command = new CreateTodoCommand { Title = request.Title, UserId = userId };
		var result = await _mediator.Send(command);

		return Ok(result);
	}
}

public class CreateTodoRequest
{
	public string Title { get; set; } = string.Empty;
}