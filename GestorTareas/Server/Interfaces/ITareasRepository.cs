using GestorTareas.Server.Models;

namespace GestorTareas.Server.Interfaces
{
    public interface ITareasRepository
    {
        Task<Tareas> actualizarTareaPorId(int id, Tareas tarea);
        Task<Tareas> agregarNuevaTarea(Tareas tarea);
        Task<Tareas> eliminarTareaPorId(int id);
        Task<Tareas> obtenerTareaPorId(int id);
        Task<List<Tareas>> obtenerTareas();
    }
}
