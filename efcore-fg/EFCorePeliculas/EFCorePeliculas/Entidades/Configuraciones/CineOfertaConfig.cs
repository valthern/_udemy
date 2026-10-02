using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace EFCorePeliculas.Entidades.Configuraciones
{
    public class CineOfertaConfig : IEntityTypeConfiguration<CineOferta>
    {
        public void Configure(EntityTypeBuilder<CineOferta> builder)
        {
            // CineOferta
            builder.Property(co => co.PorcentajeDescuento)
                .HasPrecision(precision: 5, scale: 2);
            //modelBuilder.Entity<CineOferta>().Property(co => co.FechaInicio)
            //    .HasColumnType("date");
            //modelBuilder.Entity<CineOferta>().Property(co => co.FechaFin)
            //    .HasColumnType("date");
        }
    }
}
