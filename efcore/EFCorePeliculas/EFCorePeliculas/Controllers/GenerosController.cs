using EFCorePeliculas.Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EFCorePeliculas.Controllers
{
    [Route("api/generos")]
    [ApiController]
    public class GenerosController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public GenerosController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<IEnumerable<Genero>> Get()
        {
            return await context.Generos.OrderByDescending(g => g.Nombre).ToListAsync();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Genero>> Get(int id)
        {
            var genero = await context.Generos.SingleOrDefaultAsync(g => g.Identificador == id);

            if (genero is null) return NotFound();

            return genero;
        }

        [HttpGet("primer")]
        public async Task<ActionResult<Genero>> Primer()
        {
            var genero = await context.Generos.FirstOrDefaultAsync(g => g.Nombre.StartsWith("C"));

            if (genero is null) return NotFound();

            return genero;
        }

        [HttpGet("filtrar")]
        public async Task<IEnumerable<Genero>> Filtrar(string nombre)
        {
            return await context.Generos
                .Where(g => g.Nombre.Contains(nombre))
                .ToListAsync();
        }

        [HttpGet("paginacion")]
        public async Task<ActionResult<IEnumerable<Genero>>> Getpaginacion(int pagina = 1)
        {
            var cantidadRegistrosPorPagina = 2;
            var generos = await context.Generos
                .Skip((pagina-1)*cantidadRegistrosPorPagina)
                .Take(cantidadRegistrosPorPagina)
                .ToListAsync();
            return generos;
        }
    }
}
