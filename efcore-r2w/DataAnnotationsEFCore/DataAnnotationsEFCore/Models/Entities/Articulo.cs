using System.ComponentModel.DataAnnotations;

namespace DataAnnotationsEFCore.Models.Entities;

    public class Articulo
    {
    public int Id { get; set; }

    [MaxLength(200)]
    public required string Titulo { get; set; }

    [MaxLength(1000)]
    public required string Contenido { get; set; }

    public int TiempoLectura { get; set; }
}

