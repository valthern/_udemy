using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RelacionesConEFCore.Data;
using RelacionesConEFCore.DTOs;
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

    public async Task<IActionResult> IndexPaginado(int pagina = 1, int tamanoPagina = 5)
    {
        // Validaciones
        if (pagina < 1) pagina = 1;
        if (tamanoPagina < 1 || tamanoPagina > 100) tamanoPagina = 5;

        var query = context.Comentarios
            .Include(c => c.Usuario)
            .Include(c => c.Articulo)
            .AsNoTracking();

        // Calcular el total de registros (para calcular el número de páginas).
        var total = await query.CountAsync();

        // Total de páginas (al menos 1 para no romper la vista).
        var totalPaginas = (int)Math.Ceiling(total / (double)tamanoPagina);
        if (totalPaginas == 0) totalPaginas = 1;
        if (pagina > totalPaginas) pagina = totalPaginas;

        // orden estable antes de paginar. Orden invertido para que los comentarios más recientes aparezcan primero.
        var comentarios = await query
            .OrderByDescending(c => c.Id)
            // salta los registros de las páginas anteriores y ...
            .Skip((pagina - 1) * tamanoPagina)
            // toma los registros de la página actual.
            .Take(tamanoPagina)
            .ToListAsync();

        var vm = new ComentariosIndexDTO
        {
            Comentarios = comentarios,
            PaginaActual = pagina,
            TotalPaginas = totalPaginas,
            TamanoPagina = tamanoPagina,
            TotalRegistros = total
        };

        return View(vm);
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

    public IActionResult Crear()
    {
        ViewBag.Usuarios = context.Usuarios.ToList();
        ViewBag.Articulos = context.Articulos.ToList();

        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Crear([Bind("Id, Texto, UsuarioId, ArticuloId, Aprobado")] Comentario comentario)
    {
        if (ModelState.IsValid)
        {
            context.Add(comentario);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        ViewBag.Usuarios = context.Usuarios.ToList();
        ViewBag.Articulos = context.Articulos.ToList();

        return View(comentario);
    }

    public async Task<IActionResult> Editar(int? id)
    {
        if (id is null) return NotFound();

        var comentario = await context.Comentarios.FindAsync(id);
        if (comentario is null) return NotFound();

        ViewBag.Usuarios = context.Usuarios.ToList();
        ViewBag.Articulos = context.Articulos.ToList();

        return View(comentario);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Editar(int id, [Bind("Id, Texto, UsuarioId, ArticuloId, Aprobado")] Comentario comentario)
    {
        if (id != comentario.Id) return NotFound();

        if (ModelState.IsValid)
        {
            try
            {
                context.Update(comentario);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!context.Comentarios.Any(c => c.Id == comentario.Id))
                    return NotFound();
                else
                    throw;
            }
        }

        ViewBag.Usuarios = context.Usuarios.ToList();
        ViewBag.Articulos = context.Articulos.ToList();

        return View(comentario);
    }

    public async Task<IActionResult> Borrar(int? id)
    {
        if (id is null) return NotFound();

        var comentario = await context.Comentarios
            .Include(c => c.Usuario)
            .Include(c => c.Articulo)
            .SingleOrDefaultAsync(c => c.Id == id);

        if (comentario is null) return NotFound();

        return View(comentario);
    }

    [HttpPost, ActionName("Borrar")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> BorrarConfirmar(int id)
    {
        var comentario = await context.Comentarios.FirstAsync(c => c.Id == id);

        if (comentario is not null)
        {
            context.Comentarios.Remove(comentario);
            await context.SaveChangesAsync();
        }

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Estadisticas()
    {
        var datos = await context.Comentarios
            .GroupBy(c => c.Usuario.Nombre)
            .Select(g => new DTOs.UsuarioComentariosDTO
            {
                Usuario = g.Key,
                TotalComentarios = g.Count()
            })
            .OrderByDescending(c => c.TotalComentarios)
            .ToListAsync();

        if (datos is null) return NotFound();

        return View(datos);
    }
}
