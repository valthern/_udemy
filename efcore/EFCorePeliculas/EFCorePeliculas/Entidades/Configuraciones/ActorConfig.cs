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
        }
    }
}
