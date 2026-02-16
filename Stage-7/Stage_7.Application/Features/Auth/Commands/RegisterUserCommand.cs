using MediatR;
using Stage_7.Application;
using Stage_7.Domain;

namespace Stage_7.Application.Features.Auth.Commands;

public record RegisterUserCommand(string Username, string Email, string Password) : IRequest<int>;

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, int>
{
	private readonly IAppDbContext _context;

	public RegisterUserCommandHandler(IAppDbContext context)
	{
		_context = context;
	}

	public async Task<int> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
	{
	
		var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

		var user = new User
		{
			Username = request.Username,
			Email = request.Email,
			PasswordHash = passwordHash
		};

		_context.Users.Add(user);
		await _context.SaveChangesAsync(cancellationToken);

		return user.Id;
	}
}