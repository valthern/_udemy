using Blazorpeliculas.Shared.Entidades;

namespace Blazorpeliculas.Client.Repositorios
{
    public interface IRepositorio
    {
        List<Pelicula> ObtenerPeliculas();
    }
}
