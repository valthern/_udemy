using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EFCorePeliculas.Servicios
{
    public interface IEventosDbContext
    {
        void ManejarStateChange(object sender, EntityStateChangedEventArgs args);
        void ManejarTracked(object sender, EntityTrackedEventArgs args);
    }
    public class EventosDbContext:IEventosDbContext
    {
        private readonly ILogger<EventosDbContext> logger;

        public EventosDbContext(ILogger<EventosDbContext> logger)
        {
            this.logger = logger;
        }

        public void ManejarTracked(object sender, EntityTrackedEventArgs args)
        {
            var mensaje = $"Entidad: {args.Entry.Entity}, estado: {args.Entry.State}";
            logger.LogInformation(mensaje);
        }

        public void ManejarStateChange(object sender, EntityStateChangedEventArgs args)
        {
            var mensaje = $"Entidad: {args.Entry.Entity}, estado anterior: {args.OldState} - estado nuevo: {args.NewState}";
            logger.LogInformation(mensaje);
        }
    }
}
