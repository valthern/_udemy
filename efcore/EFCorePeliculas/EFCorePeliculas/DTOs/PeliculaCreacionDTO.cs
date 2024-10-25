namespace EFCorePeliculas.DTOs
{
    public class PeliculaCreacionDTO
    {
        public string Ttulo { get; set; }
        public bool EnCartelera { get; set; }
        public DateTime FechaEstreno { get; set; }
        public List<int> Generos { get; set; }
        public List<int> SalasDeCine { get; set; }
        public List<PeliculaActorCreacionDTO> PeliculasActores { get; set; }
    }
}
