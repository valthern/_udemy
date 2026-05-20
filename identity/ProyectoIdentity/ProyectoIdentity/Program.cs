using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using ProyectoIdentity.Datos;
using ProyectoIdentity.Models;
using ProyectoIdentity.Services;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

//var nombreCadCon = "csiTiDevConnection";
//var cadenaConexionBd = builder.Configuration.GetConnectionString(nombreCadCon);
//builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(cadenaConexionBd));

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("csiTiDevConnection")));

// Se agrega el soporte para Identity (usuario personalizado + roles + tokens).
builder.Services
    .AddIdentity<AppUsuarios, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// Registrar el servicio de envío de emails con Google(inyección de dependencias).
// Ahora cualquer parte del sistema puede enviar correos utilizando IEmailSender.
builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));
builder.Services.AddTransient<IEmailSender, GmailEmailSender>();

builder.Services.ConfigureApplicationCookie(opciones =>
{
    // Expiración y "RememberMe"
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

    // Propiedaes de la cookie.
    opciones.Cookie.Name = Assembly.GetExecutingAssembly().GetName().Name;
    opciones.Cookie.HttpOnly = true;
    // La cookie solo se enviará en solicitudes del mismo sitio y navegación cross-site (p.ej: enlaces externos). Puedes usar Strict para mayor seguridad, pero puede afectar la experiencia del usuario.
    opciones.Cookie.SameSite = SameSiteMode.Lax;
    // Exige que la cookie solo se transmita a través de conexiones seguras (HTTPS). Es recomendable habilitarlo en producción.
    opciones.Cookie.SecurePolicy = CookieSecurePolicy.Always;
});

builder.Services.Configure<SecurityStampValidatorOptions>(opciones =>
{
    opciones.ValidationInterval = TimeSpan.FromMinutes(30); // Intervalo de validación del sello de seguridad.
    // Para renovación inmediata en escenarios críticos:
    //opciones.ValidationInterval = TimeSpan.Zero; // Valida el sello de seguridad en cada solicitud.
});

// Requerir Https para todas las solicitudes (recomendado en producción).
//builder.Services.AddHttpsRedirection(opciones => opciones.HttpsPort = 443);

builder.Services.Configure<IdentityOptions>(opciones =>
{
    // Configuración de la contraseña.
    opciones.Password.RequireDigit = true; // Requiere al menos un dígito.
    opciones.Password.RequiredLength = 6; // Longitud mínima de la contraseña.
    opciones.Password.RequireNonAlphanumeric = false; // No requiere caracteres no alfanuméricos (p.ej: !@#$%).
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
