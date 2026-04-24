using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BlogCoreSolution.Models
{
    public class Slider
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido.")]
        [MaxLength(100)]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public bool Estado { get; set; }

        [DataType(DataType.ImageUrl)]
        [Display(Name = "URL de la imagen")]
        public string UrlImagen { get; set; }
    }
}
