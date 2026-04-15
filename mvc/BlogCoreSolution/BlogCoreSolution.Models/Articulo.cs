using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BlogCoreSolution.Models
{
    public class Articulo
    {
        public Articulo() => FechaCreacion = DateTime.Now;

        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "El nombre es obligatorio")]
        [Display(Name = "Nombre del artículo")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "La descripción es obligatoria")]
        [Display(Name = "Descripción del artículo")]
        public string Descripcion { get; set; }
        [Display(Name = "Fecha de creación")]
        public DateTime FechaCreacion { get; set; }
        [DataType(DataType.ImageUrl)]
        [Display(Name = "URL de la imagen")]
        public string UrlImagen { get; set; }

        [Required(ErrorMessage = "La categoría es obligatoria")]
        public int CategoriaId { get; set; }
        [Display(Name = "Categoría")]
        [ForeignKey("CategoriaId")]
        public Categoria Categoria { get; set; }
    }
}
