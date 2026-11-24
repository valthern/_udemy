using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RelacionesConEFCore.Data;
using RelacionesConEFCore.DTOs;
using RelacionesConEFCore.Models.Entities;

namespace RelacionesConEFCore.Controllers;

public class ArticulosController : Controller
{
    private readonly ApplicationDbContext context;

    public ArticulosController(ApplicationDbContext context) => this.context = context;

    // Carga Diligente (Eager Loading con Include y ThenInclude)
    public async Task<IActionResult> Index()
    {
        var articulos = await context.Articulos
            .Include(a => a.Categoria) // Relación 1:N
            .Include(a => a.Etiquetas) // Relación N:N
            .Include(a => a.Comentarios) // Relación 1:N
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
        if (!ModelState.IsValid)
        {
            ViewBag.Categorias = context.Categorias.ToList();
            ViewBag.Etiquetas = context.Etiquetas.ToList();
            return View(articulo);
        }

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

    [HttpGet]
    public async Task<IActionResult> Detalle(int? id)
    {
        if (id is null) return NotFound();

        var articulo = await context.Articulos
            .Include(a => a.Categoria) //1:N
            .Include(a => a.Etiquetas) //N:N
            .Include(a => a.Comentarios) //1:N
            .FirstOrDefaultAsync(a => a.Id == id);

        if (articulo is null) return NotFound();

        return View(articulo);
    }

    [HttpGet]
    public async Task<IActionResult> Editar(int? id)
    {
        if (id is null) return NotFound();

        var articulo = await context.Articulos
            .Include(a => a.Etiquetas) //N:N
            .FirstOrDefaultAsync(a => a.Id == id);

        if (articulo is null) return NotFound();

        ViewBag.Categorias = context.Categorias.ToList();
        ViewBag.Etiquetas = context.Etiquetas.ToList();

        return View(articulo);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Editar(int id, Articulo articulo, int[] etiquetasSeleccionadas)
    {
        if (id != articulo.Id) return NotFound();

        if (ModelState.IsValid)
        {
            try
            {
                var articuloExistente = await context.Articulos
                    .Include(a => a.Etiquetas)
                    .FirstOrDefaultAsync(a => a.Id == id);

                if (articuloExistente is null) return NotFound();

                articuloExistente.Titulo = articulo.Titulo;
                articuloExistente.Contenido = articulo.Contenido;
                articuloExistente.CategoriaId = articulo.CategoriaId;

                if (etiquetasSeleccionadas is not null)
                {
                    articuloExistente.Etiquetas.Clear();

                    foreach (var eid in etiquetasSeleccionadas)
                    {
                        var etiqueta = await context.Etiquetas.FindAsync(eid);
                        if (etiqueta is not null)
                            articuloExistente.Etiquetas.Add(etiqueta);
                    }
                }

                context.Update(articuloExistente);
                await context.SaveChangesAsync();
            }
            catch (Exception)
            {
                if (!context.Articulos.Any(a => a.Id == articulo.Id))
                    return NotFound();
                else throw;
            }

            return RedirectToAction(nameof(Index));
        }

        return View(articulo);
    }

    [HttpGet]
    public async Task<IActionResult> Borrar(int? id)
    {
        if (id is null) return NotFound();

        var articulo = await context.Articulos
            .Include(a => a.Categoria)
            .FirstOrDefaultAsync(a => a.Id == id);

        if (articulo is null) return NotFound();

        return View(articulo);
    }

    [HttpPost, ActionName("Borrar")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> BorrarConfirmado(int? id)
    {
        var articulo = await context.Articulos
            .Include(a => a.Comentarios) //1:N
            .FirstOrDefaultAsync(a => a.Id == id);

        if (articulo is not null)
        {
            if (articulo.Comentarios is not null)
            {
                context.Comentarios.RemoveRange(articulo.Comentarios);
                context.Articulos.Remove(articulo);
                await context.SaveChangesAsync();
            }
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> ArticuloPorCategoria(int? categoriaId)
    {
        var articulos = context.Articulos
            .Include(a => a.Categoria) //1:N
            .Include(a => a.Etiquetas) //N:N
            .AsQueryable();

        if (categoriaId.HasValue)
            articulos = articulos.Where(a => a.CategoriaId == categoriaId.Value);

        ViewBag.Categorias = new SelectList(context.Categorias, "Id", "Nombre", categoriaId);

        return View(await articulos.ToListAsync());
    }

    [HttpGet]
    public async Task<IActionResult> ArticulosOrdenamiento()
    {
        var articulos = context.Articulos
            .Include(a => a.Categoria) //1:N
            .Include(a => a.Etiquetas) //N:N
            .OrderBy(a => a.Categoria.Nombre)
            .ThenBy(a => a.Titulo)
            .ToList();

        return View(articulos);
    }

    public async Task<IActionResult> Resumen()
    {
        var resumen = await context.Articulos
            .Select(a => new ArticuloResumenDTO
            {
                Titulo = a.Titulo,
                Categoria = a.Categoria.Nombre,
                TotalComentarios = a.Comentarios.Count()
            }).ToListAsync();

        return View(resumen);
    }

    public async Task<IActionResult> Busqueda(string busqueda)
    {
        var query = context.Articulos
            .Include(a => a.Categoria)
            .Include(a => a.Etiquetas)
            .AsQueryable();

        if (!string.IsNullOrEmpty(busqueda))
            query = query.Where(a => a.Titulo.Contains(busqueda));
        //query = query.Where(a => a.Titulo.StartsWith(busqueda));
        //query = query.Where(a => a.Titulo.EndsWith(busqueda));

        var articulos = await query.ToListAsync();
        // para que recuerde el texto en la vista.
        ViewBag.Busqueda = busqueda;

        return View(articulos);
    }

    public async Task<IActionResult> RecienteSQL()
    {
        var articulos = await context.Articulos
            .FromSqlRaw(@"
                SELECT TOP 5 a.*
                FROM blog.Articulos a
                INNER JOIN blog.Categorias c
                ON a.CategoriaId = c.Id
                ORDER BY a.Id DESC
            ")
            .Include(a => a.Categoria)
            .ToListAsync();

        return View(articulos);
    }

    public async Task<IActionResult> PorCategoriaSQL(int categoriaId)
    {
        var articulos = await context.Articulos
            .FromSqlInterpolated(@$"
                SELECT *
                FROM blog.Articulos
                WHERE blog.Articulos.CategoriaId = {categoriaId}
                ORDER BY blog.Articulos.Id DESC
            ")
            .Include(a => a.Categoria)
            .ToListAsync();

        return View("RecientesSQL", articulos);
    }
}
