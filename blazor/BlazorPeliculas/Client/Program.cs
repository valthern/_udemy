using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using BlazorPeliculas.Client;
using BlazorPeliculas.Client.Repositorios;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

ConfigureServices(builder.Services);
await builder.Build().RunAsync();

void ConfigureServices(IServiceCollection services)
{
    services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
    services.AddSingleton<IRepositorio, Repositorio>();
}
