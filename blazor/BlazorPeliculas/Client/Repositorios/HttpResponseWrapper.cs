using System.Net;

namespace BlazorPeliculas.Client.Repositorios
{
    public class HttpResponseWrapper<T>
    {
        public T? Response { get; }
        public bool Error { get; }
        public HttpResponseMessage HttpResponseMessage { get; }

        public HttpResponseWrapper(T? response, bool error, HttpResponseMessage httpResponseMessage)
        {
            Response = response;
            Error = error;
            HttpResponseMessage = httpResponseMessage;
        }

        public async Task<string?> ObtenerMensajeError()
        {
            if (!Error) return null;

            var codigoEstatus = HttpResponseMessage.StatusCode;

            switch (codigoEstatus)
            {
                case HttpStatusCode.NotFound:
                    return "Recurso no encontrado. Error " + HttpStatusCode.NotFound.ToString();
                //break;
                case HttpStatusCode.BadRequest:
                    return "Error "+ HttpStatusCode.BadRequest.ToString() + ". " + await HttpResponseMessage.Content.ReadAsStringAsync();
                case HttpStatusCode.Unauthorized:
                    return "Tienes que loguearte pra hacer esto. Error " + HttpStatusCode.Unauthorized.ToString();
                case HttpStatusCode.Forbidden:
                    return "No tienes permisos para hacer esto. Error " + HttpStatusCode.Forbidden.ToString();
                default:
                    return "Ha ocurrido un error inesperado. Error " + codigoEstatus.ToString();
            }
        }
    }
}
