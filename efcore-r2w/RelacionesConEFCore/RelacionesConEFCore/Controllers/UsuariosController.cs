using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RelacionesConEFCore.Data;
using RelacionesConEFCore.Models.Entities;

namespace RelacionesConEFCore.Controllers;

public class UsuariosController : Controller
{
    private readonly ApplicationDbContext context;

    public UsuariosController(ApplicationDbContext context) => this.context = context;

    public async Task<IActionResult> Index()
    {
        var usuarios = await context.Usuarios
            .Include(u => u.Perfil)
            .Include(u => u.Comentarios)
            .ToListAsync();

        return View(usuarios);
    }

    public async Task<IActionResult> Detalle(int? id)
    {
        if (id is null) return NotFound();

        var usuario = await context.Usuarios
            .Include(u => u.Perfil)
            .Include(u => u.Comentarios)
            .FirstOrDefaultAsync(u => u.Id == id);

        if (usuario is null) return NotFound();

        return View(usuario);
    }

    public IActionResult Crear() => View();

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Crear([Bind("Id, Nombre, Email")] Usuario usuario)
    {
        if (ModelState.IsValid)
        {
            context.Add(usuario);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        return View(usuario);
    }

    public async Task<IActionResult> Editar(int? id)
    {
        if (id is null) return NotFound();

        var usuario = await context.Usuarios.FindAsync(id);
        if (usuario is null) return NotFound();

        return View(usuario);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Editar(int id, [Bind("Id, Nombre, Email")] Usuario usuario)
    {
        if (id != usuario.Id) return NotFound();

        if (ModelState.IsValid)
        {
            try
            {
                context.Update(usuario);
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!context.Usuarios.Any(u => u.Id == usuario.Id))
                    return NotFound();
                else
                    throw;
            }

            return RedirectToAction(nameof(Index));
        }

        return View(usuario);
    }

    public async Task<IActionResult> Borrar(int? id)
    {
        if (id is null) return NotFound();

        var usuario = await context.Usuarios.FirstOrDefaultAsync(u => u.Id == id);
        if (usuario is null) return NotFound();

        return View(usuario);
    }

    [HttpPost, ActionName("Borrar")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> BorrarConfirmar(int id)
    {
        var usuario = await context.Usuarios
            .Include(u => u.Perfil)
            .Include(u => u.Comentarios)
            .FirstOrDefaultAsync(u => u.Id == id);

        if (usuario is not null)
        {
            context.Usuarios.Remove(usuario);
            await context.SaveChangesAsync();
        }

        return RedirectToAction(nameof(Index));
    }
}
