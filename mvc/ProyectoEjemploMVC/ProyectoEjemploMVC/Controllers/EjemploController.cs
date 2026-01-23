using Microsoft.AspNetCore.Mvc;

namespace ProyectoEjemploMVC.Controllers
{
    public class EjemploController : Controller
    {
        public IActionResult Index() => Content("Esta es la acción Index del controlador \"Ejemplo\"");

        public IActionResult Detalle(int id) => Content($"Detalle del controlador con ID: {id}");
    }
}
