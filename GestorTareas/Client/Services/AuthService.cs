using GestorTareas.Shared.Models;
using System.Net.Http.Json;

namespace GestorTareas.Client.Services
{
    public class AuthService
    {
        private readonly HttpClient _httpClient;


        public AuthService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        /// <summary>
        /// Es la función que conecta la vista con el servidor para el inicio de sesión y generar el token JWT
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<string> Login(UsuarioDTO user)
        {
            var response = await _httpClient.PostAsJsonAsync("api/auth/login", user);

            if (response.IsSuccessStatusCode)
            {
                response.EnsureSuccessStatusCode();
                var result = await response.Content.ReadFromJsonAsync<AuthResponseDTO>();
                return result?.Token; 
            }
            else
            {
                return null;
            }

            
        }

        /// <summary>
        /// Función que conecta la vista con el servidor para generar una nueva cuenta de inicio de sesión
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task Register(UsuarioDTO user)
        {
            var response = await _httpClient.PostAsJsonAsync("api/auth/register", user);
            response.EnsureSuccessStatusCode();
        }
    }
}
