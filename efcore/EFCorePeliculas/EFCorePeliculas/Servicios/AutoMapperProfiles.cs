using AutoMapper;
using EFCorePeliculas.DTOs;
using EFCorePeliculas.Entidades;
using NetTopologySuite;
using NetTopologySuite.Geometries;

namespace EFCorePeliculas.Servicios
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            #region De Obtención
            /***** De Obtención *****/
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
            #endregion



            #region De Creación
            /***** De Creación *****/
            var geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326);

            CreateMap<CineCreacionDTO, Cine>().ForMember(c => c.Ubicacion, ent => ent.MapFrom(dto => geometryFactory.CreatePoint(new Coordinate(dto.Longitud, dto.Latitud))));

            CreateMap<CineOfertaCreacionDTO, CineOferta>();

            CreateMap<SalaDeCineCreacionDTO, SalaDeCine>();

            CreateMap<PeliculaCreacionDTO, Pelicula>()
                .ForMember(p => p.Generos, ent => ent.MapFrom(dto => dto.Generos.Select(id => new Genero { Identificador = id })))
                .ForMember(p => p.SalasDeCine, ent => ent.MapFrom(dto => dto.SalasDeCine.Select(id => new SalaDeCine { Id = id })));

            CreateMap<PeliculaActorCreacionDTO, PeliculaActor>();

            CreateMap<ActorCreacionDTO, Actor>();
            #endregion
        }
    }
}
