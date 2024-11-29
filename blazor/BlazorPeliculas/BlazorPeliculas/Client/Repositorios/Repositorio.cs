using BlazorPeliculas.Shared.Entidades;

namespace BlazorPeliculas.Client.Repositorios
{
    public class Repositorio : IRepositorio
    {
        public List<Pelicula> ObtenerPeliculas() => new()
        {
            new() {Titulo = "Wakanda Forever",FechaLanzamiento = new DateTime(2024, 11, 26) },
            new() {Titulo="Moana", FechaLanzamiento=new DateTime(2016,11,23) },
            new() {Titulo="Inception", FechaLanzamiento=new DateTime(2010,7,16) }
        };
    }
}
