using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stage_7.Domain;
using Stage_7.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using MediatR;
using Stage_7.Application.Features.Todos.Queries;
using Stage_7.Application.Features.Todos.Commands; 

namespace Stage_4.Controllers
{
	[Authorize]
	[Route("api/[controller]")]
	[ApiController]
	public class TodosController : ControllerBase
	{
		private readonly AppDbContext _context; 
		private readonly IMediator _mediator;   

		public TodosController(AppDbContext context, IMediator mediator)
		{
			_context = context;
			_mediator = mediator;
		}

	
		[HttpGet]
		public async Task<ActionResult<IEnumerable<TodoItem>>> GetTodos()
		{
			var user = await GetCurrentUser();
			if (user == null) return Unauthorized();

			var query = new GetTodosQuery(user.Id);
			var result = await _mediator.Send(query);

			return Ok(result);
		}

		
		[HttpGet("{id}")]
		public async Task<ActionResult<TodoItem>> GetTodoItem(int id)
		{
			var user = await GetCurrentUser();
			if (user == null) return Unauthorized();

			var todoItem = await _context.Todos.FindAsync(id);

			if (todoItem == null) return NotFound();
			if (todoItem.UserId != user.Id) return NotFound();

			return todoItem;
		}

		//
		[HttpPost]
		public async Task<ActionResult<TodoItem>> PostTodoItem(TodoItem todoItem)
		{
			var user = await GetCurrentUser();
			if (user == null) return Unauthorized();

			var command = new CreateTodoCommand
			{
				Title = todoItem.Title,
				IsCompleted = todoItem.IsCompleted,
				UserId = user.Id
			};

			var result = await _mediator.Send(command);

			return CreatedAtAction("GetTodoItem", new { id = result.Id }, result);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> PutTodoItem(int id, TodoItem todoItem)
		{
			if (id != todoItem.Id) return BadRequest();

			var user = await GetCurrentUser();
			if (user == null) return Unauthorized();

			var command = new UpdateTodoCommand
			{
				Id = id,
				Title = todoItem.Title,
				IsCompleted = todoItem.IsCompleted,
				UserId = user.Id
			};

			var success = await _mediator.Send(command);

			if (!success) return NotFound();

			return NoContent();
		}

		
		[Authorize(Policy = "CanManageTodos")]
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteTodo(int id)
		{
			var user = await GetCurrentUser();
			if (user == null) return Unauthorized();

			
			var command = new DeleteTodoCommand
			{
				Id = id,
				UserId = user.Id
			};

			var success = await _mediator.Send(command);

			if (!success) return NotFound();

			return NoContent();
		}

		
		private async Task<User?> GetCurrentUser()
		{
			var username = User.Identity?.Name;
			if (string.IsNullOrEmpty(username)) return null;
			return await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
		}
	}
}