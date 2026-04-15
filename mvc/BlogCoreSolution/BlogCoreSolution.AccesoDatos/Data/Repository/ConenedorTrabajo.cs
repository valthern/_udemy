using BlogCoreSolution.AccesoDatos.Data.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Net.Quic;
using System.Text;

namespace BlogCoreSolution.AccesoDatos.Data.Repository
{
    public class ContenedorTrabajo : IContenedorTrabajo
    {
        private readonly ApplicationDbContext context;
        public ICategoriaRepository Categoria { get; private set; }
        public IArticuloRepository Articulo {  get; private set; }

        public ContenedorTrabajo(ApplicationDbContext context)
        {
            this.context = context;
            Categoria = new CategoriaRepository(context);
            Articulo = new ArticuloRepository(context);
        }

        public void Save() => context.SaveChanges();

        public void Dispose() => context.Dispose();
    }
}
