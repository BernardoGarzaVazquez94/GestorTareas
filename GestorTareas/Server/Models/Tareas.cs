using GestorTareas.Shared.Models;

namespace GestorTareas.Server.Models
{
    public class Tareas
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public int UsuarioId { get; set; }
    }
}
