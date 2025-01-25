using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace BlazorPeliculas.Client.Auth
{
    public class ProveedorAutenticacionPrueba : AuthenticationStateProvider
    {
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var anonimo = new ClaimsIdentity();
            var usuarioJelipe = new ClaimsIdentity(
                new List<Claim>
                {
                    new("llave1", "valor1"),
                    new("edad","999"),
                    new(ClaimTypes.Name,"Felipe"),
                    //new(ClaimTypes.Role,"admin")
                },
                authenticationType: "prueba");
            return await Task.FromResult(new AuthenticationState(new ClaimsPrincipal(anonimo)));
        }
    }
}
