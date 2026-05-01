using BlogCoreSolution.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogCoreSolution.AccesoDatos.Data.Repository.IRepository
{
    public interface IUsuarioRepository
    {
        ApplicationUser ObtenerUsuario(string idUsuario);
        IEnumerable<ApplicationUser> ObtenerTodos();
        void BloquearUsuario(string idUsuario);
        void DesbloquearUsuario(string idUsuario);
    }
}
