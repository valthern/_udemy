using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProyectoIdentity.Datos;
using ProyectoIdentity.Models;

var builder = WebApplication.CreateBuilder(args);

//var nombreCadCon = "csiTiDevConnection";
//var cadenaConexionBd = builder.Configuration.GetConnectionString(nombreCadCon);
//builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(cadenaConexionBd));

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("csiTiDevConnection")));

// Se agrega el soporte para Identity.
builder.Services
    .AddIdentity<AppUsuarios, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(opciones =>
{
    // Valor por defecto para la duración de la cookie de autenticación (14 días en este caso).
    opciones.ExpireTimeSpan = TimeSpan.FromDays(7);
    // Si el usuario interactúa, se renueva la cookie.
    opciones.SlidingExpiration = true;

    // Rutas estándard para el login, logout y acceso denegado.
    opciones.LoginPath = "/Account/Login";
    opciones.LogoutPath = "/Account/Logout";
    opciones.AccessDeniedPath = "/Account/AccessDenied";

    // Parámetro de retorno (ReturnUrl) que se usa para redirigir al usuario a la página que intentaba acceder antes de iniciar sesión.
    opciones.ReturnUrlParameter = "ReturnUrl"; // ? ReturnUrl=/pagina-protegida
});

builder.Services.Configure<IdentityOptions>(opciones =>
{
    // Configuración de la contraseña.
    opciones.Password.RequireDigit = true; // Requiere al menos un dígito.
    opciones.Password.RequiredLength = 8; // Longitud mínima de la contraseña.
    opciones.Password.RequireNonAlphanumeric = false; // No requiere caracteres no alfanuméricos.
    opciones.Password.RequireUppercase = true; // Requiere al menos una letra mayúscula.
    opciones.Password.RequireLowercase = true; // Requiere al menos una letra minúscula.

    // Bloqueo por intentos fallidos.
    opciones.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
    opciones.Lockout.MaxFailedAccessAttempts = 5;
    opciones.Lockout.AllowedForNewUsers = true;

    // Unicidad de email y usuarios.
    opciones.User.RequireUniqueEmail = true;
    //opciones.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789.-_@";

    // SignIn. (requisitos para iniciar sesión).
    opciones.SignIn.RequireConfirmedEmail = false;
    opciones.SignIn.RequireConfirmedPhoneNumber = false;
    opciones.SignIn.RequireConfirmedAccount = false; // si usas pantillas para la confirmación de cuenta, puedes ponerlo en true.
});

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
