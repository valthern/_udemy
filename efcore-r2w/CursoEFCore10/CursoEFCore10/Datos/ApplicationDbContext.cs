using CursoEFCore10.Models;
using Microsoft.EntityFrameworkCore;

namespace CursoEFCore10.Datos
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Categoria> Categorias { get; set; }
    }
}
