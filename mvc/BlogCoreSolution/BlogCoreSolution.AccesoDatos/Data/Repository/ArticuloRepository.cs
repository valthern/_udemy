using BlogCoreSolution.AccesoDatos.Data.Repository.IRepository;
using BlogCoreSolution.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogCoreSolution.AccesoDatos.Data.Repository
{
    public class ArticuloRepository : Repository<Articulo>, IArticuloRepository
    {
        private readonly ApplicationDbContext ctx;

        public ArticuloRepository(ApplicationDbContext context) : base(context) => ctx = context;

        public void Update(Articulo articulo)
        {
            var objDesdeDb = ctx.Articulos.FirstOrDefault(a => a.Id == articulo.Id);
            //if (objDesdeDb is not null)
            //{
            objDesdeDb.Nombre = articulo.Nombre;
            objDesdeDb.Descripcion = articulo.Descripcion;
            //objDesdeDb.FechaCreacion = articulo.FechaCreacion;
            if (articulo.UrlImagen is not null && objDesdeDb.UrlImagen != articulo.UrlImagen)
                objDesdeDb.UrlImagen = articulo.UrlImagen;
            objDesdeDb.CategoriaId = articulo.CategoriaId;
            //}
            //}            
        }
    }
}
