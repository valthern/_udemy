using BlogCoreSolution.AccesoDatos.Data.Repository.IRepository;
using BlogCoreSolution.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlogCore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoriasController : Controller
    {
        private readonly IContenedorTrabajo contenedorTrabajo;

        public CategoriasController(IContenedorTrabajo contenedorTrabajo) => this.contenedorTrabajo = contenedorTrabajo;

        [HttpGet]
        public IActionResult Index() => View();

        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                // Lógica para agregar la categoría a la base de datos
                contenedorTrabajo.Categoria.Add(categoria);
                contenedorTrabajo.Save();
                return RedirectToAction(nameof(Index));
            }

            return View();
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            Categoria categoria = new();
            categoria = contenedorTrabajo.Categoria.Get(id);
            if (categoria is null) return NotFound();

            return View(categoria);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                contenedorTrabajo.Categoria.Update(categoria);
                contenedorTrabajo.Save();
                return RedirectToAction(nameof(Index));
            }

            return View(categoria);
        }

        #region Llamadas a la API
        public IActionResult GetAll() => Json(new { data = contenedorTrabajo.Categoria.GetAll() });

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var objFromDb=contenedorTrabajo.Categoria.Get(id);
            if ((objFromDb is null))
                return Json(new { success = false, message = "Error borrando la categoría" });

            contenedorTrabajo.Categoria.Remove(objFromDb);
            contenedorTrabajo.Save();
            return Json(new { success = true, message = "Se borró la categoría exitosamente" });
        }
        #endregion 
    }
}
