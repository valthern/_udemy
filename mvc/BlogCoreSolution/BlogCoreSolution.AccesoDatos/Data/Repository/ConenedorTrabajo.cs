using BlogCoreSolution.AccesoDatos.Data.Repository.IRepository;
using BlogCoreSolution.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Net.Quic;
using System.Text;

namespace BlogCoreSolution.AccesoDatos.Data.Repository
{
    public class ContenedorTrabajo : IContenedorTrabajo
    {
        private readonly ApplicationDbContext context;
        //private readonly UserManager<ApplicationUser> userManager;

        public ICategoriaRepository Categoria { get; private set; }
        public IArticuloRepository Articulo { get; private set; }
        public ISliderRepository Slider { get; private set; }
        public IUsuarioRepository Usuario { get; private set; }

        public ContenedorTrabajo(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            this.context = context;
            //this.userManager = userManager;
            Categoria = new CategoriaRepository(context);
            Articulo = new ArticuloRepository(context);
            Slider = new SliderRepository(context);
            Usuario = new UsuarioRepository(context, userManager);
        }

        public void Save() => context.SaveChanges();

        public void Dispose() => context.Dispose();
    }
}
