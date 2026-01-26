using FluentValidation;
using Stage_4.DTOs;

namespace Stage_4.Validators;

public class TodoCreateDtoValidator : AbstractValidator<TodoCreateDto>
{
	public TodoCreateDtoValidator()
	{
		RuleFor(x => x.Title)
			.NotEmpty().WithMessage("Title dena zaroori hai!")
			.MinimumLength(5).WithMessage("Title kam se kam 5 characters ka hona chahiye!");
	}
}