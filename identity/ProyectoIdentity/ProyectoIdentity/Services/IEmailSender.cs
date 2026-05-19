namespace ProyectoIdentity.Services
{
    public interface IEmailSender
    {
        Task EnviarEmailAsync(string emailDestino, string asunto, string mensajeHtml);

        // Micros
    }
}
