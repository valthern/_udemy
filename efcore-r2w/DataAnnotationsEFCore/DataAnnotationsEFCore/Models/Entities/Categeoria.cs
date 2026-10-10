using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAnnotationsEFCore.Models.Entities;

[Table("Categorias", Schema = "blog")]
public class Categoria
{
    //[Key]
    //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Column("Nombre", TypeName = "nvarchar(80)")]
    [Required, MaxLength(80)]
    [Display(Name = "Nombre de la Categoría")]
    public string Nombre { get; set; }

    [Column("Slug", TypeName = "varchar(100)")]
    [Required]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "El campo {0} debe tener entre {2} y {1} caracteres.")]
    [Display(Name = "Identificador (Slug)", Description = "Identificador único para la categoría")]
    public string Slug { get; set; }

    // Estado de UI (no persistido en la base de datos)
    [NotMapped]
    public bool Seleccionado { get; set; }
}
