﻿using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFCorePeliculas.Entidades
{
    //[Table("TablaGeneros", Schema = "peliculas")]
    //[Index(nameof(Titulo), IsUnique = true)]
    public class Genero : EntidadAuditable
    {
        //[Key]
        public int Identificador { get; set; }
        // Se pueden usar cualquiera de los dos atributos para limitar el tamaño de los strings guardados en la BD
        //[StringLength(150)]
        //[MaxLength(150)]
        //[Required]
        //[Column("NombreGenero")]
        [ConcurrencyCheck]
        public string Nombre { get; set; }
        public bool EstaBorrado { get; set; }
        public HashSet<Pelicula> Peliculas { get; set; }
        public string Ejemplo { get; set; }
        //public string Ejemplo2 { get; set; }
    }
}
