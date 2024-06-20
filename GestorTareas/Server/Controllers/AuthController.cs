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

		/// <summary>
		/// EP para registrar nuevos usuarios en la base de datos
		/// Usuario que no este en la base de datos no podra acceder a las funciones de la app
		/// </summary>
		/// <param name="user"></param>
		/// <returns></returns>
		[HttpPost("register")]
		public async Task<IActionResult> Register(Usuarios user)
		{
			try
			{
                user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);//Se aplica un hash al password para encriptarla y guardarla 

                _context.Usuarios.Add(user);
                await _context.SaveChangesAsync();

                return Ok();
            }
			catch (Exception ex)
			{
                Console.WriteLine(ex.ToString());
                throw;
			}
			
		}

		/// <summary>
		/// EP para inicio de sesión, únicamente acceden usuarios dados de alta usuarios de la tabla usuarios
		/// Este EP sirve para generar el Bearer Token, puede usarse por si necesitan generar token y usar los EP desde POSTMAN
		/// </summary>
		/// <param name="login"></param>
		/// <returns></returns>
		[HttpPost("login")]
		public async Task<IActionResult> Login(Usuarios login)
		{
			try
			{
				//Buscamos al usuario que esta iniciando sesión
                var user = await _context.Usuarios.FirstOrDefaultAsync(u => u.Username == login.Username);

				//Si el usuario no existe o si la verificación no es correcta, el usuario no esta autorizado y no genera token
                if (user == null || !BCrypt.Net.BCrypt.Verify(login.Password, user.Password))
                {
                    return Unauthorized();
                }

				//Iniciamos la instancia de la clase JWTSecurityTokenHandler
                var tokenHandler = new JwtSecurityTokenHandler();

				//Obtenemos la llave que esta en el archivo appsettings.JSON
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

				//Creamos el token 
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var tokenString = tokenHandler.WriteToken(token);

				//Retornamos el token de acceso
                return Ok(new { Token = tokenString });
            }
			catch (Exception ex)
			{
				Console.WriteLine(ex.ToString());
				throw;
			}
			
		}
	}

	
	}

