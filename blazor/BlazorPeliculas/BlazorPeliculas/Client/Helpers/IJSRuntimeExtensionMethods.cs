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

        public static async ValueTask<object> GuardarEnLocalStorage(this IJSRuntime js, string llave, string contenido)
        {
            return await js.InvokeAsync<object>("localStorage.setItem", llave);
        }

        public static async ValueTask<object> ObtenerDeLocalStorage(this IJSRuntime js, string llave)
        {            
            return await js.InvokeAsync<object>("localStorage.getItem", llave);
        }            
                     
        public static async ValueTask<object> RemoverDelLocalStorage(this IJSRuntime js, string llave)
        {
            return await js.InvokeAsync<object>("localStorage.removeItem", llave);
        }
    }
}
