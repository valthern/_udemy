using System.Timers;
using Timer = System.Timers.Timer;

namespace BlazorPeliculas.Client.Auth
{
    public class RenovadorToken : IDisposable
    {
        private readonly ILoginService loginService;
        Timer? timer;

        public RenovadorToken(ILoginService loginService)
        {
            this.loginService = loginService;
        }

        public void Iniciar()
        {
            timer = new()
            {
                // Cuatro minutos
                Interval = 1000 * 60 * 4
                // Cinco Segundos
                //timer.Interval = 1000 * 5;
            };
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
        }

        private void Timer_Elapsed(object? sender, ElapsedEventArgs e)
        {
            loginService.ManejarRenovacionToken();
        }

        public void Dispose()
        {
            timer?.Dispose();
        }
    }
}
