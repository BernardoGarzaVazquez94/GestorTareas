namespace GestorTareas.Shared.Models
{
	public class TareasDTO
	{
		public int Id { get; set; }
		public string Titulo { get; set; }
		public string Descripcion { get; set; }
		public DateTime FechaVencimiento { get; set; }
		public int UsuarioId { get; set; }
		public UsuarioDTO Usuario { get; set; }
	}
}
