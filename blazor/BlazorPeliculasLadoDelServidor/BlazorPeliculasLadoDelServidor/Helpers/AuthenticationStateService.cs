using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace BlazorPeliculasLadoDelServidor.Helpers
{
    public class AuthenticationStateService
    {
        private readonly AuthenticationStateProvider authenticationStateProvider;

        public AuthenticationStateService(AuthenticationStateProvider authenticationStateProvider)
        {
            this.authenticationStateProvider = authenticationStateProvider;
        }

        public async Task<string> GetCurrentUserId()
        {
            var userState = await authenticationStateProvider.GetAuthenticationStateAsync();

            if (!userState.User.Identity!.IsAuthenticated) return null;
            
            var claims= userState.User.Claims;

            var claimWithUserId = claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

            return claimWithUserId is null ? throw new ApplicationException("No es posible encontrar el ID del usuario") : claimWithUserId.Value;
        }
    }
}
