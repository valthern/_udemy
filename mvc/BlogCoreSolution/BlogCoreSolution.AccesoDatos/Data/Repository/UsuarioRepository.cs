using BlogCoreSolution.AccesoDatos.Data.Repository.IRepository;
using BlogCoreSolution.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogCoreSolution.AccesoDatos.Data.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ApplicationDbContext ctx;

        public UsuarioRepository(ApplicationDbContext ctx, UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
            this.ctx = ctx;
        }

        public IEnumerable<ApplicationUser> ObtenerTodos(string idUsuarioActual) =>
            ctx.Users
                .Where(u => u.Id != idUsuarioActual)
                .ToList();

        public ApplicationUser ObtenerUsuario(string idUsuario) => ctx.Users.FirstOrDefault(u => u.Id == idUsuario);

        public void BloquearUsuario(string idUsuario)
        {
            var usuario = ctx.Users.Find(idUsuario) ?? throw new InvalidOperationException($"Usuario con ID {idUsuario} no encontrado.");
            usuario.LockoutEnd = DateTime.Now.AddYears(100);
            ctx.SaveChanges();
        }

        public void DesbloquearUsuario(string idUsuario)
        {
            var usuario = ctx.Users.Find(idUsuario) ?? throw new InvalidOperationException($"Usuario con ID {idUsuario} no encontrado.");
            usuario.LockoutEnd = DateTime.Now;
            ctx.SaveChanges();
        }
    }
}
