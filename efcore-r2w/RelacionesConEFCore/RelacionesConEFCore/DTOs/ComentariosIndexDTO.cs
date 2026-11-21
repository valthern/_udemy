using RelacionesConEFCore.Models.Entities;

namespace RelacionesConEFCore.DTOs
{
    public class ComentariosIndexDTO
    {
        public IEnumerable<Comentario> Comentarios { get; set; } = new List<Comentario>();
        public int PaginaActual { get; set; }
        public int TotalPaginas { get; set; }
        public int TamanoPagina { get; set; }
        public int TotalRegistros { get; set; }
    }
}
