using System;
using System.Collections.Generic;

namespace PruebaBDPrimero.Entidades;

public partial class CinesOferta
{
    public int Id { get; set; }

    public DateOnly FechaInicio { get; set; }

    public DateOnly FechaFin { get; set; }

    public decimal PorcentajeDescuento { get; set; }

    public int? CineId { get; set; }

    public virtual Cine Cine { get; set; }
}
