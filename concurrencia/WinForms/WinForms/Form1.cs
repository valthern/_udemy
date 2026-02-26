using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinForms
{
    public partial class Form1 : Form
    {
        private readonly string apiURL;
        private readonly HttpClient httpClient;

        public Form1()
        {
            InitializeComponent();
            apiURL = "https://localhost:44397";
            httpClient = new HttpClient();
        }

        private async void btnIniciar_Click(object sender, EventArgs e)
        {
            loadingGif.Visible = true;
            #region Número de las Tarjetas para procesar
            var numeroDeTarjetas = 50_000;
            #endregion

            var tarjetas = await ObtenerTarjetasDeCredito(numeroDeTarjetas);
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            try
            {
                await ProcesarTarjetas(tarjetas);
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show(ex.Message);
            }

            MessageBox.Show($"Operación finalizada en {stopwatch.ElapsedMilliseconds / 1000.0} segundos");
            loadingGif.Visible = false;
        }

        private async Task ProcesarTarjetas(List<string> tarjetas)
        {
            #region Tarjetas para procesar simultaneamente
            var tarjetasEnProceso = 5000;
            #endregion
            using var semaforo = new SemaphoreSlim(tarjetasEnProceso);

            var tareas = new List<Task<HttpResponseMessage>>();

            tareas = tarjetas.Select(async tarjeta =>
             {
                 var json = JsonConvert.SerializeObject(tarjeta);
                 var content = new StringContent(json, Encoding.UTF8, "application/json");
                 await semaforo.WaitAsync();
                 try
                 {
                     return await httpClient.PostAsync($"{apiURL}/tarjetas", content);
                 }
                 finally
                 {
                     semaforo.Release();
                 }
             }).ToList();

            await Task.WhenAll(tareas);
        }

        private async Task<List<string>> ObtenerTarjetasDeCredito(int cantidadDeTarjetas)
        {
            return await Task.Run(() =>
            {
                var tarjetas = new List<string>();

                for (int i = 0; i < cantidadDeTarjetas; i++)
                    tarjetas.Add(i.ToString().PadLeft(16, '0'));

                return tarjetas;
            });
        }

        private async Task Esperar() => await Task.Delay(TimeSpan.FromSeconds(0));

        private async Task<string> ObtenerSaludo(string nombre)
        {
            //using(var respuesta = await httpClient.GetAsync($"{apiURL}/saludos?nombre={nombre}"))
            using (var respuesta = await httpClient.GetAsync($"{apiURL}/saludos/{nombre}"))
            {
                respuesta.EnsureSuccessStatusCode();
                var saludo = await respuesta.Content.ReadAsStringAsync();
                return saludo;
            }
        }
    }
}
