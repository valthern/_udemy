using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using BlazorPeliculas.Client;
using BlazorPeliculas.Client.Repositorios;
using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components.Authorization;
using BlazorPeliculas.Client.Auth;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

ConfigureServices(builder.Services);
await builder.Build().RunAsync();

void ConfigureServices(IServiceCollection services)
{
    services.AddSingleton(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
    services.AddScoped<IRepositorio, Repositorio>();
    services.AddSweetAlert2();
    services.AddAuthorizationCore();
    services.AddScoped<ProveedorAutenticacionJWT>();
    services.AddScoped<AuthenticationStateProvider, ProveedorAutenticacionJWT>(proveedor =>
        proveedor.GetRequiredService<ProveedorAutenticacionJWT>());
    services.AddScoped<ILoginService, ProveedorAutenticacionJWT>(proveedor =>
        proveedor.GetRequiredService<ProveedorAutenticacionJWT>());
}
