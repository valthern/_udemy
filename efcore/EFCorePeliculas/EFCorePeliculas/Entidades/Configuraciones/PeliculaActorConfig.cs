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

            #region Configuración de Relación Muchos a Muchos
            /** Esta configuración aplica en la tabla intermedia creada
             * por nosotros para, por ejemplo, personalizar otros campos
             * en ella **/
            builder.HasOne(pa => pa.Actor)
                .WithMany(a => a.PeliculasActores)
                .HasForeignKey(pa => pa.ActorId);

            builder.HasOne(pa => pa.Pelicula)
                .WithMany(p=>p.PeliculasActores)
                .HasForeignKey(pa=>pa.PeliculaId);
            #endregion

            builder.Property(pa => pa.Personaje)
                .HasMaxLength(150);
        }
    }
}
