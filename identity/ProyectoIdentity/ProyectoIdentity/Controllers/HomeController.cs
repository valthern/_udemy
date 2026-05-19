using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using ProyectoIdentity.Models;
using System.Diagnostics;

namespace ProyectoIdentity.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEmailSender emailSender;

        public HomeController(IEmailSender emailSender)
        {
            this.emailSender = emailSender;
        }

        public IActionResult Index() => View();

        public IActionResult About() => View();

        [Authorize]
        public IActionResult Privacy() => View();

        public async Task<IActionResult> EnviarPrueba()
        {
            await emailSender.EnviarEmailAsync(
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
