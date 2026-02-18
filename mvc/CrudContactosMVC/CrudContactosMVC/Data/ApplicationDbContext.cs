using CrudContactosMVC.Models;
using Microsoft.EntityFrameworkCore;

namespace CrudContactosMVC.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        // Aquí se agregan los DbSet para cada entidad del modelo
        public DbSet<Contacto> Contactos { get; set; }
    }
}
