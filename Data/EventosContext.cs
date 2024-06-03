using ApiExamen2AWS.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiExamen2AWS.Data
{
    public class EventosContext:DbContext
    {
        public EventosContext(DbContextOptions<EventosContext> options)
          : base(options) { }
        public DbSet<Evento> Eventos { get; set; }
        public DbSet<CategoriaEvento> Categorias { get; set; }
    }
}
