using Microsoft.EntityFrameworkCore;
using RelacionesConEFCore.Models.Entities;

namespace RelacionesConEFCore.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

    public DbSet<Categoria> Categorias { get; set; }
    public DbSet<Articulo> Articulos { get; set; }
    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<PerfilUsuario> PerfilesUsuarios { get; set; }
    public DbSet<Comentario> Comentarios { get; set; }
    public DbSet<Etiqueta> Etiquetas { get; set; }
}
