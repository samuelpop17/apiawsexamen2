using ApiExamen2AWS.Data;
using ApiExamen2AWS.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiExamen2AWS.Repositories
{
    public class RepositoryEventos
    {
        private EventosContext context;

        public RepositoryEventos(EventosContext context)
        {
            this.context = context;
        }

        public async Task<List<Evento>> GetEventosAsync()
        {
            return await this.context.Eventos.ToListAsync();
        }
        public async Task<List<CategoriaEvento>> GetCategoriasEventosAsync()
        {
            return await this.context.Categorias.ToListAsync();
        }

        public async Task<List<Evento>> GetEventosConCategoriasAsync(int id)
        {
            return await this.context.Eventos.Where(x=>x.IdCategoria == id).ToListAsync();
        }
    }
}
