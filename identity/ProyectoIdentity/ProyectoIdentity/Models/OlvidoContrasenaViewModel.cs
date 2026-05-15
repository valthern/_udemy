using System.ComponentModel.DataAnnotations;

namespace ProyectoIdentity.Models
{
    public class OlvidoContrasenaViewModel
    {
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [EmailAddress(ErrorMessage = "El campo {0} debe ser una dirección de correo electrónico válida.")]
        public string Email { get; set; }
    }
}
