using AutoMapper;
using EFCorePeliculas.DTOs;
using EFCorePeliculas.Entidades;

namespace EFCorePeliculas.Servicios
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Actor, ActorDTO>();

            CreateMap<Cine, CineDTO>()
                .ForMember(dto => dto.Latitud, ent => ent.MapFrom(c => c.Ubicacion.Y))
                .ForMember(dto => dto.Longitud, ent => ent.MapFrom(c => c.Ubicacion.X));

            CreateMap<Genero, GeneroDTO>();

            CreateMap<Pelicula, PeliculaDTO>()
                .ForMember(dto => dto.Cines, ent => ent.MapFrom(p => p.SalasDeCine.Select(s => s.Cine)))
                .ForMember(dto => dto.Actores, ent => ent.MapFrom(p => p.PeliculasActores.Select(pa => pa.Actor)));
        }
    }
}
