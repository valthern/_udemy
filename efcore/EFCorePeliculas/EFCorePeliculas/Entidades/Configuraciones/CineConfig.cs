using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace EFCorePeliculas.Entidades.Configuraciones
{
    public class CineConfig : IEntityTypeConfiguration<Cine>
    {
        public void Configure(EntityTypeBuilder<Cine> builder)
        {
            // Cine
            builder.Property(c => c.Nombre)
                .HasMaxLength(150)
                .IsRequired();

            builder.HasOne(c => c.CineOferta)
                .WithOne()
                .HasForeignKey<CineOferta>(co => co.CineId);

            builder.HasMany(c => c.SalasDeCine)
                .WithOne(s => s.Cine)
                .HasForeignKey(s => s.ElCine)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(c => c.CineDetalle)
                .WithOne(cd => cd.Cine)
                .HasForeignKey<CineDetalle>(cd => cd.Id);
        }
    }
}
