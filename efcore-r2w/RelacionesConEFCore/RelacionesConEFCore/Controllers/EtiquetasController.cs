using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RelacionesConEFCore.Data;

namespace RelacionesConEFCore.Controllers
{
    public class EtiquetasController:Controller
    {
        private readonly ApplicationDbContext context;

        public EtiquetasController(ApplicationDbContext context) => this.context = context;

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var etiqueta = await context.Etiquetas
                .Include(e => e.Articulos)
                .ToListAsync();

            return View(etiqueta);
        }
    }
}
