using AppParqueoAzul.Models;
using AppParqueoAzul.Services;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AppParqueoAzul.ViewModels
{
   public class NuevaTarjetaCreditoViewModel:TarjetaCredito,INotifyPropertyChanged
    {


        private NavigationService navigationService;
        private ApiService apiService;
        private DialogService dialogService;


        public bool isRunning;

        public event PropertyChangedEventHandler PropertyChanged;

        public bool IsRunning
        {
            set
            {
                if (isRunning != value)
                {
                    isRunning = value;

                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsRunning"));
                }
            }
            get { return isRunning; }
        }

        public NuevaTarjetaCreditoViewModel()
        {
            apiService = new ApiService();
            navigationService = new NavigationService();
            dialogService = new DialogService();
        }



        public ICommand SalvarTarjetaCommand { get { return new RelayCommand(SalvarTarjeta); } }

        private async void SalvarTarjeta()
        {
            IsRunning = true;
            var main = MainViewModel.GetInstance();

            var nuevaTarjetaCredito = new TarjetaCredito
            {
                Apellido = Apellido,
                Nombre = Nombre,
                CVV_CVC=CVV_CVC,
                Numero=Numero,
                UsuarioId = navigationService.GetUsuarioActual().UsuarioId,
                
            };
            var response = await apiService.NuevaTarjetaCredito(nuevaTarjetaCredito);
            if (response.IsSuccess)
            {
                await dialogService.ShowMessage("OK", response.Message);
                navigationService.NavigateBack();
                main.LoadMenu();
                IsRunning = false;
                return;
            }

            IsRunning = false;
            await dialogService.ShowMessage("Error", "No");

        }
    }
}
