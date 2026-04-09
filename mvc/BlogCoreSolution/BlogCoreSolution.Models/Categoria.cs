using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BlogCoreSolution.Models
{
    public class Categoria
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "El nombre de la categoría es obligatorio")]
        [MaxLength(60)]
        [Display(Name = "Nombre de la Categoría")]
        public string? Nombre { get; set; }
        [Display(Name = "Orden de visualización")]
        [Range(1,10, ErrorMessage = "El orden de visualización debe ser un número entre 1 y 10")]
        public int Orden { get; set; }
        [Display(Name = "Fecha de creación")]
        public DateTime FechaCreacion { get; set; }
    }
}
