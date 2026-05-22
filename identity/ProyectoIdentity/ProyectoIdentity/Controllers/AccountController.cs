using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
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
        private readonly IEmailSender emailSender;

        public AccountController(UserManager<AppUsuarios> userManager, SignInManager<AppUsuarios> signInManager, IEmailSender emailSender)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.emailSender = emailSender;
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

                // HAbilitar la confirmación de la cuenta generando el Token de confirmación
                var token = await userManager.GenerateEmailConfirmationTokenAsync(usuario);

                // Codificamos el Token para incluirlo en la URL
                var tokenCodificado = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));

                // Construímos la URL del enlace de confirmación:
                var urlConfirmacion = Url.Action(
                    "ConfirmarEmail",
                    "Account",
                    new { userId = usuario.Id, token = tokenCodificado },
                    protocol: Request.Scheme
                    );

                // Enviar correo de confirmación:
                var asunto = "Confirma tu cuenta en Render2Web® Identity";

                var mensajeHtml = $@"
                    <h2>Hola, {usuario.Nombre}</h2>
                    <p>¡Gracias por registrarte. Para activar tu cuenta, haz clic en el siguiente enlace:</p>
                    <a href=""{urlConfirmacion}"" target=""_blank"" style='color: #2563eb; font-weight: bold;'>Confirmar mi cuenta</a>
                    <br />
                    <p>Si no solicitaste tu registro, puedes ignorar este mensaje.</p>
                    <hr />
                    <p>Render2Web® - Seguridad de cuenta</p>
                ";

                // Envío del correo electrónico:
                await emailSender.SendEmailAsync(usuario.Email, asunto, mensajeHtml);

                // Mostrar una vista informando al usuario que revise su email:
                return RedirectToAction("RegistroConfirmacion");
            }

            foreach (var error in resultado.Errors)
                ModelState.AddModelError(string.Empty, error.Description);

            return View(model);
        }

        [HttpGet]
        public IActionResult RegistroConfirmacion() => View();

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

            // 3.1.- Crear un mensaje HTML:
            var mensajeHtml = $@"
                <h2>Recuperación de contraseña</h2>
                <p>Hola, has solicitado restablecer tu contraseña. Haz clic en el siguiente enlace para crear una nueva contraseña:</p>
                <a href=""{url}"" target=""_blank"">Restablecer contraseña</a>
                <br />
                <p>Si no solicitaste este cambio, puedes ignorar este mensaje.</p>
                <hr />
                <p>Render2Web® - Seguridad de cuenta</p>
                ";

            // 3.2.- Envío del correo electrónico:
            await emailSender.SendEmailAsync(model.Email, "Recuperación de contraseña", mensajeHtml);

            // 4.- Guardamos la URL en TempData para mostrarla en la confirmación (solo para demostración, no se recomienda en producción):
            //TempData["UrlRecuperacion"] = urlConfirmacion;

            // Aquí NO enviamos el correo todavía, solo redirigimos a la confirmación.
            return RedirectToAction("OlvidoContrasenaConfirmacion");
        }

        [HttpGet]
        public IActionResult OlvidoContrasenaConfirmacion() => View();

        // Método que permite cambiar la contraseña. Recibe el email y el token como parámetros en la URL.
        [HttpGet]
        public IActionResult ResetPassword(string email, string token)
        {
            if (email is null || token is null)
                return RedirectToAction("OlvidoContrasenaConfirmacion");

            ResetPasswordViewModel model = new()
            {
                Email = email,
                Token = token
            };

            return View(model);
        }

        // Procesamiento olvidó contraseña.
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var usuario = await userManager.FindByEmailAsync(model.Email);

            if (usuario is null)
                return RedirectToAction("ResetPasswordConfirmacion");

            // El Token necesita ser decodificado:
            var tokenDecodificado = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(model.Token));

            var resultado = await userManager.ResetPasswordAsync(usuario, tokenDecodificado, model.Password);

            if (!resultado.Succeeded)
            {
                foreach (var error in resultado.Errors)
                    ModelState.AddModelError(string.Empty, error.Description);

                return View(model);
            }

            return RedirectToAction("ResetPasswordConfirmacion");
        }

        [HttpGet]
        public IActionResult ResetPasswordConfirmacion() => View();

        [HttpGet]
        public async Task<IActionResult> ConfirmarEmail(string userId, string token)
        {
            if (userId is null || token is null)
                return RedirectToAction("Error", "Home");

            var usuario = await userManager.FindByIdAsync(userId);

            if (usuario is null)
                return NotFound("No se encontró el usuario");

            var tokenDecodificado = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(token));

            var resultado = await userManager.ConfirmEmailAsync(usuario, tokenDecodificado);

            if (resultado.Succeeded)
                return View("ConfirmarEmail");

            return View("ErrorConfirmarEmail");
        }
    }
}
