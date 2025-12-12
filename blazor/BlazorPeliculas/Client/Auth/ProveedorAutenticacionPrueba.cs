using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace BlazorPeliculas.Client.Auth
{
    public class ProveedorAutenticacionPrueba : AuthenticationStateProvider
    {
        public async override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var fechaNacimiento = new DateTime(1978, 02, 26);
            var fechaNacimientoOmarStr = fechaNacimiento.ToString("dd/MM/yyyy");
            var edad = CalcularEdad(fechaNacimiento, DateTime.Today);

            var anonimo = new ClaimsIdentity();
            var usuarioOmar = new ClaimsIdentity(
                new List<Claim>
                {
                    new(ClaimTypes.Name, "Omar"),
                    new(ClaimTypes.Email, "oimartinez@magnograffelicidad.mx"),
                    new(ClaimTypes.DateOfBirth, fechaNacimientoOmarStr),
                    new("Edad",edad.ToString()),
                    new(ClaimTypes.Country, "Mexico"),
                    new("Estado", "Puebla"),
                    new("Ciudad", "Puebla"),
                    //new(ClaimTypes.Role,"admin")
                },
                authenticationType: "prueba");
            return await Task.FromResult(new AuthenticationState(new ClaimsPrincipal(usuarioOmar)));
        }

        private int CalcularEdad(DateTime nacimiento, DateTime hoy) => hoy.AddYears(-nacimiento.Year) < nacimiento ? hoy.Year - nacimiento.Year - 1 : hoy.Year - nacimiento.Year;
    }
}
