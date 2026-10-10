using DataAnnotationsEFCore.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAnnotationsEFCore.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

    public DbSet<Categoria> Categorias { get; set; }
    public DbSet<Articulo> Articulos { get; set; }
}
