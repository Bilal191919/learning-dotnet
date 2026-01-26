using Microsoft.AspNetCore.Mvc;
using Stage_4.DTOs;
using Stage_4.Models;

namespace Stage_4.Controllers;

[ApiController]
[Route("api/v1/[controller]")] 
public class TodosController : ControllerBase
{
	// Filhal hum data memory mein save karenge
	private static List<TodoItem> _todos = new();

	[HttpPost]
	public IActionResult Create(TodoCreateDto dto)
	{
		// 1. Mapping: User ke data ko Model mein badalna
		var newItem = new TodoItem
		{
			Id = _todos.Count + 1,
			Title = dto.Title,
			Description = dto.Description
		};

		_todos.Add(newItem);

		// 2. Mapping: Model ko wapas Response DTO mein badalna
		var response = new TodoResponseDto
		{
			Id = newItem.Id,
			Title = newItem.Title,
			IsCompleted = newItem.IsCompleted
		};

		// Status 201 Created return karna (HR isse bohot impress hota hai)
		return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
	}

	[HttpGet("{id}")]
	public IActionResult GetById(int id)
	{
		var item = _todos.FirstOrDefault(x => x.Id == id);
		if (item == null) return NotFound(); // Status 404

		return Ok(item);
	}
}
