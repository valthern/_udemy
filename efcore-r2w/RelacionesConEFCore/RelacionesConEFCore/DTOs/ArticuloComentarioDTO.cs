namespace RelacionesConEFCore.DTOs
{
    public class ArticuloComentarioDTO
    {
        public string Titulo { get; set; } = string.Empty;
        public string? Comentario { get; set; }
        public string? Usuario { get; set; }
    }
}
