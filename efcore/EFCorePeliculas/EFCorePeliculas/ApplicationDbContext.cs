using EFCorePeliculas.Entidades;
using Microsoft.EntityFrameworkCore;

namespace EFCorePeliculas
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Genero>().HasKey(g => g.Identificador);
            modelBuilder.Entity<Genero>()
                .Property(g => g.Nombre)
                //.HasColumnName("NombreGenero")
                .HasMaxLength(150)
                .IsRequired();
            //modelBuilder.Entity<Genero>().ToTable(name: "TablaGeneros", schema: "Peliculas");

            modelBuilder.Entity<Actor>()
                .Property(a => a.Name)
                .HasMaxLength(150)
                .IsRequired();
            modelBuilder.Entity<Actor>()
                .Property(a => a.FechaNacimiento)
                .HasColumnType("date");

            modelBuilder.Entity<Cine>()
                .Property(c => c.Nombre)
                .HasMaxLength(150)
                .IsRequired();
            modelBuilder.Entity<Cine>()
                .Property(c => c.Precio)
                .HasPrecision(precision: 9, scale: 2);
        }

        public DbSet<Genero> Generos { get; set; }
        public DbSet<Actor> Actors { get; set; }
        public DbSet<Cine> Cines { get; set; }
    }
}
