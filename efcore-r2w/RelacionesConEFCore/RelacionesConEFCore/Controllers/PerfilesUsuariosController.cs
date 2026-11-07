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

    public async Task<IActionResult> Editar(int? id)
    {
        if (id is null) return NotFound();

        var perfil = await context.PerfilesUsuarios
            .Include(pu => pu.Usuario)
            .SingleOrDefaultAsync(pu => pu.UsuarioId == id);
        //.FindAsync(id);

        //var perfil = (from pu in context.PerfilesUsuarios
        //             where pu.UsuarioId == id
        //             let NombreUsuario = pu.Usuario.Nombre
        //             select new
        //             {
        //                 pu.UsuarioId,
        //                 pu.Biografia,
        //                 pu.FotoUrl,
        //                 NombreUsuario
        //             }).SingleOrDefault();

        //ViewBag.NombreUsuario = context.Usuarios.Where(u => u.Id == id).Single().Nombre;
        return View(perfil);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Editar(int id, [Bind("UsuarioId, Biografia, FotoUrl")] PerfilUsuario perfilUsuario)
    {
        if (id != perfilUsuario.UsuarioId) return NotFound();

        if (ModelState.IsValid)
        {
            try
            {
                context.Update(perfilUsuario);
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!context.PerfilesUsuarios.Any(pu => pu.UsuarioId == perfilUsuario.UsuarioId))
                    return NotFound();
                else
                    throw;
            }

            return RedirectToAction(nameof(Index));
        }

        return View(perfilUsuario);
    }

    public async Task<IActionResult> Borrar(int? id)
    {
        if (id is null) return NotFound();

        var perfil = await context.PerfilesUsuarios
            .Include(pu => pu.Usuario)
            .FirstOrDefaultAsync(pu => pu.UsuarioId == id);

        if (perfil is null) return NotFound();

        return View(perfil);
    }

    [HttpPost, ActionName("Borrar")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> BorrarConfirmar(int id)
    {
        var perfil = await context.PerfilesUsuarios.FindAsync(id);

        if (perfil is not null)
        {
            context.PerfilesUsuarios.Remove(perfil);
            await context.SaveChangesAsync();
        }

        return RedirectToAction(nameof(Index));
    }
}
