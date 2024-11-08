using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace EFCorePeliculas.Entidades.Configuraciones
{
    public class ActorConfig : IEntityTypeConfiguration<Actor>
    {
        public void Configure(EntityTypeBuilder<Actor> builder)
        {
            // Actor
            builder.Property(a => a.Nombre)
                .HasMaxLength(150)
                .IsRequired();
            
            //modelBuilder.Entity<Actor>().Property(a => a.FechaNacimiento)
            //    .HasColumnType("date");

            builder.Property(a => a.Nombre).HasField("nombre");

            //builder.Ignore(a => a.Edad);
            //builder.Ignore(a => a.DireccionHogar);

            builder.OwnsOne(a => a.DireccionHogar, dir =>
            {
                dir.Property(d => d.Calle).HasColumnName("Calle");
                dir.Property(d => d.Provincia).HasColumnName("Provincia");
                dir.Property(d => d.Pais).HasColumnName("Pais");
            });
        }
    }
}
 