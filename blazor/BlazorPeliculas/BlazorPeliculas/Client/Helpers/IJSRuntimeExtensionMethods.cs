using Microsoft.JSInterop;
using System.Runtime.CompilerServices;

namespace BlazorPeliculas.Client.Helpers
{
    public static class IJSRuntimeExtensionMethods
    {
        public static async ValueTask<bool> Confirm(this IJSRuntime js, string mensaje)
        {
            await js.InvokeVoidAsync("console.log", "Prueba de console log (InvokeVoidAsync)");
            return await js.InvokeAsync<bool>("confirm", mensaje);
        }
    }
}
