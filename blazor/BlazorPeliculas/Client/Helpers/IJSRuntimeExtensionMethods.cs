using Microsoft.JSInterop;

namespace BlazorPeliculas.Client.Helpers
{
    public static class IJSRuntimeExtensionMethods
    {
        public static async ValueTask<bool> Confirm(this IJSRuntime js, string mensaje)
        {
            await js.InvokeVoidAsync("console.log", "Prueba de console log");
            return await js.InvokeAsync<bool>("confirm", mensaje);
        }

        public static ValueTask<object> GuardarEnLocalStorge(this IJSRuntime js, string llave, string contenido) => js.InvokeAsync<object>("localStorage.setItem", llave, contenido);
        
        public static ValueTask<object> ObtenerDeLocalStorage(this IJSRuntime js, string llave) => js.InvokeAsync<object>("localStorage.getItem", llave);
        
        public static ValueTask<object> RemoverDelLocalStorge(this IJSRuntime js, string llave) => js.InvokeAsync<object>("localStorage.removeItem", llave);
    }
}
