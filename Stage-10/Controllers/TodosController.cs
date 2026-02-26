using MediatR;
using Microsoft.AspNetCore.Mvc;
using Stage_7.Application.Common;
using Stage_7.Application.DTOs;
using Stage_7.Application.Features.Todos.Queries;

namespace Stage_4.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class TodosController : ControllerBase
	{
		private readonly IMediator _mediator;

		public TodosController(IMediator mediator)
		{
			_mediator = mediator;
		}

	
		[HttpGet]
		public async Task<ActionResult<PagedResult<TodoDto>>> GetTodos(int userId, int pageNumber = 1, int pageSize = 10, bool useOptimized = true)
		{
			
			var query = new GetTodosQuery
			{
				UserId = userId,
				PageNumber = pageNumber,
				PageSize = pageSize,
				UseOptimized = useOptimized
			};

			var result = await _mediator.Send(query);
			return Ok(result);
		}
	}
}