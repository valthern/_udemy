using AutoMapper;
using BlazorPeliculasLadoDelServidor.Data;
using BlazorPeliculasLadoDelServidor.DTOs;
using BlazorPeliculasLadoDelServidor.Entidades;
using BlazorPeliculasLadoDelServidor.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlazorPeliculasLadoDelServidor.Repositorios
{
    public class RepositorioActores
    {
        private readonly ApplicationDbContext context;
        private readonly IAlmacenadorArchivos almacenadorArchivos;
        private readonly IMapper mapper;
        private readonly string contenedor = "personas";

        public RepositorioActores(ApplicationDbContext context, IAlmacenadorArchivos almacenadorArchivos, IMapper mapper)
        {
            this.context = context;
            this.almacenadorArchivos = almacenadorArchivos;
            this.mapper = mapper;
        }

        public async Task<RespuestaPaginadaDTO<Actor>> Get(PaginacionDTO paginacion)
        {
            var queryable = context.Actores.AsQueryable();
            var respuestaPaginada = new RespuestaPaginadaDTO<Actor>();
            respuestaPaginada.TotalPaginas = await queryable.CalcularTotalPaginas(paginacion.CantidadRegistros);
            respuestaPaginada.Registros = await queryable
                .AsNoTracking()
                .OrderBy(a => a.Nombre)
                .Paginar(paginacion)
                .ToListAsync();
            return respuestaPaginada;
        }

        public async Task<Actor?> Get(int id) => await context.Actores.AsNoTracking().FirstOrDefaultAsync(actor => actor.Id == id);

        public async Task<List<Actor>> Get(string textoBusqueda)
        {
            if (string.IsNullOrWhiteSpace(textoBusqueda)) return new List<Actor>();

            textoBusqueda = textoBusqueda.ToLower();

            return await context.Actores
                .AsNoTracking()
                .Where(a => a.Nombre.ToLower().Contains(textoBusqueda))
                .Take(5)
                .ToListAsync();
        }

        public async Task<int> Post(Actor actor)
        {
            if (!string.IsNullOrWhiteSpace(actor.Foto))
            {
                var fotoActor = Convert.FromBase64String(actor.Foto);
                actor.Foto = await almacenadorArchivos.GuardarArchivo(fotoActor, ".jpg", contenedor);
            }

            context.Add(actor);
            await context.SaveChangesAsync();
            return actor.Id;
        }

        public async Task Put(Actor actor)
        {
            var actorDB = await context.Actores.FirstOrDefaultAsync(a => a.Id == actor.Id) ?? throw new ApplicationException($"El actor con Id {actor.Id} no existe.");
            actorDB = mapper.Map(actor, actorDB);

            if (!string.IsNullOrWhiteSpace(actor.Foto))
            {
                var fotoActor = Convert.FromBase64String(actor.Foto);
                actorDB.Foto = await almacenadorArchivos.EditarArchivo(fotoActor, ".jpg", contenedor, actorDB.Foto!);
            }

            await context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var actor = await context.Actores.FirstOrDefaultAsync(a => a.Id == id) ?? throw new ApplicationException($"El actor con Id {id} no existe.");
            context.Remove(actor);
            await context.SaveChangesAsync();
            await almacenadorArchivos.EliminarArchivo(actor.Foto!, contenedor);
        }
    }
}
