using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace EFCorePeliculas.Entidades.Configuraciones
{
    public class PeliculaConfig : IEntityTypeConfiguration<Pelicula>
    {
        public void Configure(EntityTypeBuilder<Pelicula> builder)
        {
            // Pelicula
            builder.Property(p => p.Titulo)
                .HasMaxLength(250)
                .IsRequired();

            //modelBuilder.Entity<Pelicula>().Property(p => p.FechaEstreno)
            //    .HasColumnType("date");
            
            builder.Property(p => p.PosterURL)
                .HasMaxLength(500)
                .IsUnicode(false);

            //builder.HasMany(p => p.Generos)
            //    .WithMany(g => g.Peliculas)
            //    .UsingEntity(x => 
            //        x.ToTable("GenerosPeliculas")
            //        .HasData(new { PeliculasId = 1, GenerosIdentificador = 7 })
            //        );
        }
    }
}
