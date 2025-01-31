﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using EFCorePeliculas.DTOs;
using EFCorePeliculas.Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EFCorePeliculas.Controllers
{
    [ApiController]
    [Route("api/actores")]
    public class ActoresController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public ActoresController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<ActorDTO>> Get()
        {
            //var actores = await context.Actores.Select(a => new ActorDTO { Id = a.Id, Titulo = a.Titulo }).ToListAsync();
            var actores = await context.Actores.ProjectTo<ActorDTO>(mapper.ConfigurationProvider).ToListAsync();

            return actores;
        }

        [HttpPost]
        public async Task<ActionResult> Post(ActorCreacionDTO actorCreacionDTO)
        {
            var actor = mapper.Map<Actor>(actorCreacionDTO);
            context.Add(actor);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(ActorCreacionDTO actorCreacionDTO, int id)
        {
            var actorBD = await context.Actores.AsTracking().FirstOrDefaultAsync(a => a.Id == id);

            if (actorBD is null) return NotFound();

            actorBD = mapper.Map(actorCreacionDTO, actorBD);
            var entry = context.Entry(actorBD);
            //await context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("desconectado/{id:int}")]
        public async Task<ActionResult> PutDesconectado(ActorCreacionDTO actorCreacionDTO, int id)
        {
            var existeActor = await context.Actores.AnyAsync(a => a.Id == id);

            if (!existeActor) return NotFound();

            var actor = mapper.Map<Actor>(actorCreacionDTO);
            actor.Id = id;
            //context.Update(actor);
            context.Entry(actor).Property(a => a.Nombre).IsModified = true;

            await context.SaveChangesAsync();
            return Ok();
        }
    }
}
