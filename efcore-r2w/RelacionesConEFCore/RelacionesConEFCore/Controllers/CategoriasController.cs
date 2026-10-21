using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RelacionesConEFCore.Data;
using RelacionesConEFCore.Models.Entities;
using System.Security.Cryptography.Pkcs;

namespace RelacionesConEFCore.Controllers;

public class CategoriasController : Controller
{
    private readonly ApplicationDbContext context;

    public CategoriasController(ApplicationDbContext context) => this.context = context;

    public async Task<IActionResult> Index()
    {
        var categorias = await context.Categorias.ToListAsync();
        return View(categorias);
    }

    public async Task<IActionResult> Detalle(int? id)
    {
        if (id is null) return NotFound();

        var categoria = await context.Categorias
            .Include(c => c.Articulos)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (categoria is null) return NotFound();

        return View(categoria);
    }

    public IActionResult Crear() => View();

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

    public async Task<IActionResult> Editar(int? id)
    {
        if (id is null) return NotFound();

        var categoria = await context.Categorias.FindAsync(id);
        if (categoria is null) return NotFound();

        return View(categoria);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Editar(int id, [Bind("Id, Nombre")] Categoria categoria)
    {
        if (id != categoria.Id) return NotFound();

        if (ModelState.IsValid)
        {
            try
            {
                context.Update(categoria);
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!context.Categorias.Any(c => c.Id == categoria.Id))
                    return NotFound();
                else
                    throw;
            }

            return RedirectToAction(nameof(Index));
        }

        return View(categoria);
    }

    public async Task<IActionResult> Borrar(int? id)
    {
        if (id is null) return NotFound();

        var categoria = await context.Categorias.FirstOrDefaultAsync(c => c.Id == id);
        if (categoria is null) return NotFound();

        return View(categoria);
    }

    [HttpPost, ActionName("Borrar")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> BorrarConfirmar(int id)
    {
        var categoria = await context.Categorias.FindAsync(id);

        if (categoria is not null)
        {
            context.Categorias.Remove(categoria);
            await context.SaveChangesAsync();
        }

        return RedirectToAction(nameof(Index));
    }
}
