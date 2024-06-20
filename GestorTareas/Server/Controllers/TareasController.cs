using GestorTareas.Server.Data;
using GestorTareas.Server.Interfaces;
using GestorTareas.Server.Models;
using GestorTareas.Shared;
using GestorTareas.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace GestorTareas.Server.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
    //[Authorize]-- Se comenta la linea Authorize para poder realizar el ejercicio de pruebas,
    //a traves del POSTMAN si esta funcionando los bearer token generados
    public class TareasController : ControllerBase
	{
		

		//Definición de interfaz para realizar inyección de dependencias en el codigo
		//Así como trabajar de manera modular y reutilizar funciones especificas
        private readonly ITareasRepository ITareasRepository;


        public TareasController(ITareasRepository ItareasRepository)
		{			
			ITareasRepository = ItareasRepository;
		}

        /// <summary>
        /// EP para obtener la lista de todas las tareas
        /// </summary>
        /// <returns></returns>
        ///         
        [HttpGet]
        [Route("obtener-tareas")]
        public async Task<ActionResult<IEnumerable<Tareas>>> ObtenerTareas()
		{
			try
			{
				//Obtiene toda la lista de tareas
				List<Tareas> tareas = await ITareasRepository.obtenerTareas();
				return tareas;                
            }
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				throw;
			}

			
		}

        /// <summary>
        /// EP para obtener una tarea en especifico
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ///         
        [HttpGet]
        [Route("obtener-tarea-porId")]
        public async Task<ActionResult<Tareas>> ObtenerTareaPorId(int id)
		{
			try
			{
				//Busca la tarea especifica por el id de la tarea
                Tareas tarea = await ITareasRepository.obtenerTareaPorId(id);

				//Si no encuentra la tarea se envia un notFound
                if (tarea == null)
                {
                    return NotFound();
                }
                return tarea;
            }
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				throw;
			}

			
		}

		/// <summary>
		/// EP para agregar una tarea
		/// </summary>
		/// <param name="task"></param>
		/// <returns></returns>
		[HttpPost]
        [Route("agregar-tarea")]
        public async Task<ActionResult<Tareas>> AgregarTarea(Tareas tarea)
		{

			try
			{
                tarea.UsuarioId = 1;                
                Tareas nuevaTarea = await ITareasRepository.agregarNuevaTarea(tarea);

                return CreatedAtAction(nameof(ObtenerTareaPorId), new { id = nuevaTarea.Id }, tarea);


            }
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				throw;
			}
			

            
		}

		/// <summary>
		/// EP para actualizar la información de una tarea
		/// </summary>
		/// <param name="id"></param>
		/// <param name="task"></param>
		/// <returns></returns>
		[HttpPut]
        [Route("actualizar-tarea-porId")]
        public async Task<IActionResult> ActualizarTareaPorId(int id, Tareas tarea)
		{
			try
			{
				//Busca y actualiza una tarea por el ID 
                Tareas tareaActualizada = await ITareasRepository.actualizarTareaPorId(id, tarea);

				//Si el modelo de la tarea viene null se indica que es una mala solicitud
                if (tarea == null)
                {
                    return BadRequest();
                }
                else
                {
					//Si se actualizo correctamente se manda la respuesta JSON de la tarea actualizada
                    if (tareaActualizada != null)
                    {
                        return CreatedAtAction(nameof(ObtenerTareaPorId), new { id = tareaActualizada.Id }, tarea);
                    }
                    else
                    {
                        return NotFound();
                    }
                }
            }
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				throw;
			}			                        
        }

		/// <summary>
		/// Ep para eliminar una tarea por id de la tarea
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[HttpDelete]
        [Route("eliminar-tarea-porId")]
        public async Task<IActionResult> BorrarTarea(int id)
		{
			try
			{
				//Funcion para eliminar una tarea por el ID 
				Tareas tareaEliminada = await ITareasRepository.eliminarTareaPorId(id);
                
                if (tareaEliminada == null)
                {
                    return NotFound();
                }
                
                return NoContent();
            }
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				throw;
			}

			
		}
	       
    }
}
