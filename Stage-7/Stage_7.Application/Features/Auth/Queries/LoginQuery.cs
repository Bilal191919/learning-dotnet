using MediatR;
using Microsoft.EntityFrameworkCore;
using Stage_7.Application;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Stage_7.Application.Features.Auth.Queries;

public record LoginQuery(string Username, string Password) : IRequest<string>;

public class LoginQueryHandler : IRequestHandler<LoginQuery, string>
{
	private readonly IAppDbContext _context;
	private readonly IConfiguration _configuration;

	public LoginQueryHandler(IAppDbContext context, IConfiguration configuration)
	{
		_context = context;
		_configuration = configuration;
	}

	public async Task<string> Handle(LoginQuery request, CancellationToken cancellationToken)
	{
		var user = await _context.Users
			.FirstOrDefaultAsync(u => u.Username == request.Username, cancellationToken);

		if (user == null) throw new Exception("User not found!");

		bool isPasswordValid = BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash);
		if (!isPasswordValid) throw new Exception("Invalid password!");

		var authClaims = new List<Claim>
		{
			new Claim(ClaimTypes.Name, user.Username),
			new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
			new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
		};

		var keyString = _configuration["Jwt:Key"]!;
		var issuer = _configuration["Jwt:Issuer"];
		var audience = _configuration["Jwt:Audience"];

		Console.WriteLine($"LOGIN - Issuer: {issuer}, Audience: {audience}, Key Length: {keyString?.Length}");

		var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keyString));

		var token = new JwtSecurityToken(
			issuer: issuer,
			audience: audience,
			expires: DateTime.Now.AddHours(3),
			claims: authClaims,
			signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
		);

		return new JwtSecurityTokenHandler().WriteToken(token);
	}
}