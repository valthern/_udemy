using System;
using System.Collections.Generic;

namespace PruebaBDPrimero.Entidades;

public partial class Pelicula
{
    public int Id { get; set; }

    public string Titulo { get; set; }

    public bool EnCartelera { get; set; }

    public DateOnly FechaEstreno { get; set; }

    public string PosterUrl { get; set; }

    public virtual ICollection<PeliculasActore> PeliculasActores { get; set; } = new List<PeliculasActore>();

    public virtual ICollection<Genero> GenerosIdentificadors { get; set; } = new List<Genero>();

    public virtual ICollection<SalasDeCine> SalasDeCines { get; set; } = new List<SalasDeCine>();
}
