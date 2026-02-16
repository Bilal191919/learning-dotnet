using Xunit;
using Stage_7.Application.Features.Todos.Commands;
using Stage_7.Domain;

namespace Stage_8.UnitTests
{
	public class TodoValidationTests
	{
		[Fact]
		public async Task Should_Fail_When_Title_Is_Empty()
		{
			var validator = new CreateTodoCommandValidator();
			var command = new CreateTodoCommand { Title = "" };

			var result = await validator.ValidateAsync(command);

			Assert.False(result.IsValid);
		}

		[Fact]
		public async Task Should_Pass_When_Title_Is_Valid()
		{
			var validator = new CreateTodoCommandValidator();
			var command = new CreateTodoCommand { Title = "Buy Milk" };

			var result = await validator.ValidateAsync(command);

			Assert.True(result.IsValid);
		}

		[Fact]
		public async Task Should_Fail_When_Title_Exceeds_100_Characters()
		{
			var validator = new CreateTodoCommandValidator();
			var longTitle = new string('A', 101);
			var command = new CreateTodoCommand { Title = longTitle };

			var result = await validator.ValidateAsync(command);

			Assert.False(result.IsValid);
		}

		[Fact]
		public void New_TodoItem_Should_Have_IsCompleted_False_By_Default()
		{
			var todo = new TodoItem { Title = "Test" };

			Assert.False(todo.IsCompleted);
		}

		[Fact]
		public void Command_Should_Map_Title_Correctly()
		{
			var title = "Learn Unit Testing";
			var command = new CreateTodoCommand { Title = title };

			Assert.Equal(title, command.Title);
		}
	}
}
