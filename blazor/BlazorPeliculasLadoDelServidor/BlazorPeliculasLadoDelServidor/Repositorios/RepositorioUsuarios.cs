using BlazorPeliculasLadoDelServidor.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BlazorPeliculasLadoDelServidor.Repositorios
{
    public class RepositorioUsuarios
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<IdentityUser> userManager;

        public RepositorioUsuarios(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        public async Task<List<UsuarioDTO>> Get(PaginacionDTO paginacion)
        {
            var queryable = context.Users.AsQueryable();

            await HttpContext
                .InsertarParametrosPaginacionEnRespuesta(queryable, paginacion.CantidadRegistros);

            return await queryable
                .Paginar(paginacion)
                .Select(u => new UsuarioDTO { Id = u.Id, Email = u.Email! })
                .ToListAsync();
        }

        public async Task<List<RolDTO>> GetRoles() => await context.Roles.Select(r => new RolDTO { Nombre = r.Name! }).ToListAsync();

        public async Task AsignarRolUsuario(EditarRolDTO editarRolDTO)
        {
            var usuario = await userManager.FindByIdAsync(editarRolDTO.UsuarioId);

            if (usuario is null) return BadRequest("Usuario no existe");

            await userManager.AddToRoleAsync(usuario, editarRolDTO.Rol);
            return NoContent();
        }

        public async Task RemoverRolUsuario(EditarRolDTO editarRolDTO)
        {
            var usuario = await userManager.FindByIdAsync(editarRolDTO.UsuarioId);

            if (usuario is null) return BadRequest("Usuario no existe");

            await userManager.RemoveFromRoleAsync(usuario, editarRolDTO.Rol);
            return NoContent();
        }
    }
}
