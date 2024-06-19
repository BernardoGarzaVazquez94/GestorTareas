using GestorTareas.Server.Data;
using GestorTareas.Server.Models;
using GestorTareas.Shared;
using GestorTareas.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GestorTareas.Server.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class TareasController : ControllerBase
	{
		private readonly GestorTareasContext _context;

		public TareasController(GestorTareasContext context)
		{
			_context = context;
		}

		/// <summary>
		/// EP para obtener la lista de todas las tareas
		/// </summary>
		/// <returns></returns>
		[HttpGet]
        [Route("obtener-tareas")]
        public async Task<ActionResult<IEnumerable<Tareas>>> ObtenerTareas()
		{
			return await _context.Tareas.ToListAsync();
		}

		/// <summary>
		/// EP para obtener una tarea en especifico
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[HttpGet]
        [Route("obtener-tarea-porId")]
        public async Task<ActionResult<Tareas>> ObtenerTareaPorId(int id)
		{
			var task = await _context.Tareas.FindAsync(id);
			if (task == null)
			{
				return NotFound();
			}
			return task;
		}

		/// <summary>
		/// EP para agregar una tarea
		/// </summary>
		/// <param name="task"></param>
		/// <returns></returns>
		[HttpPost]
        [Route("agregar-tarea")]
        public async Task<ActionResult<Tareas>> AgregarTarea(Tareas task)
		{

			task.UsuarioId = 1;

            _context.Tareas.Add(task);
			await _context.SaveChangesAsync();
			return CreatedAtAction(nameof(ObtenerTareaPorId), new { id = task.Id }, task);
		}

		/// <summary>
		/// EP para actualizar la información de una tarea
		/// </summary>
		/// <param name="id"></param>
		/// <param name="task"></param>
		/// <returns></returns>
		[HttpPut]
        [Route("actualizar-tarea-porId")]
        public async Task<IActionResult> ActualizarTareaPorId(int id, Tareas task)
		{

			var tarea = await _context.Tareas.Where(p=> p.Id == id).FirstOrDefaultAsync();

			if (tarea == null)
			{
				return BadRequest();
			}
			else
			{
				tarea.Titulo = task.Titulo;
				tarea.Descripcion = task.Descripcion;
				tarea.FechaVencimiento = task.FechaVencimiento;
            }			
			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!TaskExists(id))
				{
					return NotFound();
				}
				else
				{
					throw;
				}
			}
            return CreatedAtAction(nameof(ObtenerTareaPorId), new { id = task.Id }, task);
        }

		/// <summary>
		/// Ep para eliminar una tarea por id de la tarea
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[HttpDelete]
        [Route("eliminar-tarea-porId")]
        public async Task<IActionResult> DeleteTask(int id)
		{
			var task = await _context.Tareas.FindAsync(id);
			if (task == null)
			{
				return NotFound();
			}
			_context.Tareas.Remove(task);
			await _context.SaveChangesAsync();
			return NoContent();
		}

		private bool TaskExists(int id)
		{
			return _context.Tareas.Any(e => e.Id == id);
		}


       

    }
}
