using Microsoft.AspNetCore.Components;

namespace Blazorpeliculas.Client.Pages
{
    public partial class Counter
    {
        [Inject] ServicioSingleton singleton { get; set; } = null!;
        [Inject] ServicioScoped scoped { get; set; } = null!;
        [Inject] ServicioTransient transient { get; set; } = null!;

        private int currentCount = 0;

        private void IncrementCount()
        {
            currentCount++;
            singleton.Valor = currentCount;
            scoped.Valor = currentCount;
            transient.Valor = currentCount;
        }
    }
}
