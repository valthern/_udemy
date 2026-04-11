using BlogCoreSolution.AccesoDatos.Data.Repository.IRepository;
using BlogCoreSolution.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogCoreSolution.AccesoDatos.Data.Repository
{
    public class CategoriaRepository : Repository<Categoria>, ICategoriaRepository
    {
        private readonly ApplicationDbContext _ctx;

        public CategoriaRepository(ApplicationDbContext context) : base(context)
        {
            _ctx = context;
        }

        public void Update(Categoria categoria)
        {
            var objDesdeDb = _ctx.Categorias.FirstOrDefault(c => c.Id == categoria.Id);
            //if (objDesdeDb is not null)
            //{
                objDesdeDb.Nombre = categoria.Nombre;
                objDesdeDb.Orden = categoria.Orden;
            //}

            // No es necesario llamar a context.Categorias.Update(objDesdeDb) porque el objeto ya está siendo rastreado por el contexto.
            // El contexto detectará automáticamente los cambios realizados en objDesdeDb y los aplicará cuando se llame a SaveChanges() en la capa de "Unidad de Trabajo".
            //context.SaveChanges();  /* Esto no se pone aquí */
            
        }
    }
}
