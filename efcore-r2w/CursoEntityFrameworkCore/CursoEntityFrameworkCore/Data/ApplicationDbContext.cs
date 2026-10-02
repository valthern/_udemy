using CursoEntityFrameworkCore.Models.Constants;
using Microsoft.EntityFrameworkCore;

namespace CursoEntityFrameworkCore.Data;

public class ApplicationDbContext:DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

    public DbSet<Categoria> Categorias { get; set; }
}

