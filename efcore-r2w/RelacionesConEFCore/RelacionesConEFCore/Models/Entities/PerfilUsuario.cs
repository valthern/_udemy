using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RelacionesConEFCore.Models.Entities;

[Table("PerfilesUsuarios", Schema = "blog")]
public class PerfilUsuario
{
    // This property is the primary key and also serves as a foreign key to the Usuario entity.
    [Key]
    // The ForeignKey attribute is commented out because EF Core can infer the relationship based on naming conventions.
    //[ForeignKey("Usuario")]
    //[ForeignKey(nameof(Usuario))]
    public int UsuarioId { get; set; }

    [MaxLength(500)]
    public string Biografia { get; set; }

    [MaxLength(300)]
    public string FotoUrl { get; set; }

    public Usuario Usuario { get; set; }
}

