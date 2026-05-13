using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Abstractions;
using ProyectoIdentity.Models;

namespace ProyectoIdentity.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUsuarios> userManager;
        private readonly SignInManager<AppUsuarios> signInManager;

        public AccountController(UserManager<AppUsuarios> userManager, SignInManager<AppUsuarios> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Registro() => View();

        [HttpPost]
        public async Task<IActionResult> Registro(RegistroViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            AppUsuarios usuario = new()
            {
                UserName = model.Email,
                Email = model.Email,
                Nombre = model.Nombre,
                Url = model.Url,
                CodigoPais = model.CodigoPais,
                PhoneNumber = model.PhoneNumber,
                Pais = model.Pais,
                Ciudad = model.Ciudad,
                Direccion = model.Direccion,
                FechaNacimiento = model.FechaNacimiento,
                Estado = model.Estado
            };

            var resultado = await userManager.CreateAsync(usuario, model.Password);

            if (resultado.Succeeded)
            {
                // Si quieres iniciar sesión automáticamente, habilita esto:
                // await signInManager.SignInAsync(usuario, false);
                return RedirectToAction("Login");
            }

            foreach (var error in resultado.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            AppUsuarios usuario = new()
            {
                UserName = model.Email,
                Email = model.Email,
            };

            var resultado = await signInManager.PasswordSignInAsync(
                model.Email,
                model.Password,
                model.RememberMe,
                lockoutOnFailure: false
                );

            if (resultado.Succeeded)
                return RedirectToAction("Index", "Home");

            ModelState.AddModelError(string.Empty, "Credenciales de acceso incorrectas.");
            return View(model);
        }
    }
}
