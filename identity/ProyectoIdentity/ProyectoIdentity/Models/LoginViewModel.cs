using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ProyectoIdentity.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [EmailAddress(ErrorMessage = "El campo {0} no es una dirección de correo válida.")]
        [DisplayName("Correo Electrónico")]
        public string Email { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "La contraseña debe tener al menos {1} caracteres.")]
        [DisplayName("Contraseña")]
        public string Password { get; set; }

        //[Required(ErrorMessage = "El campo {0} es obligatorio.")]
        //[DataType(DataType.Password)]
        //[Compare("Password", ErrorMessage = "Las contraseñas no coinciden.")]
        //[DisplayName("Confirmar Contraseña")]
        //public string ConfirmPassword { get; set; }

        [DisplayName("¿Recordar sesión?")]
        public bool RememberMe { get; set; }
    }
}
