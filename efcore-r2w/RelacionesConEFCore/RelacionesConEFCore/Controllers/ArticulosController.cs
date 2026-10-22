using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RelacionesConEFCore.Data;
using RelacionesConEFCore.Models.Entities;

namespace RelacionesConEFCore.Controllers;

[Controller]
public class ArticulosController : Controller
{
    private readonly ApplicationDbContext context;

    public ArticulosController(ApplicationDbContext context) => this.context = context;

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var articulos = await context.Articulos
            .Include(a => a.Categoria)
            .Include(a => a.Etiquetas)
            .Include(a => a.Comentarios)
            .ToListAsync();

        return View(articulos);
    }

    public IActionResult Crear()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Crear([Bind("Id, Nombre")] Categoria categoria)
    {
        if (ModelState.IsValid)
        {
            context.Add(categoria);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        return View(categoria);
    }
}
