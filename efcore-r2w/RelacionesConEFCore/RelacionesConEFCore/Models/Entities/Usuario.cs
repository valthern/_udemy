using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RelacionesConEFCore.Models.Entities;

[Table("Usuarios", Schema = "blog")]
public class Usuario
{
    public int Id { get; set; }

    [Required, MaxLength(100)]
    public required string Nombre { get; set; }

    [Required, EmailAddress, MaxLength(150)]
    public required string Email { get; set; }

    public PerfilUsuario Perfil { get; set; } = null;

    public List<Comentario> Comentarios { get; set; }
}

