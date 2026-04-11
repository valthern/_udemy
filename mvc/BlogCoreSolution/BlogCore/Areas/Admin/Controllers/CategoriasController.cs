using BlogCoreSolution.AccesoDatos.Data.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace BlogCore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoriasController : Controller
    {
        private readonly IContenedorTrabajo contenedorTrabajo;

        public CategoriasController(IContenedorTrabajo contenedorTrabajo)
        {
            this.contenedorTrabajo = contenedorTrabajo;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        #region API
        public IActionResult GetAll()
        {
            return Json(new { data = contenedorTrabajo.Categoria.GetAll() });
        }
        #endregion 
    }
}
