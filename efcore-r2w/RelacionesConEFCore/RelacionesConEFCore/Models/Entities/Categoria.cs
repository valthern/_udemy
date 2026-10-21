using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RelacionesConEFCore.Models.Entities;

[Table("Categorias", Schema = "blog")]
public class Categoria
{
    public int Id { get; set; }

    [Required(ErrorMessage ="El campo {0} es obligatorio"), MaxLength(80)]
    public required string Nombre { get; set; }

    public List<Articulo> Articulos { get; set; }
}
