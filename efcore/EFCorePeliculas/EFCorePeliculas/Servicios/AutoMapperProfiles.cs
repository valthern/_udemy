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

            // Sin ProjectTo()
            //CreateMap<Pelicula, PeliculaDTO>()
            //    .ForMember(dto => dto.Cines, ent => ent.MapFrom(p => p.SalasDeCine.Select(s => s.Cine)))
            //    .ForMember(dto => dto.Actores, ent => ent.MapFrom(p => p.PeliculasActores.Select(pa => pa.Actor)));

            // Con ProjectTo()
            CreateMap<Pelicula, PeliculaDTO>()
                // Para ordenar los géneros con el Nombre de forma descendente
                .ForMember(dto => dto.Generos, ent => ent.MapFrom(p => p.Generos.OrderByDescending(g => g.Nombre)))
                .ForMember(dto => dto.Cines, ent => ent.MapFrom(p => p.SalasDeCine.Select(s => s.Cine)))
                .ForMember(dto => dto.Actores, ent => ent.MapFrom(p => p.PeliculasActores.Select(pa => pa.Actor)));

        }
    }
}
