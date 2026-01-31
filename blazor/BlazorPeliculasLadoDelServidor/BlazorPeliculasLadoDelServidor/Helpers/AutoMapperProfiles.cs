using AutoMapper;
using BlazorPeliculasLadoDelServidor.DTOs;
using BlazorPeliculasLadoDelServidor.Entidades;

namespace BlazorPeliculasLadoDelServidor.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Actor, Actor>()
                .ForMember(a => a.Foto, opciones => opciones.Ignore());

            CreateMap<Pelicula, Pelicula>()
                .ForMember(a => a.Poster, opciones => opciones.Ignore());

            CreateMap<VotoPeliculaDTO, VotoPelicula>();
        }
    }
}
