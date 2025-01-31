﻿using BlazorPeliculas.Client.Repositorios;
using BlazorPeliculas.Shared.Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlazorPeliculas.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GenerosController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public GenerosController(ApplicationDbContext context) =>
            this.context = context;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Genero>>> Get() =>
            await context.Generos.ToListAsync();

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Genero>> Get(int id)
        {
            var genero = await context.Generos.FirstOrDefaultAsync(g => g.Id == id);

            if (genero is null) return NotFound();

            return genero;
        }

        [HttpPost]
        public async Task<ActionResult<int>> Post(Genero genero)
        {
            context.Add(genero);
            await context.SaveChangesAsync();
            return genero.Id;
        }

        [HttpPut]
        public async Task<ActionResult> Put(Genero genero)
        {
            context.Update(genero);
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var filasAfectadas = await context.Generos
                .Where(g => g.Id == id)
                .ExecuteDeleteAsync();

            if (filasAfectadas == 0) return NotFound();

            return NoContent();
        }
    }
}
