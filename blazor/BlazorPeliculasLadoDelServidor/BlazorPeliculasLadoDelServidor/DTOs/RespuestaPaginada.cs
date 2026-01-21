namespace BlazorPeliculasLadoDelServidor.DTOs
{
    public class RespuestaPaginadaDTO<T>
    {
        public int TotalPaginas { get; set; }
        public List<T> Registros { get; set; }
    }
}
