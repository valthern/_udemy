using BlogCoreSolution.AccesoDatos.Data.Repository.IRepository;
using BlogCoreSolution.Utilidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BlogCore.Areas.Admin.Controllers
{
    [Authorize(Roles = CNT.Administrador)]
    [Area("Admin")]
    public class UsuariosController : Controller
    {
        private readonly IContenedorTrabajo contenedorTrabajo;

        public UsuariosController(IContenedorTrabajo contenedorTrabajo) => this.contenedorTrabajo = contenedorTrabajo;

        [HttpGet]
        public IActionResult Index()
        {
            //return View(contenedorTrabajo.Usuario.ObtenerTodos());
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var usuarioActual = claimsIdentity!.FindFirst(ClaimTypes.NameIdentifier);
            return View(contenedorTrabajo.Usuario.ObtenerTodos(usuarioActual?.Value!));
        }

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
