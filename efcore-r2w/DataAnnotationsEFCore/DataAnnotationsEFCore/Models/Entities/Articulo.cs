using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace DataAnnotationsEFCore.Models.Entities;

public class Articulo
{
    public int Id { get; set; }

    [Required(ErrorMessage="El campo {0} es obligatorio"), MaxLength(200)]
    public required string Titulo { get; set; }

    [Required(ErrorMessage="El campo {0} es obligatorio"), MaxLength(1000)]
    public required string Contenido { get; set; }

    [Range(1, 120, ErrorMessage = "El tiempo de lectura debe estar entre {1} y {2} minutos.")]
    [Display(Name = "Tiempo de Lectura (minutos)")]
    public int TiempoLectura { get; set; }

    [Range(typeof(DateTime), "1/1/2000", "12/31/2100", ErrorMessage = "La fecha de publicación debe estar entre {1} y {2}.")]
    [Display(Name = "Fecha de Publicación")]
    [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
    // Para Fechas sin fracciones de segundo.
    [Precision(0)]
    public DateTime PublicadoEn { get; set; }

    [Display(Name = "Información Adicional")]
    public string InformacionAdicional { get; set; }

    [Precision(12, 2)]
    public decimal PrecioPublicidad { get; set; }

    [Display(Name = "Visitas")]
    [DisplayFormat(DataFormatString = "{0:N0}", ApplyFormatInEditMode = true)]
    public int NumeroVisitas { get; set; }
}
