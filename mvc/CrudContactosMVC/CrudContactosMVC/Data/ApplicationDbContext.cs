using CrudContactosMVC.Models;
using Microsoft.EntityFrameworkCore;

namespace CrudContactosMVC.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        // Aquí se agregan los DbSet más adelante.
        public DbSet<Contacto> Contactos { get; set; }
    }
}
