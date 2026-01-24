using BlazorPeliculasLadoDelServidor.Data;
using BlazorPeliculasLadoDelServidor.Entidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlazorPeliculasLadoDelServidor.Repositorios
{
    public class RepositorioGeneros
    {
        private readonly ApplicationDbContext context;

        public RepositorioGeneros(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<List<Genero>> Get() => await context.Generos
            .AsNoTracking()
            .ToListAsync();

        public async Task<Genero?> Get(int id) => await context.Generos
            .AsNoTracking()
            .FirstOrDefaultAsync(genero => genero.Id == id);

        public async Task<int> Post(Genero genero)
        {
            context.Add(genero);
            await context.SaveChangesAsync();
            return genero.Id;
        }

        public async Task Put(Genero genero)
        {
            context.Update(genero);
            await context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var filasAfectadas = await context.Generos
                .Where(g => g.Id == id)
                .ExecuteDeleteAsync();
        }
    }
}
