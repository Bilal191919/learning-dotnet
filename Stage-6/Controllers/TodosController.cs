using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stage_4.Data;
using Stage_4.Models;
using Microsoft.AspNetCore.Authorization;

namespace Stage_4.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class TodosController : ControllerBase
	{
		private readonly AppDbContext _context;

		public TodosController(AppDbContext context)
		{
			_context = context;
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<TodoItem>>> GetTodos()
		{
			return await _context.TodoItems.ToListAsync();
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<TodoItem>> GetTodoItem(int id)
		{
			var todoItem = await _context.TodoItems.FindAsync(id);

			if (todoItem == null)
			{
				return NotFound();
			}

			return todoItem;
		}

		[HttpPost]
		public async Task<ActionResult<TodoItem>> PostTodoItem(TodoItem todoItem)
		{
			_context.TodoItems.Add(todoItem);
			await _context.SaveChangesAsync();

			return CreatedAtAction("GetTodoItem", new { id = todoItem.Id }, todoItem);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> PutTodoItem(int id, TodoItem todoItem)
		{
			if (id != todoItem.Id)
			{
				return BadRequest();
			}

			_context.Entry(todoItem).State = EntityState.Modified;

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!TodoItemExists(id))
				{
					return NotFound();
				}
				else
				{
					throw;
				}
			}

			return NoContent();
		}

		[Authorize(Policy = "CanManageTodos")]
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteTodo(int id)
		{
			var todoItem = await _context.TodoItems.FindAsync(id);
			if (todoItem == null)
			{
				return NotFound();
			}

			_context.TodoItems.Remove(todoItem);
			await _context.SaveChangesAsync();

			return NoContent();
		}

		private bool TodoItemExists(int id)
		{
			return _context.TodoItems.Any(e => e.Id == id);
		}
	}
}