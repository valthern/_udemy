using Microsoft.AspNetCore.Mvc;

namespace ProyectoEjemploMVC.Controllers
{
    public class PruebitaController : Controller
    {
        public IActionResult Index() => Content("Esta es una pinche pruebita pequeña");
    }
}
