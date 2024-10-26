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

        [HttpPost]
        public async Task<ActionResult> Post(Genero genero)
        {
            var estatus1 = context.Entry(genero).State;
            context.Add(genero);
            var estatus2 = context.Entry(genero).State;
            await context.SaveChangesAsync();
            var estatus3 = context.Entry(genero).State;
            return Ok();
        }

        [HttpPost("varios")]
        public async Task<ActionResult> Post(Genero[] generos)
        {
            context.AddRange(generos);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpPost("agregar2")]
        public async Task<ActionResult> Agregar2(int id)
        {
            var genero = await context.Generos.AsTracking().SingleOrDefaultAsync(g => g.Identificador == id);
            if (genero is null) return NotFound();
            genero.Nombre += " 2";
            await context.SaveChangesAsync();
            return Ok();
        }
    }
}
