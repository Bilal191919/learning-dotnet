using MediatR;
using Microsoft.AspNetCore.Mvc;
using Stage_7.Application.Features.Auth.Commands;
using Stage_7.Application.Features.Auth.Queries;

namespace Stage_4.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
	private readonly IMediator _mediator;

	public AuthController(IMediator mediator)
	{
		_mediator = mediator;
	}

	
	[HttpPost("register")]
	public async Task<ActionResult<int>> Register(RegisterUserCommand command)
	{
		return await _mediator.Send(command);
	}


	[HttpPost("login")]
	public async Task<ActionResult<string>> Login(LoginQuery query)
	{
		try
		{
			var result = await _mediator.Send(query);
			return Ok(result);
		}
		catch (Exception ex)
		{
			return BadRequest(ex.Message);
		}
	}
}