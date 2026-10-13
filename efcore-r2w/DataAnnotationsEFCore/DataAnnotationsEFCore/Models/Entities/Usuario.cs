using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAnnotationsEFCore.Models.Entities;

[Table("Usuarios", Schema = "blog")]
public class Usuario
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid UsuarioId { get; set; }

    [Required(ErrorMessage = "El campo {0} es obligatorio"), MaxLength(100)]
    public required string Nombre { get; set; }

    [Required(ErrorMessage = "El campo {0} es obligatorio"), MaxLength(150)]
    [EmailAddress(ErrorMessage = "El campo {0} debe ser una dirección de correo electrónico válida")]
    [Unicode(false)]
    public required string Email { get; set; }
}
