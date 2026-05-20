using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using ProyectoIdentity.Models;
using System.Net.Mail;

namespace ProyectoIdentity.Services
{
    public class GmailEmailSender : IEmailSender
    {
        private readonly EmailSettings settings;

        public GmailEmailSender(IOptions<EmailSettings> settings)
        {
            this.settings = settings.Value;
        }

        public async Task SendEmailAsync(string emailDestino, string asunto, string mensajeHtml)
        {
            using var client = new SmtpClient();
            client.Host = settings.Host;
            client.Port = settings.Port;
            client.EnableSsl = settings.EnableSSL;
            client.Credentials = new System.Net.NetworkCredential(settings.UserName, settings.Password);

            var mensaje = new MailMessage
            {
                From = new MailAddress(settings.UserName, "Soporte Curso Identity - Render2Web®"),
                Subject = asunto,
                Body = mensajeHtml,
                IsBodyHtml = true
            };

            mensaje.To.Add(emailDestino);
            await client.SendMailAsync(mensaje);
        }
    }
}
