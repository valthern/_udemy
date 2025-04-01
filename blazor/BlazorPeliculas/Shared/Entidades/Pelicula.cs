using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazorpeliculas.Shared.Entidades
{
    public class Pelicula
    {
        public string Titulo { get; set; } = null!;
        public DateTime FechaLanzamiento { get; set; }
    }
}
