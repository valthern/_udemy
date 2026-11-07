using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RelacionesConEFCore.Data;
using RelacionesConEFCore.Models.Entities;

namespace RelacionesConEFCore.Controllers;

public class ComentariosController : Controller
{
    private readonly ApplicationDbContext context;

    public ComentariosController(ApplicationDbContext context) => this.context = context;

    public async Task<IActionResult> Index()
    {
        var comentarios = await context.Comentarios
            .Include(c => c.Usuario)
            .Include(c => c.Articulo)
            .ToListAsync();

        return View(comentarios);
    }

    public async Task<IActionResult> Detalle(int? id)
    {
        if (id is null) return NotFound();

        var comentario = await context.Comentarios
            .Include(c => c.Usuario)
            .Include(c => c.Articulo)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (comentario is null) return NotFound();

        return View(comentario);
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
}
