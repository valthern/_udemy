using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoEFCore8Udemy
{
    public class ApplicationDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=NECRO\\DEVELOPMENT;Database=DemoEFCore8Udemy;TrustServerCertificate=True;User=sa;Password=Almanzor");
            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<Persona> Personas { get; set; }
    }
}
