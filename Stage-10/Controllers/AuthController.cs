using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Stage_7.Domain;
using Stage_4.DTOs;

namespace Stage_4.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		private readonly UserManager<User> _userManager;
		private readonly IConfiguration _configuration;

		public AuthController(UserManager<User> userManager, IConfiguration configuration)
		{
			_userManager = userManager;
			_configuration = configuration;
		}

		[HttpPost("register")]
		public async Task<IActionResult> Register(RegisterDto request)
		{
			var user = new User
			{
				UserName = request.Username,
				FullName = request.FullName
			};

			var result = await _userManager.CreateAsync(user, request.Password);

			if (!result.Succeeded)
			{
				return BadRequest(result.Errors);
			}

			return Ok("User registered successfully.");
		}

		[HttpPost("login")]
		public async Task<IActionResult> Login(LoginDto request)
		{
			var user = await _userManager.FindByNameAsync(request.Username);

			if (user == null || !await _userManager.CheckPasswordAsync(user, request.Password))
			{
				return Unauthorized("Invalid username or password.");
			}

			var authClaims = new List<Claim>
			{
				new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
				new Claim(ClaimTypes.Name, user.UserName)
			};

			var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

			var token = new JwtSecurityToken(
				issuer: _configuration["Jwt:Issuer"],
				audience: _configuration["Jwt:Audience"],
				expires: DateTime.Now.AddHours(3),
				claims: authClaims,
				signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
			);

			return Ok(new
			{
				token = new JwtSecurityTokenHandler().WriteToken(token),
				expiration = token.ValidTo
			});
		}
	}
}