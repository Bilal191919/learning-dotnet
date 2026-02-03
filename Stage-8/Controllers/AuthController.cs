using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Stage_7.Domain;
using Stage_7.Infrastructure;
using Stage_4.DTOs;

namespace Stage_4.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		private readonly AppDbContext _context;
		private readonly IConfiguration _configuration;

		public AuthController(AppDbContext context, IConfiguration configuration)
		{
			_context = context;
			_configuration = configuration;
		}

		[HttpPost("register")]
		public async Task<ActionResult<User>> Register(UserDto request)
		{
			if (await _context.Users.AnyAsync(u => u.Username == request.Username))
				return BadRequest("User already exists.");

			var user = new User
			{
				Username = request.Username,
				PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password)
			};

			_context.Users.Add(user);
			await _context.SaveChangesAsync();

			return Ok(user);
		}

		[HttpPost("login")]
		public async Task<ActionResult<string>> Login(UserDto request)
		{
			var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == request.Username);
			if (user == null) return BadRequest("User not found.");
			if (!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash)) return BadRequest("Wrong password.");

			string token = CreateToken(user);
			return Ok(token);
		}

		// 👇 YAHAN CHANGE KIYA HAI (HmacSha256Signature)
		private string CreateToken(User user)
		{
			List<Claim> claims = new List<Claim>
			{
				new Claim(ClaimTypes.Name, user.Username),
				new Claim(ClaimTypes.Role, "Admin")
			};

			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("YeWahiSecretKeyHaiJoControllerMeinThi123!"));

			// ⚠️ FIX: Sha512 ki jagah Sha256 use kiya hai jo is key ke sath chalta hai
			var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

			var token = new JwtSecurityToken(
					issuer: "https://localhost:7221",
					audience: "https://localhost:7221",
					claims: claims,
					expires: DateTime.Now.AddDays(1),
					signingCredentials: creds
				);

			return new JwtSecurityTokenHandler().WriteToken(token);
		}
	}
}