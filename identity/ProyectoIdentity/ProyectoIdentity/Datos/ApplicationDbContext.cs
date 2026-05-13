using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProyectoIdentity.Models;

namespace ProyectoIdentity.Datos
{
    public class ApplicationDbContext : IdentityDbContext<AppUsuarios>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        // Adición de modelos personalizados que vayamos necesitando.

    }
}
