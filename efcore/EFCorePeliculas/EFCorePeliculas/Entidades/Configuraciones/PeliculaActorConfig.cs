using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace EFCorePeliculas.Entidades.Configuraciones
{
    public class PeliculaActorConfig : IEntityTypeConfiguration<PeliculaActor>
    {
        public void Configure(EntityTypeBuilder<PeliculaActor> builder)
        {
            // PeliculaActor
            builder.HasKey(pa => new { pa.PeliculaId, pa.ActorId });
            builder.Property(pa => pa.Personaje)
                .HasMaxLength(150);
        }
    }
}
