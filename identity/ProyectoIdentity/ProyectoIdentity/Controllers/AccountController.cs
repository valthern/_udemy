using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.IdentityModel.Abstractions;
using ProyectoIdentity.Models;
using System.Text;

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
        public IActionResult Login(string? returnUrl = null)
        {
            //ViewData["ReturnUrl"] = returnUrl;
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl = null)
        {
            ViewBag.ReturnUrl = returnUrl;

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
            {
                // Redirigir sólo si es una URL local válida para evitar ataques de redirección abierta.
                if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                    return Redirect(returnUrl);

                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError(string.Empty, "Credenciales de acceso incorrectas.");
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Logout(string? returnUrl = null)
        {
            await signInManager.SignOutAsync();

            // Redirigir sólo si es una URL local válida para evitar ataques de redirección abierta.
            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                return Redirect(returnUrl);

            return RedirectToAction("Login", "Account");
        }

        // Acceso denegado.
        [HttpGet]
        public IActionResult AccessDenied() => View();

        // Olvidó contraseña.
        [HttpGet]
        public IActionResult OlvidoContrasena() => View();

        // Procesamiento olvidó contraseña.
        [HttpPost]
        public async Task<IActionResult> OlvidoContrasena(OlvidoContrasenaViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var usuario = await userManager.FindByEmailAsync(model.Email);

            // No se debe revelar si el usuario existe o no, ni si el correo electrónico está confirmado, para evitar ataques de enumeración de usuarios.
            //if (usuario is null || !await userManager.IsEmailConfirmedAsync(usuario))
            //    return RedirectToAction("OlvidoContrasenaConfirmacion");
            if (usuario is null)
                return RedirectToAction("OlvidoContrasenaConfirmacion");

            // Ejemplo de generación de token de recuperación de contraseña:
            // https://localhost:5001/Account/ResetPassword?email=correo@dominio.com&token=AQUIVAELTOKENQUESEGENERARÁ...
            // 1.- Generar el token de recuperación:
            var token = await userManager.GeneratePasswordResetTokenAsync(usuario);

            // 2.- Codificar el token para que sea seguro en la URL:
            var tokenCodificado = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));

            // 3.- Construir la URL segura:
            var url = Url.Action(
                "ResetPassword",
                "Account",
                new { email = model.Email, token = tokenCodificado },
                protocol: Request.Scheme
                );

            // 4.- Guardamos la URL en TempData para mostrarla en la confirmación (solo para demostración, no se recomienda en producción):
            TempData["UrlRecuperacion"] = url;

            // Aquí NO enviamos el correo todavía, solo redirigimos a la confirmación.
            return RedirectToAction("OlvidoContrasenaConfirmacion");
        }

        [HttpGet]
        public IActionResult OlvidoContrasenaConfirmacion() => View();
    }
}
