using CursoEFCoreDatabaseFirst.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add DbContext with connection string from appsettings.json
var cadConnStrName ="csZ1TowerG1i";
var cadConnStr= builder.Configuration.GetConnectionString(cadConnStrName);
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(cadConnStr));

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
