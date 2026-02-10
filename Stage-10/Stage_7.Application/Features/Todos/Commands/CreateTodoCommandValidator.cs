using FluentValidation;

namespace Stage_7.Application.Features.Todos.Commands
{
	public class CreateTodoCommandValidator : AbstractValidator<CreateTodoCommand>
	{
		public CreateTodoCommandValidator()
		{
			RuleFor(x => x.Title)
				.NotEmpty()
				.NotNull()
				.MaximumLength(50);
		}
	}
}