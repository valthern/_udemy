using BlazorPeliculasLadoDelServidor.DTOs;
using Microsoft.EntityFrameworkCore;

namespace BlazorPeliculasLadoDelServidor.Helpers
{
    public static class IQueryableExtensions
    {
        public static async Task<int> CalcularTotalPaginas<T>(this IQueryable<T> queryable,
            int cantidadRegistrosMostrar)
        {
            double conteo = await queryable.CountAsync();
            int totalPaginas = (int)Math.Ceiling(conteo / cantidadRegistrosMostrar);
            return totalPaginas;
        }

        public static IQueryable<T> Paginar<T>(this IQueryable<T> queryable,
            PaginacionDTO paginacion)
        {
            return queryable
                .Skip((paginacion.Pagina - 1) * paginacion.CantidadRegistros)
                .Take(paginacion.CantidadRegistros);
        }
    }
}
