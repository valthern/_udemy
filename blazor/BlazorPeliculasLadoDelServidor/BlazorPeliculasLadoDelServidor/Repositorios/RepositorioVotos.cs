using AutoMapper;
using BlazorPeliculasLadoDelServidor.Data;
using BlazorPeliculasLadoDelServidor.DTOs;
using BlazorPeliculasLadoDelServidor.Entidades;
using BlazorPeliculasLadoDelServidor.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlazorPeliculasLadoDelServidor.Repositorios
{
    public class RepositorioVotos
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<IdentityUser> userManager;
        private readonly IMapper mapper;
        private readonly AuthenticationStateService authenticationStateService;

        public RepositorioVotos(ApplicationDbContext context, 
            UserManager<IdentityUser> userManager, 
            IMapper mapper, 
            AuthenticationStateService authenticationStateService)
        {
            this.context = context;
            this.userManager = userManager;
            this.mapper = mapper;
            this.authenticationStateService = authenticationStateService;
        }

        public async Task Votar(VotoPeliculaDTO votoPeliculaDTO)
        {
            var usuarioId = await authenticationStateService.GetCurrentUserId();

            if (usuarioId is null) return;

            var votoActual = await context.VotosPeliculas
                .FirstOrDefaultAsync(x => x.PeliculaId == votoPeliculaDTO.PeliculaId && x.UsuarioId == usuarioId);

            if (votoActual is null)
            {
                var votoPelicula = mapper.Map<VotoPelicula>(votoPeliculaDTO);
                votoPelicula.UsuarioId = usuarioId!;
                votoPelicula.FechaVoto = DateTime.Now;
                context.Add(votoPelicula);
            }
            else
            {
                votoActual.FechaVoto = DateTime.Now;
                votoActual.Voto = votoPeliculaDTO.Voto;
            }
            await context.SaveChangesAsync();
        }
    }
}
