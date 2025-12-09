using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BlazorPeliculas.Server.Helpers
{
    public static class HttpContextExtensions
    {
        public static async Task InsertarParametrosPaginacionEnRespuesta<T>(
            this HttpContext httpContext, IQueryable<T> queriable,int cantidadRegistrosAMostrar)
        {
            if (httpContext is null)
                throw new ArgumentNullException(nameof(httpContext));
            
            double cantidad = await queriable.CountAsync();
        }
    }
}
