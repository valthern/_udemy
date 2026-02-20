using CrudContactosMVC.Data;
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

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var contactos = await context.Contactos.ToListAsync();
            return View(contactos);
        }
    }
}
