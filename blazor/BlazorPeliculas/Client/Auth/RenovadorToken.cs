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
            var unSegundo = 1000;
            var segundosEnUnMinuto = 60;

            timer = new()
            {
                // Cuatro minutos
                Interval = unSegundo * segundosEnUnMinuto * 4
                // Cinco Segundos
                //Interval = unSegundo * 5
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
