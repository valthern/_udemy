using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCorePeliculas.Entidades.Configuraciones
{
    public class FacturaConfig : IEntityTypeConfiguration<Factura>
    {
        public void Configure(EntityTypeBuilder<Factura> builder)
        {
            builder.HasMany(typeof(FacturaDetalle)).WithOne();

            builder.Property(f => f.NumeroFactura)
                .HasDefaultValueSql("NEXT VALUE FOR factura.NumeroFactura");

            //builder.Property(f => f.Version).IsRowVersion();
        }
    }
}
