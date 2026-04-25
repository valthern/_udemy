using BlogCoreSolution.AccesoDatos.Data.Repository.IRepository;
using BlogCoreSolution.Models;
using BlogCoreSolution.Models.ViewModels;
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

        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Slider slider)
        {
            if (ModelState.IsValid)
            {
                string rutaPrincipal = hostingEnvironment.WebRootPath;
                var archivos = HttpContext.Request.Form.Files;

                if (archivos.Count > 0)
                {
                    var ruta = @"\imagenes\sliders\";
                    string nombreArchivo = Guid.NewGuid().ToString();
                    // También se puede usar el método Substring(1) para eliminar el primer carácter de la ruta, al igual que su notación de rango [1..]
                    //var subidas = Path.Combine(rutaPrincipal, ruta.TrimStart('\\'));
                    var subidas = Path.Combine(rutaPrincipal, ruta[1..]);
                    var extension = Path.GetExtension(archivos[0].FileName);

                    using (FileStream fileStreams = new(Path.Combine(subidas, nombreArchivo + extension), FileMode.Create))
                        archivos[0].CopyTo(fileStreams);

                    slider.UrlImagen = ruta + nombreArchivo + extension;

                    contenedorTrabajo.Slider.Add(slider);
                    contenedorTrabajo.Save();

                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("Imagen", "Debes seleccionar una imagen");
                }
            }

            return View(slider);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id is not null)
            {
                var slider = contenedorTrabajo.Slider.Get(id.GetValueOrDefault());
                return View(slider);
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Slider slider)
        {
            if (ModelState.IsValid)
            {
                // Nueva imagen para el artículo
                string rutaPrincipal = hostingEnvironment.WebRootPath;
                var archivos = HttpContext.Request.Form.Files;
                var sliderDb = contenedorTrabajo.Slider.Get(slider.Id);

                if (archivos.Count > 0)
                {
                    var ruta = @"\imagenes\sliders\";
                    var nombreArchivo = Guid.NewGuid().ToString();
                    var subidas = Path.Combine(rutaPrincipal, ruta[1..]);
                    var extension = Path.GetExtension(archivos[0].FileName);
                    //var nuevaExtension = Path.GetExtension(archivos[0].FileName);

                    var rutaImagen = Path.Combine(rutaPrincipal, sliderDb.UrlImagen.TrimStart('\\'));

                    if (System.IO.File.Exists(rutaImagen))
                        System.IO.File.Delete(rutaImagen);

                    using (FileStream fileStreams = new(Path.Combine(subidas, nombreArchivo + extension), FileMode.Create))
                        archivos[0].CopyTo(fileStreams);

                    slider.UrlImagen = ruta + nombreArchivo + extension;
                }

                contenedorTrabajo.Slider.Update(slider);
                contenedorTrabajo.Save();

                return RedirectToAction(nameof(Index));
            }

            return View(slider);
        }


        #region Llamadas a la API
        public IActionResult GetAll() => Json(new { data = contenedorTrabajo.Slider.GetAll() });

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
