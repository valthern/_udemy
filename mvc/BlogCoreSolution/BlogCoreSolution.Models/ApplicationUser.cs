using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BlogCoreSolution.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Direccion { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Ciudad { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Pais { get; set; }
    }
}
