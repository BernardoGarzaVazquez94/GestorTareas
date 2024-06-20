using GestorTareas.Server.Data;
using GestorTareas.Server.Interfaces;
using GestorTareas.Server.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace GestorTareas.Server.Repository
{
    public class TareasRepository: ITareasRepository
    {
        private readonly GestorTareasContext _context;

        public TareasRepository(GestorTareasContext context)
        {
            _context = context;
        }

        public async Task<Tareas> actualizarTareaPorId(int id, Tareas tarea)
        {
            try
            {
                Tareas tareaActualizada = await _context.Tareas.Where(p => p.Id == id).FirstOrDefaultAsync();

                if (tareaActualizada != null)
                {
                    tareaActualizada.Titulo = tarea.Titulo;
                    tareaActualizada.Descripcion = tarea.Descripcion;
                    tareaActualizada.FechaVencimiento = tarea.FechaVencimiento;

                    await _context.SaveChangesAsync();

                    return tareaActualizada;
                }
                else
                {
                    return null;
                }


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Función para agregar una nueva tarea
        /// </summary>
        /// <param name="tarea"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<Tareas> agregarNuevaTarea(Tareas tarea)
        {
            try
            {
                //Agrega el modelo tarea a la base de datos
                     _context.Tareas.Add(tarea);
                await _context.SaveChangesAsync();

                return tarea;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Funcion para eliminar una tarea por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Tareas> eliminarTareaPorId(int id)
        {
            try
            {
                //Se busca la tarea por el ID
                Tareas tareaEliminada = await _context.Tareas.FindAsync(id);

                //Si la consulta regresa una tarea procede a eliminarla
                if (tareaEliminada != null)
                {
                    _context.Tareas.Remove(tareaEliminada);
                    await _context.SaveChangesAsync();
                    return tareaEliminada;
                }
                else
                {
                    return null;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Obtiene una tarea en especifico por el ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<Tareas> obtenerTareaPorId(int id)
        {
            try
            {
                Tareas tarea = await _context.Tareas.FindAsync(id);

                return tarea;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Funcionalidad para obtener la lista de tareas de la tabla Tareas
        /// </summary>
        /// <returns></returns>
        public async Task<List<Tareas>> obtenerTareas()
        {
            try
            {
                List<Tareas> tareas = await _context.Tareas.ToListAsync();
                return tareas;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}
