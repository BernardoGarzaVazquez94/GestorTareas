using GestorTareas.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace GestorTareas.Server.Data
{
	public class GestorTareasContext :DbContext
	{
		public GestorTareasContext(DbContextOptions<GestorTareasContext> options) : base(options) { }

		public DbSet<Usuarios> Usuarios { get; set; }
		public DbSet<Tareas> Tareas { get; set; }
	}
}
