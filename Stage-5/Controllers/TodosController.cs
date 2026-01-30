using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stage_4.Data;
using Stage_4.Models;

namespace Stage_4.Controllers
{
	[Route("api/v1/[controller]")] // Professional Versioned Routing
	[ApiController]
	public class TodosController : ControllerBase
	{
		private readonly AppDbContext _context;

		public TodosController(AppDbContext context)
		{
			_context = context;
		}

		// 1. GET ALL: api/v1/Todos
		[HttpGet]
		public async Task<ActionResult<IEnumerable<TodoItem>>> GetTodos()
		{
			return await _context.TodoItems.ToListAsync();
		}

		// 2. MINI-ASSIGNMENT: api/v1/Todos/incomplete
		// par iska successful result dekha ja sakta hai
		[HttpGet("incomplete")]
		public async Task<ActionResult<IEnumerable<TodoItem>>> GetIncompleteTodos()
		{
			return await _context.TodoItems
				.Where(t => !t.IsCompleted)
				.ToListAsync();
		}

		// 3. GET BY ID: api/v1/Todos/5
		// ':int' constraint ambiguity khatam karti hai
		[HttpGet("{id:int}")]
		public async Task<ActionResult<TodoItem>> GetTodo(int id)
		{
			var todo = await _context.TodoItems.FindAsync(id);
			if (todo == null) return NotFound();
			return todo;
		}

		// 4. POST: api/v1/Todos
		[HttpPost]
		public async Task<ActionResult<TodoItem>> PostTodo(TodoItem todoItem)
		{
			_context.TodoItems.Add(todoItem);
			await _context.SaveChangesAsync();
			return CreatedAtAction(nameof(GetTodo), new { id = todoItem.Id }, todoItem);
		}

		// 5. PUT: api/v1/Todos/5 (Update Task)
		[HttpPut("{id:int}")]
		public async Task<IActionResult> PutTodo(int id, TodoItem todoItem)
		{
			if (id != todoItem.Id) return BadRequest();
			_context.Entry(todoItem).State = EntityState.Modified;
			await _context.SaveChangesAsync();
			return NoContent();
		}

		// 6. DELETE: api/v1/Todos/5 (Remove Task)
		[HttpDelete("{id:int}")]
		public async Task<IActionResult> DeleteTodo(int id)
		{
			var todo = await _context.TodoItems.FindAsync(id);
			if (todo == null) return NotFound();
			_context.TodoItems.Remove(todo);
			await _context.SaveChangesAsync();
			return NoContent();
		}
	}
}
