using ApiExamen2AWS.Models;
using ApiExamen2AWS.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiExamen2AWS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventosController : ControllerBase
    {
        private RepositoryEventos repo;

        public EventosController(RepositoryEventos repo)
        {
            this.repo = repo;
        }

        [HttpGet]
        public async Task<ActionResult<List<Evento>>>
            Get()
        {
            return await this.repo.GetEventosAsync();
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<ActionResult<List<CategoriaEvento>>>
            GetCategoriasEventos()
        {
            return await this.repo.GetCategoriasEventosAsync();
        }

        [HttpGet]
        [Route("[action]/{id}")]
        public async Task<ActionResult<List<Evento>>>
            GetEventosConCategorias(int id)
        {
            return await this.repo.GetEventosConCategoriasAsync(id);
        }
    }
}
