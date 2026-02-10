using AutoMapper;
using BlazorPeliculasLadoDelServidor.Data;
using BlazorPeliculasLadoDelServidor.DTOs;
using BlazorPeliculasLadoDelServidor.Entidades;
using BlazorPeliculasLadoDelServidor.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlazorPeliculasLadoDelServidor.Repositorios
{
    public class RepositorioPeliculas
    {
        private readonly ApplicationDbContext context;
        private readonly IAlmacenadorArchivos almacenadorArchivos;
        private readonly IMapper mapper;
        private readonly UserManager<IdentityUser> userManager;
        private readonly AuthenticationStateService authenticationStateService;
        private readonly string contenedor = "peliculas";

        public RepositorioPeliculas(ApplicationDbContext context, IAlmacenadorArchivos almacenadorArchivos, IMapper mapper, UserManager<IdentityUser> userManager, AuthenticationStateService authenticationStateService)
        {
            this.context = context;
            this.almacenadorArchivos = almacenadorArchivos;
            this.mapper = mapper;
            this.userManager = userManager;
            this.authenticationStateService = authenticationStateService;
        }

        public async Task<HomePageDTO> Get()
        {
            var limite = 6;

            return new HomePageDTO
            {
                PeliculasEnCartelera = await context.Peliculas
                    .Where(pelicula => pelicula.EnCartelera)
                    .OrderByDescending(pelicula => pelicula.Lanzamiento)
                    .Take(limite)
                    .ToListAsync(),
                ProximosEstrenos = await context.Peliculas
                    .Where(pelicula => pelicula.Lanzamiento > DateTime.Today)
                    .OrderBy(pelicula => pelicula.Lanzamiento)
                    .Take(limite)
                    .ToListAsync()
            };
        }

        public async Task<PeliculaVisualizarDTO> Get(int id)
        {
            var pelicula = await context.Peliculas
                .Where(p => p.Id == id)
                .Include(p => p.GenerosPelicula)
                    .ThenInclude(gp => gp.Genero)
                .Include(p => p.PeliculasActor.OrderBy(pa => pa.Orden))
                    .ThenInclude(pa => pa.Actor)
                .FirstOrDefaultAsync();

            if (pelicula is null) return null;

            var promedioVoto = 0.0;
            var votoUsuario = 0;

            if (await context.VotosPeliculas.AnyAsync(vp => vp.PeliculaId == id))
            {
                promedioVoto = await context.VotosPeliculas
                    .Where(vp => vp.PeliculaId == id)
                    .AverageAsync(vp => vp.Voto);

                var userId = await authenticationStateService.GetCurrentUserId();

                if (userId is not null)
                {
                    //var usuario = await userManager
                    //    .FindByEmailAsync(HttpContext.User.Identity!.Name!);

                    //if (usuario is null) return BadRequest("Usuario no encontrado");

                    //var usuarioId = usuario.Id;

                    var votoUsuarioDB = await context.VotosPeliculas
                        .FirstOrDefaultAsync(vp => vp.PeliculaId == id && vp.UsuarioId == userId);

                    if (votoUsuarioDB is not null)
                        votoUsuario = votoUsuarioDB.Voto;
                }
            }

            return new PeliculaVisualizarDTO
            {
                Pelicula = pelicula,
                Generos = pelicula.GenerosPelicula
                    .Select(gp => gp.Genero!).ToList(),
                Actores = pelicula.PeliculasActor
                    .Select(pa => new Actor
                    {
                        Id = pa.Actor!.Id,
                        Nombre = pa.Actor.Nombre,
                        Foto = pa.Actor.Foto,
                        Personaje = pa.Personaje
                    }).ToList(),
                PromedioVotos = promedioVoto,
                VotoUsuario = votoUsuario
            };
        }

        public async Task<RespuestaPaginadaDTO<Pelicula>> Get(ParametrosBusquedaPeliculasDTO modelo)
        {
            var peliculasQueryable = context.Peliculas.AsQueryable();

            if (!string.IsNullOrWhiteSpace(modelo.Titulo))
                peliculasQueryable = peliculasQueryable
                    .Where(p => p.Titulo.Contains(modelo.Titulo));

            if (modelo.EnCartelera)
                peliculasQueryable = peliculasQueryable
                    .Where(p => p.EnCartelera);

            if (modelo.Estrenos)
                peliculasQueryable = peliculasQueryable
                    .Where(p => p.Lanzamiento >= DateTime.Today);

            if (modelo.GeneroId != 0)
                peliculasQueryable = peliculasQueryable
                    .Where(p => p.GenerosPelicula!
                        .Select(gp => gp.GeneroId)
                        .Contains(modelo.GeneroId));

            if (modelo.MasVotadas)
                peliculasQueryable = peliculasQueryable
                    .OrderByDescending(p => p.VotosPeliculas.Average(vp => vp.Voto));

            return new RespuestaPaginadaDTO<Pelicula>
            {
                TotalPaginas = await peliculasQueryable
                    .CalcularTotalPaginas(modelo.CantidadRegistros),
                Registros = await peliculasQueryable
                    .Paginar(modelo.PaginacionDTO)
                    .ToListAsync()
            };
        }

        public async Task<PeliculaActualizacionDTO> PutGet(int id)
        {
            var peliculaActionResult = await Get(id);
            if (peliculaActionResult is null) return null;

            var peliculaVisualizarDTO = peliculaActionResult;
            var generosSeleccionadosIds = peliculaVisualizarDTO!.Generos.Select(g => g.Id).ToList();
            var generosNoSeleccionados = await context.Generos
                .Where(g => !generosSeleccionadosIds.Contains(g.Id))
                .ToListAsync();

            return new PeliculaActualizacionDTO
            {
                Pelicula = peliculaVisualizarDTO.Pelicula,
                Actores = peliculaVisualizarDTO.Actores,
                GenerosSeleccionados = peliculaVisualizarDTO.Generos,
                GenerosNoSeleccionados = generosNoSeleccionados
            };
        }

        public async Task<ActionResult<int>> Post(Pelicula pelicula)
        {
            if (!string.IsNullOrWhiteSpace(pelicula.Poster))
            {
                var poster = Convert.FromBase64String(pelicula.Poster);
                pelicula.Poster = await almacenadorArchivos.GuardarArchivo(poster, ".jpg", contenedor);
            }

            EscribirOrdenActores(pelicula);

            context.Add(pelicula);
            await context.SaveChangesAsync();
            return pelicula.Id;
        }

        private static void EscribirOrdenActores(Pelicula pelicula)
        {
            if (pelicula.PeliculasActor is not null)
                for (int i = 0; i < pelicula.PeliculasActor.Count; i++)
                    pelicula.PeliculasActor[i].Orden = i + 1;
        }

        public async Task<ActionResult> Put(Pelicula pelicula)
        {
            var peliculaDB = await context.Peliculas
                .Include(p => p.GenerosPelicula)
                .Include(p => p.PeliculasActor)
                .FirstOrDefaultAsync(p => p.Id == pelicula.Id);

            if (peliculaDB is null) return NotFound();

            peliculaDB = mapper.Map(pelicula, peliculaDB);

            if (!string.IsNullOrWhiteSpace(pelicula.Poster))
            {
                var poster = Convert.FromBase64String(pelicula.Poster);
                peliculaDB.Poster = await almacenadorArchivos.EditarArchivo(poster, ".jpg", contenedor, peliculaDB.Poster!);
            }

            EscribirOrdenActores(peliculaDB);

            await context.SaveChangesAsync();
            return NoContent();
        }

        public async Task<ActionResult> Delete(int id)
        {
            var pelicula = await context.Peliculas.FirstOrDefaultAsync(a => a.Id == id);

            if (pelicula is null) return NotFound();

            context.Remove(pelicula);
            await context.SaveChangesAsync();
            await almacenadorArchivos.EliminarArchivo(pelicula.Poster!, contenedor);

            return NoContent();
        }
    }
}
