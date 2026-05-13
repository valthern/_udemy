using Microsoft.AspNetCore.Identity;

namespace ProyectoIdentity.Models
{
    public class AppUsuarios : IdentityUser
    {
        public string Nombre { get; set; }
        public string Url { get; set; }
        public int CodigoPais { get; set; }
        public string Pais { get; set; }
        public string Ciudad { get; set; }
        public string Direccion { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public bool Estado { get; set; }
    }
}
