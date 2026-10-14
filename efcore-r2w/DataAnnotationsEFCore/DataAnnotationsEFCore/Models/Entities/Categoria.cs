using DataAnnotationsEFCore.Helpers;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAnnotationsEFCore.Models.Entities;

//[Keyless] Para establecer una entidad sin clave primaria, útil para vistas o consultas complejas.
//[ConcurrencyCheck] Marca una propiedad para la verificación de concurrencia optimista.
[Comment("Tabla de Categorías del Blog")]
[Index(nameof(Slug), IsUnique = true, Name = "IX_Categorias_Slug")]
[Table("Categorias", Schema = "blog")]
public class Categoria
{
    //[Key]
    //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Column("Nombre", TypeName = "nvarchar(80)")]
    [Required, MaxLength(80)]
    [Display(Name = "Nombre de la Categoría")]
    [Comment("Nombre de la categoría del blog")]
    public string Nombre { get; set; }

    [Column("Slug", TypeName = "varchar(100)")]
    [Required]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "El campo {0} debe tener entre {2} y {1} caracteres.")]
    [Display(Name = "Identificador (Slug)", Description = "Identificador único para la categoría")]
    //[RegularExpression(@"^[a-z0-9-]+$", ErrorMessage = "El campo {0} solo puede contener letras minúsculas, números y guiones medios (-).")]
    [NoEspacios(ErrorMessage = "El campo {0} no puede contener espacios en blanco.")]
    public string Slug { get; set; }

    // Estado de UI (no persistido en la base de datos)
    [NotMapped]
    public bool Seleccionado { get; set; }

    //[DataType(DataType.Time)]
    [DataType(DataType.Date)]
    [Display(Name = "Fecha de Publicación")]
    [Column("FechaPublicacion", TypeName = "date")]
    [Required(ErrorMessage = "El campo {0} es obligatorio.")]
    public DateTime FechaPublicacion { get; set; }
}
