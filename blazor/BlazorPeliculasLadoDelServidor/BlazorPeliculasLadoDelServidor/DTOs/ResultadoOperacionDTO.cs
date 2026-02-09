using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorPeliculasLadoDelServidor.DTOs
{
    public struct ResultadoOperacionDTO
    {
        public ResultadoOperacionDTO(bool error): this(error, string.Empty) { }

        public ResultadoOperacionDTO(bool error, string mensajeError)
        {
            Error = error;
            MensajeError = mensajeError;
        }
        public bool Error { get; set; }
        public string MensajeError { get; set; }
    }
}