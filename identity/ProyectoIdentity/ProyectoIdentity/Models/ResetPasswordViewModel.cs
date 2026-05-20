using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ProyectoIdentity.Models
{
    public class ResetPasswordViewModel
    {
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Email { get; set; }

        [Required]
        public string Token { get; set; }

        [Required(ErrorMessage = "Debe ingresar la nueva contraseña.")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "La contraseña debe tener al menos {1} caracteres.")]
        [DisplayName("Contraseña")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Debe ingresar la confirmación de la contraseña.")]
        [Compare("Password", ErrorMessage = "Las contraseñas no coinciden.")]
        [DataType(DataType.Password)]
        [DisplayName("Confirmar contraseña")]
        public string ConfirmPassword { get; set; }
    }
}
