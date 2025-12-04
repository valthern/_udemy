using BlazorPeliculas.Server.Helpers;
using BlazorPeliculas.Shared.DTOs;
using BlazorPeliculas.Shared.Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlazorPeliculas.Server.Controllers
{
    [ApiController]
    [Route("api/peliculas")]
    public class PeliculasController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IAlmacenadorArchivos almacenadorArchivos;
        private readonly string contenedor = "peliculas";

        public PeliculasController(ApplicationDbContext context, IAlmacenadorArchivos almacenadorArchivos)
        {
            this.context = context;
            this.almacenadorArchivos = almacenadorArchivos;
        }

        [HttpGet]
        public async Task<ActionResult<HomePageDTO>> Get()
        {
            var limite = 6;

            return new HomePageDTO
            {
                PeliculasEnCartelera = await context.Peliculas
                    .Where(pelicula => pelicula.EnCartelera)
                    .Take(limite)
                    .OrderByDescending(pelicula => pelicula.Lanzamiento)
                    .ToListAsync(),
                ProximosEstrenos = await context.Peliculas
                    .Where(pelicula => pelicula.Lanzamiento > DateTime.Today)
                    .Take(limite)
                    .OrderBy(pelicula => pelicula.Lanzamiento)
                    .ToListAsync()
            };
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<PeliculaVisualizarDTO>> Get(int id)
        {
            var pelicula = await context.Peliculas
                .Where(p => p.Id == id)
                .Include(p => p.GenerosPelicula)
                    .ThenInclude(gp => gp.Genero)
                .Include(p => p.PeliculasActor.OrderBy(pa => pa.Orden))
                    .ThenInclude(pa => pa.Actor)
                .FirstOrDefaultAsync();

            if (pelicula is null) return NotFound();

            // TODO: Sistema de votación
            var promedioVoto = 4;
            var votoUsuario = 5;

            var modelo = new PeliculaVisualizarDTO();

        }

        [HttpPost]
        public async Task<ActionResult<int>> Post(Pelicula pelicula)
        {
            if (!string.IsNullOrWhiteSpace(pelicula.Poster))
            {
                var poster = Convert.FromBase64String(pelicula.Poster);
                pelicula.Poster = await almacenadorArchivos.GuardarArchivo(poster, ".jpg", contenedor);
            }

            if (pelicula.PeliculasActor is not null)
            {
                for (int i = 0; i < pelicula.PeliculasActor.Count; i++)
                    pelicula.PeliculasActor[i].Orden = i + 1;
            }

            context.Add(pelicula);
            await context.SaveChangesAsync();
            return pelicula.Id;
        }
    }
}
