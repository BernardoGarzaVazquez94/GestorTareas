using GestorTareas.Shared.Models;
using System.Net.Http.Json;

namespace GestorTareas.Client.Helpers
{
	public class AuthService
	{
		private readonly HttpClient _httpClient;

		public AuthService(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}

		public async Task<string> Login(UsuarioDTO usuario)
		{
			var response = await _httpClient.PostAsJsonAsync("api/auth/login", usuario);
			response.EnsureSuccessStatusCode();
			var result = await response.Content.ReadFromJsonAsync<Dictionary<string, string>>();
			return result["Token"];
		}

		public async Task Register(UsuarioDTO usuario)
		{
			var response = await _httpClient.PostAsJsonAsync("api/auth/register", usuario);
			response.EnsureSuccessStatusCode();
		}
	}
}
