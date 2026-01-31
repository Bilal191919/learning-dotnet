using Microsoft.AspNetCore.Mvc;
using Stage_4.Data;  
using Stage_4.Models; 
using Microsoft.IdentityModel.Tokens; 
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims; 
using System.Text; 

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
		public IActionResult Register(UserDto request)
		{
			if (string.IsNullOrEmpty(request.Password))
			{
				return BadRequest("Password is required.");
			}

		
			if (_context.Users.Any(u => u.Username == request.Username))
			{
				return BadRequest("User already exists.");
			}

			string passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

			var user = new Stage_4.Models.User
			{
				Username = request.Username,
				PasswordHash = passwordHash
			};

			_context.Users.Add(user);
			_context.SaveChanges();

			return Ok("User Registered Successfully!");
		}

		
		[HttpPost("login")]
		public IActionResult Login(UserDto request)
		{
			var user = _context.Users.FirstOrDefault(u => u.Username == request.Username);

			if (user == null)
			{
				return BadRequest("User not found.");
			}

			if (!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
			{
				return BadRequest("Wrong password.");
			}

			string token = CreateToken(user);

			return Ok(token);
		}


		private string CreateToken(Stage_4.Models.User user)
		{
			List<Claim> claims = new List<Claim>
			{
				new Claim(ClaimTypes.Name, user.Username),
              
                new Claim("Permission", "CanManageTodos")
			};

			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
				_configuration.GetSection("JwtSettings:Key").Value!));

			var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

			var token = new JwtSecurityToken(
					claims: claims,
					expires: DateTime.Now.AddDays(1),
					signingCredentials: creds
				);

			var jwt = new JwtSecurityTokenHandler().WriteToken(token);
			return jwt;
		}
	}

	public class UserDto
	{
		public string Username { get; set; } = string.Empty;
		public string Password { get; set; } = string.Empty;
	}
}