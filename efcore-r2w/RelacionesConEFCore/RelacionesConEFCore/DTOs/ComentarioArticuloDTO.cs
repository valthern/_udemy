namespace RelacionesConEFCore.DTOs
{
    public class ComentarioArticuloDTO
    {
        public int Id { get; set; }
        public string Texto { get; set; } = string.Empty;
        public string? Articulo { get; set; }
    }
}
