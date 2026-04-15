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

        public ArticulosController(IContenedorTrabajo contenedorTrabajo) => this.contenedorTrabajo = contenedorTrabajo;

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
