using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CursoEFCoreDatabaseFirst.Models;

[Table("etiqueta")]
public class etiqueta
{
    public int Id { get; set; }

    [StringLength(100)]
    [Required(ErrorMessage ="El campo {0} es obligatorio")]
    public string Nombre { get; set; }
    
    public string? Descripcion { get; set; }
    
    public bool Activo { get; set; }
}
