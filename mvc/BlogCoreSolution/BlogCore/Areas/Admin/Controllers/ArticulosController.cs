using BlogCoreSolution.AccesoDatos.Data.Repository.IRepository;
using BlogCoreSolution.Models;
using BlogCoreSolution.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BlogCore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ArticulosController : Controller
    {
        private readonly IContenedorTrabajo contenedorTrabajo;
        private readonly IWebHostEnvironment hostingEnvironment;

        public ArticulosController(IContenedorTrabajo contenedorTrabajo, IWebHostEnvironment hostingEnvironment)
        {
            this.contenedorTrabajo = contenedorTrabajo;
            this.hostingEnvironment = hostingEnvironment;
        }

        [HttpGet]
        public IActionResult Index() => View();

        [HttpGet]
        public IActionResult Create()
        {
            ArticuloVM articuloVM = new()
            {
                Articulo = new Articulo(),
                ListaCategorias = contenedorTrabajo.Categoria.GetListaCategoria()
            };

            return View(articuloVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ArticuloVM articuloVM)
        {
            if (ModelState.IsValid)
            {
                string rutaPrincipal = hostingEnvironment.WebRootPath;
                var archivos = HttpContext.Request.Form.Files;

                if (articuloVM.Articulo.Id == 0 && archivos.Count > 0)
                {
                    var ruta = @"imagenes\articulos";
                    string nombreArchivo = Guid.NewGuid().ToString();
                    var subidas = Path.Combine(rutaPrincipal, ruta);
                    var extension = Path.GetExtension(archivos[0].FileName);

                    using (FileStream fileStreams = new(Path.Combine(subidas, nombreArchivo + extension), FileMode.Create))
                    {
                        archivos[0].CopyTo(fileStreams);
                    }

                    articuloVM.Articulo.UrlImagen = ruta + nombreArchivo + extension;

                    contenedorTrabajo.Articulo.Add(articuloVM.Articulo);
                    contenedorTrabajo.Save();

                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("Imagen", "Debes seleccionar una imagen");
                }
            }

            articuloVM.ListaCategorias = contenedorTrabajo.Categoria.GetListaCategoria();
            return View(articuloVM);
        }

        #region Llamadas a la API
        public IActionResult GetAll() => Json(new { data = contenedorTrabajo.Articulo.GetAll() });

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var objFromDb = contenedorTrabajo.Articulo.Get(id);
            if ((objFromDb is null))
                return Json(new { success = false, message = "Error borrando el artículo" });

            contenedorTrabajo.Articulo.Remove(objFromDb);
            contenedorTrabajo.Save();
            return Json(new { success = true, message = "Se borró el artículo exitosamente" });
        }
        #endregion 

    }
}
