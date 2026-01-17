using BlazorPeliculasLadoDelServidor.Areas.Identity;
using BlazorPeliculasLadoDelServidor.Data;
using BlazorPeliculasLadoDelServidor.Repositorios;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;

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
    //services.AddDbContext<ApplicationDbContext>(options =>
    //    options.UseSqlServer(connectionString));

    services.AddDatabaseDeveloperPageExceptionFilter();
    services
        //.AddDefaultIdentity<IdentityUser>(options => 
        //    options.SignIn.RequireConfirmedAccount = true)
        .AddDefaultIdentity<IdentityUser>()
        .AddEntityFrameworkStores<ApplicationDbContext>();
    services.AddRazorPages();
    services.AddServerSideBlazor();
    services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<IdentityUser>>();
    services.AddSingleton<WeatherForecastService>();
    services.AddTransient<RepositorioUsuarios>();
}
