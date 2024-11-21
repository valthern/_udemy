using EFCorePeliculas.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace EFCorePeliculas.Entidades.Configuraciones
{
    public class GeneroConfig : IEntityTypeConfiguration<Genero>
    {
        public void Configure(EntityTypeBuilder<Genero> builder)
        {
            // Genero
            //modelBuilder.Entity<Genero>().ToTable(name: "TablaGeneros", schema: "Peliculas");

            builder.ToTable(name: "Generos", opciones => opciones.IsTemporal());

            builder.Property("PeriodStart").HasColumnType("datetime2");

            builder.Property("PeriodEnd").HasColumnType("datetime2");

            builder.HasKey(g => g.Identificador);

            builder.Property(g => g.Nombre)
                //.HasColumnName("NombreGenero")
                .HasMaxLength(150)
                .IsRequired();
                //.IsConcurrencyToken();

            builder.HasQueryFilter(g => !g.EstaBorrado);

            builder.HasIndex(g => g.Nombre).IsUnique().HasFilter("EstaBorrado = 'false'");

            builder.Property<DateTime>("FechaCreacion").HasDefaultValueSql("GetDate()").HasColumnType("datetime2");
        }
    }
}
