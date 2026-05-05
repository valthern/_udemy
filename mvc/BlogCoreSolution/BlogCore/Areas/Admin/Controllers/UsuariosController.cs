using BlogCoreSolution.AccesoDatos.Data.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace BlogCore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UsuariosController : Controller
    {
        private readonly IContenedorTrabajo contenedorTrabajo;

        public UsuariosController(IContenedorTrabajo contenedorTrabajo) => this.contenedorTrabajo = contenedorTrabajo;

        [HttpGet]
        public IActionResult Index() => View(contenedorTrabajo.Usuario.ObtenerTodos());

        [HttpGet]
        public IActionResult Bloquear(string id)
        {
            if (id is null) return NotFound();

            contenedorTrabajo.Usuario.BloquearUsuario(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Desbloquear(string id)
        {
            if (id is null) return NotFound();

            contenedorTrabajo.Usuario.DesbloquearUsuario(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
