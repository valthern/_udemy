using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RelacionesConEFCore.Models.Entities;

[Table("Articulos", Schema = "blog")]
public class Articulo
{
    public int Id { get; set; }

    [Required, MaxLength(200)]
    public required string Titulo { get; set; }

    [Required]
    public required string Contenido { get; set; }

    public int CategoriaId { get; set; }

    public Categoria Categoria { get; set; }

    public List<Etiqueta> Etiquetas { get; set; }

    public List<Comentario> Comentarios { get; set; }
}
