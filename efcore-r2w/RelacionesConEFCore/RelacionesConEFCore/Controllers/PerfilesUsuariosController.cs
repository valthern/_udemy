using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RelacionesConEFCore.Data;
using RelacionesConEFCore.Models.Entities;

namespace RelacionesConEFCore.Controllers;

public class PerfilesUsuariosController : Controller
{
    private readonly ApplicationDbContext context;

    public PerfilesUsuariosController(ApplicationDbContext context) => this.context = context;

    public async Task<IActionResult> Index()
    {
        var perfilesUsuarios = await context.PerfilesUsuarios
            .Include(pu => pu.Usuario)
            .ToListAsync();

        return View(perfilesUsuarios);
    }

    public async Task<IActionResult> Detalle(int? id)
    {
        if (id is null) return NotFound();

        var perfilUsuario = await context.PerfilesUsuarios
            .Include(pu => pu.Usuario)
            .FirstOrDefaultAsync(pu => pu.UsuarioId == id);

        if (perfilUsuario is null) return NotFound();

        return View(perfilUsuario);
    }

    public IActionResult Crear()
    {
        ViewBag.Usuarios = context.Usuarios.Where(u => u.Perfil == null).ToList();

        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Crear([Bind("UsuarioId, Biografia, FotoUrl")] PerfilUsuario perfilUsuario)
    {
        if (ModelState.IsValid)
        {
            context.Add(perfilUsuario);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        ViewBag.Usuarios = context.Usuarios.Where(u => u.Perfil == null).ToList();
        return View(perfilUsuario);
    }
}
