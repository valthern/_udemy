using Blazorpeliculas.Shared.Entidades;

namespace Blazorpeliculas.Client.Repositorios
{
    public class Repositorio : IRepositorio
    {
        public List<Pelicula> ObtenerPeliculas()
        {
            return new List<Pelicula>
            {
                new() { Titulo = "Wakanda Forever", FechaLanzamiento = new DateTime(2025, 03, 31) },
                new() { Titulo = "Moana", FechaLanzamiento = new DateTime(2016, 11, 23) },
                new() { Titulo = "Inception", FechaLanzamiento = new DateTime(2010, 07, 16) }
            };
        }
    }
}
