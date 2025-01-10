using AutoMapper;
using BlazorPeliculas.Shared.Entidades;

namespace BlazorPeliculas.Server.Helpers
{
    public class AutoMapperProfiles:Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Actor, Actor>()
                .ForMember(a => a.Foto, option => option.Ignore());

            CreateMap<Pelicula, Pelicula>()
                .ForMember(p=>p.Poster, option => option.Ignore());
        }
    }
}
