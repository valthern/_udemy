using System.ComponentModel.DataAnnotations.Schema;

namespace RelacionesConEFCore.Models.Entities;

[Table("Comentarios", Schema = "blog")]
public class Comentario
{
    public int Id { get; set; }

    public string Texto { get; set; } = null;

    // Este atributo sería necesario si no se siguieran las convenciones de EF Core para nombrar las propiedades de clave foránea.
    // En este caso, como la propiedad de navegación se llama "Usuario", EF Core puede inferir automáticamente que "UsuarioId"
    // es la clave foránea correspondiente.
    //[ForeignKey(nameof(Usuario))]
    public int UsuarioId { get; set; }

    public int ArticuloId { get; set; }

    public Usuario Usuario { get; set; }

    public Articulo Articulo { get; set; }
}

