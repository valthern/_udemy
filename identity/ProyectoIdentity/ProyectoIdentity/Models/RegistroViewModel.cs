using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ProyectoIdentity.Models
{
    public class RegistroViewModel
    {
        // Campos obligatorios de Identity
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [EmailAddress(ErrorMessage = "El campo {0} no es una dirección de correo válida.")]
        [DisplayName("Correo Electrónico")]
        public string Email { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "La contraseña debe tener al menos {1} caracteres.")]
        [DisplayName("Contraseña")]
        public string Password { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Las contraseñas no coinciden.")]
        [DisplayName("Confirmar Contraseña")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [StringLength(100, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres.")]
        [DisplayName("Nombre completo")]
        public string Nombre { get; set; }

        [Url(ErrorMessage = "El campo {0} no es una URL válida.")]
        [DisplayName("Sitio web personal")]
        public string Url { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [Range(1, 200, ErrorMessage = "El campo {0} debe estar entre {1} y {2}.")]
        [DisplayName("Código de país")]
        public int CodigoPais { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [Phone(ErrorMessage = "El campo {0} no es un número de teléfono válido.")]
        [DisplayName("Número de teléfono")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [DisplayName("País")]
        public string Pais { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [DisplayName("Ciudad")]
        public string Ciudad { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [DisplayName("Dirección completa")]
        public string Direccion { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [DataType(DataType.Date)]
        [DisplayName("Fecha de nacimiento")]
        public DateTime FechaNacimiento { get; set; }

        [DisplayName("Activo")]
        public bool Estado { get; set; }
    }
}
