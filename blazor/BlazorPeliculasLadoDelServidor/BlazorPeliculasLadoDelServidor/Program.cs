using BlazorPeliculasLadoDelServidor.Areas.Identity;
using BlazorPeliculasLadoDelServidor.Data;
using BlazorPeliculasLadoDelServidor.Helpers;
using BlazorPeliculasLadoDelServidor.Repositorios;
using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Tewr.Blazor.FileReader;

var builder = WebApplication.CreateBuilder(args);

ConfigureServices(builder.Services);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();



// Métodos auxiliares para organizar el código
void ConfigureServices(IServiceCollection services)
{
    var csName = "TicDesarrolloConnection";
    //var csName = "NecroConnection";

    // Add services to the container.
    var connectionString = builder.Configuration
        .GetConnectionString(csName) ??
        throw new InvalidOperationException($"Connection string '{csName}' not found.");

    // Additional service configurations can be added here.
    var optionBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
    optionBuilder.UseSqlServer(connectionString);
    services.AddTransient(_ => new ApplicationDbContext(optionBuilder.Options));
    // Por defecto AddContext registra el contexto con alcance (scoped):
    //services.AddDbContext<ApplicationDbContext>(options =>
    //    options.UseSqlServer(connectionString));

    services.AddDatabaseDeveloperPageExceptionFilter();
    services
        //.AddDefaultIdentity<IdentityUser>(options => 
        //    options.SignIn.RequireConfirmedAccount = true)
        .AddDefaultIdentity<IdentityUser>()
        .AddRoles<IdentityRole>()
        .AddEntityFrameworkStores<ApplicationDbContext>();
    services.AddRazorPages();
    services.AddServerSideBlazor();
    services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<IdentityUser>>();
    services.AddSingleton<WeatherForecastService>();
    services.AddTransient<RepositorioUsuarios>();
    services.AddTransient<RepositorioGeneros>();
    services.AddTransient<RepositorioActores>();
    services.AddSweetAlert2();
    services.AddScoped<IAlmacenadorArchivos, AlmacenadorArchivosLocal>();
    services.AddAutoMapper(typeof(Program));
    services.AddFileReaderService(options => options.InitializeOnFirstCall = true);
    // Asegúrate de que el paquete NuGet "BlazorInputFile" o "Blazor.FileReader" esté instalado en tu proyecto.
    // Si no está instalado, abre la consola del Administrador de paquetes y ejecuta:
    // dotnet add package Blazor.FileReader
    // Si ya tienes la directiva using y el paquete instalado, limpia y reconstruye la solución para asegurarte de que las referencias estén actualizadas.
}
