using CursoEFCoreDatabaseFirst.Data;
using CursoEFCoreDatabaseFirst.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CursoEFCoreDatabaseFirst.Controllers;

public class HomeController : Controller
{
    private readonly ApplicationDbContext context;

    public HomeController(ApplicationDbContext context) => this.context = context;

    public IActionResult Index()
    {
        var categorias = context.categorias.ToList();
        return View(categorias);
    }

    public IActionResult Privacy() => View();

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error() => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
}
