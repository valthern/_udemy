using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;
using System.Collections.ObjectModel;

namespace EFCorePeliculas.Entidades
{
    public class Cine : Notificacion
    {
        private string nombre;
        private Point ubicacion;
        private CineOferta cineOferta;
        private CineDetalle cineDetalle;
        private Direccion direccion;

        public int Id { get; set; }
        public string Nombre { get => nombre; set => Set(value, ref nombre); }
        public Point Ubicacion { get => ubicacion; set => Set(value, ref ubicacion); }
        public CineOferta CineOferta { get => cineOferta; set => Set(value, ref cineOferta); }
        public ObservableCollection<SalaDeCine> SalasDeCine { get; set; }
        public CineDetalle CineDetalle { get => cineDetalle; set => Set(value, ref cineDetalle); }
        public Direccion Direccion { get => direccion; set => Set(value, ref direccion); }
    }
}
