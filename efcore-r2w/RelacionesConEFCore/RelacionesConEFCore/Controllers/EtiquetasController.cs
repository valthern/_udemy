using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RelacionesConEFCore.Data;
using RelacionesConEFCore.Models.Entities;

namespace RelacionesConEFCore.Controllers;

public class EtiquetasController : Controller
{
    private readonly ApplicationDbContext context;

    public EtiquetasController(ApplicationDbContext context) => this.context = context;

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var etiquetas = await context.Etiquetas
            .Include(e => e.Articulos)
            .ToListAsync();

        return View(etiquetas);
    }


    public async Task<IActionResult> Detalle(int? id)
    {
        if (id is null) return NotFound();

        var etiqueta = await context.Etiquetas
            .Include(e => e.Articulos)
            .FirstOrDefaultAsync(e => e.Id == id);

        if (etiqueta is null) return NotFound();

        return View(etiqueta);
    }

    public IActionResult Crear() => View();

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Crear([Bind("Id, Nombre")] Etiqueta etiqueta)
    {
        if (ModelState.IsValid)
        {
            context.Add(etiqueta);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        return View(etiqueta);
    }

    public async Task<IActionResult> Editar(int? id)
    {
        if (id is null) return NotFound();

        var etiqueta = await context.Etiquetas.FindAsync(id);
        if (etiqueta is null) return NotFound();

        return View(etiqueta);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Editar(int id, [Bind("Id, Nombre")] Etiqueta etiqueta)
    {
        if (id != etiqueta.Id) return NotFound();

        if (ModelState.IsValid)
        {
            try
            {
                context.Update(etiqueta);
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!context.Etiquetas.Any(e => e.Id == etiqueta.Id))
                    return NotFound();
                else
                    throw;
            }

            return RedirectToAction(nameof(Index));
        }

        return View(etiqueta);
    }
}
