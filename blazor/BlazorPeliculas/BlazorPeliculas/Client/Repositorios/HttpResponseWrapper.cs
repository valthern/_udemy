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
        public HttpResponseMessage HttpResponseMessage { get; set; }

        public async Task<string?> ObtenerMensajeError()
        {
            if (!Error) return null;

            var codigoEstatus = HttpResponseMessage.StatusCode;

            return codigoEstatus switch
            {
                HttpStatusCode.NotFound => "Recurso no encontrado",
                HttpStatusCode.BadRequest => await HttpResponseMessage.Content.ReadAsStringAsync(),
                HttpStatusCode.Unauthorized => "Tienes que logearte para hacer esto",
                HttpStatusCode.Forbidden => "No tienes permisos para hacer esto",
                _ => $"Ha ocurrido un error inesperado (Código de status: {(int)codigoEstatus} {codigoEstatus})",
            };
        }
    }
}
