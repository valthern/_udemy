using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAnnotationsEFCore.Models.Entities;

[Table("Comentarios", Schema = "blog")]
public class Comentario
{
    public int Id { get; set; }

    [Required(ErrorMessage = "El campo {0} es obligatorio"), MaxLength(80)]
    [Display(Name = "Autor del Comentario")]
    public string Autor { get; set; }

    [Required(ErrorMessage = "El campo {0} es obligatorio"), MaxLength(150)]
    [Display(Name = "Texto del Comentario")]
    [DataType(DataType.Text)]
    //[DataType(DataType.MultilineText)]
    //[DataType(DataType.Url)]
    //[DataType(DataType.Password)]
    //[DataType(DataType.Currency)]
    public string Texto { get; set; }

    [Required(ErrorMessage = "El campo {0} es obligatorio"), MaxLength(150)]
    [EmailAddress(ErrorMessage = "El campo {0} debe ser una dirección de correo electrónico válida")]
    [Display(Name = "Correo Electrónico")]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }
}
