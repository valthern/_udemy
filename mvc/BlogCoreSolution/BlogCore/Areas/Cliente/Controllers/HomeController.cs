using BlogCoreSolution.AccesoDatos.Data.Repository.IRepository;
using BlogCoreSolution.Models;
using BlogCoreSolution.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BlogCore.Areas.Cliente.Controllers
{
    [Area("Cliente")]
    public class HomeController : Controller
    {
        private readonly IContenedorTrabajo contenedorTrabajo;

        public HomeController(IContenedorTrabajo contenedorTrabajo)
        {
            this.contenedorTrabajo = contenedorTrabajo;
        }

        [HttpGet]
        public IActionResult Index()
        {
            HomeVM homeVM = new()
            {
                Sliders = contenedorTrabajo.Slider.GetAll(),
                Articulos = contenedorTrabajo.Articulo.GetAll()
            };

            ViewBag.IsHome = true;
            return View(homeVM);
        }

        [HttpGet]
        public IActionResult Detalle(int id)
        {
            var articuloBd = contenedorTrabajo.Articulo.Get(id);
            return View(articuloBd);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
