using System;
using System.Collections.Generic;

namespace PruebaBDPrimero.Entidades;

public partial class Persona
{
    public int Id { get; set; }

    public string Nombre { get; set; }

    public virtual ICollection<Mensaje> MensajeEmisors { get; set; } = new List<Mensaje>();

    public virtual ICollection<Mensaje> MensajeReceptors { get; set; } = new List<Mensaje>();
}
