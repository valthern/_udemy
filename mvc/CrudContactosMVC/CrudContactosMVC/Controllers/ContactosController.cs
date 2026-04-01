using CrudContactosMVC.Data;
using CrudContactosMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CrudContactosMVC.Controllers
{
    public class ContactosController : Controller
    {
        private readonly ApplicationDbContext context;

        public ContactosController(ApplicationDbContext context)
        {
            this.context = context;
        }

        // GET: Contactos
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var contactos = await context.Contactos.ToListAsync();
            return View(contactos);
        }

        [HttpGet]
        public IActionResult Crear() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear(Contacto contacto)
        {
            if (ModelState.IsValid)
            {
                context.Contactos.Add(contacto);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(contacto);
        }

        [HttpGet]
        public async Task<IActionResult> Editar(int? id)
        {
            if (id is null) return NotFound();

            var contacto = await context.Contactos.FindAsync(id);

            if (contacto is null) return NotFound();

            return View(contacto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(int id, Contacto contacto)
        {
            if (id != contacto.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    context.Update(contacto);
                    await context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContactoExists(contacto.Id))
                        return NotFound();
                    else
                        throw;
                }
            }

            return View(contacto);
        }

        [HttpGet]
        public async Task<IActionResult> Detalle(int? id)
        {
            if (id is null) return NotFound();

            var contacto = await context.Contactos.FirstOrDefaultAsync(c => c.Id == id);

            if (contacto is null) return NotFound();

            return View(contacto);
        }

        [HttpGet]
        public async Task<IActionResult> Borrar(int? id)
        {
            if (id is null) return NotFound();

            var contacto = await context.Contactos.FirstOrDefaultAsync(c => c.Id == id);

            if (contacto is null) return NotFound();

            return View(contacto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Borrar(int id)
        {
            var contacto = await context.Contactos.FindAsync(id);

            if(contacto is not null)
            {
                context.Contactos.Remove(contacto);
                await context.SaveChangesAsync();
                //return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }

        private bool ContactoExists(int id) => context.Contactos.Any(c => c.Id == id);
    }
}
