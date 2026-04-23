using BlogCoreSolution.AccesoDatos.Data.Repository.IRepository;
using BlogCoreSolution.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlogCore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SlidersController : Controller
    {
        private readonly IContenedorTrabajo contenedorTrabajo;
        private readonly IWebHostEnvironment hostingEnvironment;

        public SlidersController(IContenedorTrabajo contenedorTrabajo, IWebHostEnvironment hostingEnvironment)
        {
            this.contenedorTrabajo = contenedorTrabajo;
            this.hostingEnvironment = hostingEnvironment;
        }

        [HttpGet]
        public IActionResult Index() => View();

        #region Llamadas a la API
        public IActionResult GetAll() => Json(new { data = contenedorTrabajo.Slider.GetAll(includeProperties: nameof(Slider)) });

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var sliderDb = contenedorTrabajo.Slider.Get(id);
            var rutaDirectorioPrincipal = hostingEnvironment.WebRootPath;
            var rutaImagen = Path.Combine(rutaDirectorioPrincipal, sliderDb.UrlImagen[1..]);

            if (System.IO.File.Exists(rutaImagen))
                System.IO.File.Delete(rutaImagen);

            if (sliderDb is null)
                return Json(new { success = false, message = "Error al eliminar el slider" });

            contenedorTrabajo.Slider.Remove(sliderDb);
            contenedorTrabajo.Save();
            return Json(new { success = true, message = "Se borró el slider exitosamente" });
        }
        #endregion
    }
}
