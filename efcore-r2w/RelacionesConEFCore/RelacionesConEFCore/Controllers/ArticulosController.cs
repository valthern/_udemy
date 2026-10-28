using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RelacionesConEFCore.Data;
using RelacionesConEFCore.Models.Entities;

namespace RelacionesConEFCore.Controllers;

public class ArticulosController : Controller
{
    private readonly ApplicationDbContext context;

    public ArticulosController(ApplicationDbContext context) => this.context = context;

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var articulos = await context.Articulos
            .Include(a => a.Categoria) //1:N
            .Include(a => a.Etiquetas) //N:N
            .Include(a => a.Comentarios) //1:N
            .ToListAsync();

        return View(articulos);
    }

    public IActionResult Crear()
    {
        ViewBag.Categorias = context.Categorias.ToList();
        ViewBag.Etiquetas = context.Etiquetas.ToList();
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Crear(Articulo articulo, int[] etiquetasSeleccionadas)
    {
        if (!ModelState.IsValid) return View(articulo);

        if (etiquetasSeleccionadas is not null && etiquetasSeleccionadas.Any())
        {
            articulo.Etiquetas = new List<Etiqueta>();
            foreach (var id in etiquetasSeleccionadas)
            {
                var etiqueta = await context.Etiquetas.FindAsync(id);
                if (etiqueta is not null)
                    articulo.Etiquetas.Add(etiqueta);
            }
        }

        context.Add(articulo);
        await context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}
