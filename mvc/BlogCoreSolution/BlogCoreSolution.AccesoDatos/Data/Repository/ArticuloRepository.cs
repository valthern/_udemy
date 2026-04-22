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

        public ArticuloRepository(ApplicationDbContext ctx) : base(ctx) => this.ctx = ctx;

        public void Update(Articulo articulo)
        {
            ArgumentNullException.ThrowIfNull(articulo);
            var objDesdeDb = ctx.Articulos.Find(articulo.Id) ?? throw new InvalidOperationException("Artículo no encontrado.");

            objDesdeDb.Nombre = articulo.Nombre;
            objDesdeDb.Descripcion = articulo.Descripcion;
            if (articulo.UrlImagen is not null && objDesdeDb.UrlImagen != articulo.UrlImagen)
                objDesdeDb.UrlImagen = articulo.UrlImagen;
            objDesdeDb.CategoriaId = articulo.CategoriaId;
        }
    }
}
