namespace Stage_4.DTOs;

public class TodoResponseDto
{
	public int Id { get; set; }
	public string Title { get; set; } = string.Empty;
	public bool IsCompleted { get; set; }
}