using GestorTareas.Shared.Models;
using System.Net.Http.Json;

namespace GestorTareas.Client.Services
{
	public class TareaService
	{
		private readonly HttpClient _httpClient;

		public TareaService(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}

		public async Task<IEnumerable<TareasDTO>> GetTasks()
		{
			return await _httpClient.GetFromJsonAsync<IEnumerable<TareasDTO>>("api/tasks");
		}

		public async Task<TareasDTO> GetTask(int id)
		{
			return await _httpClient.GetFromJsonAsync<TareasDTO>($"api/tasks/{id}");
		}

		public async Task CreateTask(TareasDTO task)
		{
			var response = await _httpClient.PostAsJsonAsync("api/tasks", task);
			response.EnsureSuccessStatusCode();
		}

		public async Task UpdateTask(TareasDTO task)
		{
			var response = await _httpClient.PutAsJsonAsync($"api/tasks/{task.Id}", task);
			response.EnsureSuccessStatusCode();
		}

		public async Task DeleteTask(int id)
		{
			var response = await _httpClient.DeleteAsync($"api/tasks/{id}");
			response.EnsureSuccessStatusCode();
		}
	}
}
