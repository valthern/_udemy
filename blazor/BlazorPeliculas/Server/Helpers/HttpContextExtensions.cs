using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BlazorPeliculas.Server.Helpers
{
    public static class HttpContextExtensions
    {
        public static async Task InsertarParametrosPaginacionEnRespuesta<T>(
            this HttpContext context, IQueryable<T> queriable, int cantidadRegistrosAMostrar)
        {
            if (context is null)
                throw new ArgumentNullException(nameof(context));

            double conteo = await queriable.CountAsync();
            double totalpaginas = Math.Ceiling(conteo / cantidadRegistrosAMostrar);
            context.Response.Headers.Add("conteo", conteo.ToString());
            context.Response.Headers.Add("totalPaginas", totalpaginas.ToString());
        }
    }
}
