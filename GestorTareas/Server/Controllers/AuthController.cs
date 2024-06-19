using GestorTareas.Server.Data;
using GestorTareas.Server.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GestorTareas.Server.Controllers
{
	// Controllers/AuthController.cs
	[ApiController]
	[Route("api/[controller]")]
	public class AuthController : ControllerBase
	{
		private readonly GestorTareasContext _context;
		private readonly IConfiguration _configuration;

		public AuthController(GestorTareasContext context, IConfiguration configuration)
		{
			_context = context;
			_configuration = configuration;
		}

		[HttpPost("register")]
		public async Task<IActionResult> Register(Usuarios user)
		{
			user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
			_context.Usuarios.Add(user);
			await _context.SaveChangesAsync();
			return Ok();
		}

		[HttpPost("login")]
		public async Task<IActionResult> Login(Usuarios login)
		{
			var user = await _context.Usuarios.FirstOrDefaultAsync(u => u.Username == login.Username);
			if (user == null || !BCrypt.Net.BCrypt.Verify(login.Password, user.Password))
			{
				return Unauthorized();
			}

			var tokenHandler = new JwtSecurityTokenHandler();
			var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);
			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(new Claim[]
				{
				new Claim(ClaimTypes.Name, user.Username)
				}),
				Expires = DateTime.UtcNow.AddMinutes(int.Parse(_configuration["Jwt:DurationInMinutes"])),
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
				Issuer = _configuration["Jwt:Issuer"],
				Audience = _configuration["Jwt:Audience"]
			};

			var token = tokenHandler.CreateToken(tokenDescriptor);
			var tokenString = tokenHandler.WriteToken(token);

			return Ok(new { Token = tokenString });
		}
	}

	
	}

