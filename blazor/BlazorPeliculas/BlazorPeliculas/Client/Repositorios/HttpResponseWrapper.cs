using System.Net;

namespace BlazorPeliculas.Client.Repositorios
{
    public class HttpResponseWrapper<T>
    {
        public HttpResponseWrapper(T? response, bool error, HttpResponseMessage httpResponseMessage)
        {
            Response = response;
            Error = error;
            HttpResponseMessage = httpResponseMessage;
        }

        public bool Error { get; set; }
        public T? Response { get; set; }
        public HttpResponseMessage? HttpResponseMessage { get; set; }

        public async Task<string?> ObtenerMensajeError()
        {
            if (!Error) return null;

            var codigoEstatus = HttpResponseMessage!.StatusCode;

            switch (codigoEstatus)
            {
                case HttpStatusCode.NotFound:
                    return "Recurso no encontrado";
                case HttpStatusCode.BadRequest:
                    return await HttpResponseMessage.Content.ReadAsStringAsync();
                case HttpStatusCode.Unauthorized:
                    return "Tienes que logearte para hacer esto";
                case HttpStatusCode.Forbidden:
                    return "No tienes permisos para hacer esto";
                default:
                    return "Ha ocurrido un error inesperado";
            }
        }
    }
}
