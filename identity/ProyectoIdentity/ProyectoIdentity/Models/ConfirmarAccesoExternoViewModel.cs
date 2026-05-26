using System.ComponentModel.DataAnnotations;

namespace ProyectoIdentity.Models
{
    public class ConfirmarAccesoExternoViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public string ReturnUrl { get; set; }
    }
}
