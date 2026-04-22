using BlogCoreSolution.AccesoDatos.Data.Repository.IRepository;
using BlogCoreSolution.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogCoreSolution.AccesoDatos.Data.Repository
{
    public class SliderRepository : Repository<Slider>, ISliderRepository
    {
        private readonly ApplicationDbContext ctx;

        public SliderRepository(ApplicationDbContext ctx) : base(ctx) => this.ctx = ctx;

        public void Update(Slider slider)
        {
            ArgumentNullException.ThrowIfNull(slider);
            var sliderDb = ctx.Sliders.Find(slider.Id) ?? throw new InvalidOperationException("Slider no encontrado.");

            sliderDb.Nombre = slider.Nombre;
            sliderDb.Estado = slider.Estado;
            if (slider.UrlImagen is not null && sliderDb.UrlImagen != slider.UrlImagen)
                sliderDb.UrlImagen = slider.UrlImagen;
        }
    }
}
