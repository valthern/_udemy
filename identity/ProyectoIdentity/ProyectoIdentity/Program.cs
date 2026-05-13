using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProyectoIdentity.Datos;
using ProyectoIdentity.Models;

var builder = WebApplication.CreateBuilder(args);

var nombreCadCon = "csiTiDevConnection";
var cadenaConexionBd = builder.Configuration.GetConnectionString(nombreCadCon);
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(cadenaConexionBd));

// Se agrega el soporte para Identity.
builder.Services
    .AddIdentity<AppUsuarios, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseRouting();

// Soporte para la autenticación. OBLIGATORIO ponerlo ANTES de UseAuthorization.
app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
