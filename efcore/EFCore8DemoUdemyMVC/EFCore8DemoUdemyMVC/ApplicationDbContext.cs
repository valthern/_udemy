using EFCore8DemoUdemyMVC.Models;
using Microsoft.EntityFrameworkCore;

namespace EFCore8DemoUdemyMVC
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Persona> Personas { get; set; }

    }
}
