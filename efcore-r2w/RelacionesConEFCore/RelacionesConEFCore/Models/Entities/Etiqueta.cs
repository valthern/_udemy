using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RelacionesConEFCore.Models.Entities;

[Table("Etiquetas", Schema = "blog")]
public class Etiqueta
{
    public int Id { get; set; }

    [Required(ErrorMessage ="El campo {0} es obligatorio"), MaxLength(50)]
    public required string Nombre { get; set; }

    public List<Articulo> Articulos { get; set; }
}
