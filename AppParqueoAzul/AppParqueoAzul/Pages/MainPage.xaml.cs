using AppParqueoAzul.Classes;
using AppParqueoAzul.Models;
using AppParqueoAzul.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;


namespace AppParqueoAzul.Pages
{
    public partial class MainPage : ContentPage
    {
        ApiService apiService = new ApiService();
        Double tr = 0;        
        Double tiempoLabel = 0;
        bool TieneTiempo = false;
        public MainPage()
        {
            InitializeComponent();           
            LoadTime();
            Device.StartTimer(TimeSpan.FromMinutes(2),OnTimer);            
        }

        private async void  LoadTime()
        {
            var tiempo = await apiService.ConsultarTiempo(new UsuarioRequest { UsuarioId = App.UsuarioActual.UsuarioId.ToString(), });

            if (tiempo.Restante > new TimeSpan(0) && tiempo != null)
            {
                ProgressBarTime.IsVisible = true;
                ImagenCity.IsVisible = false;
                TieneTiempo = true;
                Debug.WriteLine("si tiene tiempo disponible, {0}", tiempo.Restante);
                Double tiempoRestante = (tiempo.Restante.Hours + tiempo.Restante.Minutes / 100.0 + tiempo.Restante.Seconds / 10000.0) * (tiempo.Restante > TimeSpan.Zero ? 1 : -1);
                Double tiempoComprado = (tiempo.Comprado.Hours + tiempo.Comprado.Minutes / 100.0 + tiempo.Comprado.Seconds / 10000.0) * (tiempo.Comprado > TimeSpan.Zero ? 1 : -1);
                tr = ((100 / tiempoComprado) * tiempoRestante) / 100;
                Debug.WriteLine(tr);
                tiempoLabel = Convert.ToInt32(tiempoRestante * 100);
                ProgressTime.Progress = 1 - tr;
                LabelRestante.Text = tiempoLabel + "  min restante";
            }
            else
            {

                ProgressBarTime.IsVisible = false;
                ImagenCity.IsVisible = true;
                TieneTiempo = false;

                Debug.WriteLine("no tiene tiempo disponible, {0}", tiempo.Restante);
            }

           
        }

        private bool OnTimer()
        {
            LoadTime();
            return TieneTiempo;
        }





    }
}
