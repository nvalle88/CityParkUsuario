using AppParqueoAzul.Classes;
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
   public class BuscarTarjetaPrepagoViewModel:BuscarTarjetaPrepago,INotifyPropertyChanged
    {
        #region Atributos

        private NavigationService navigationService;
        private DialogService dialogService;
        private ApiService apiService;

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

        #endregion

        #region Constructors

        public BuscarTarjetaPrepagoViewModel()
        {
            dialogService = new DialogService();
            apiService = new ApiService();
            navigationService = new NavigationService();
        }

        #endregion

        #region Commandos

        public ICommand BuscarSaldoCommand { get { return new RelayCommand(BuscarSaldo); } }

        private async void BuscarSaldo()
        {
            IsRunning = true;
            var usuario = navigationService.GetUsuarioActual();
            var response = await apiService.BuscarSaldo(Codigo, usuario.UsuarioId.ToString());

            if (response.IsSuccess)
            {
                var tarjetaPrepago = (Saldo)response.Result;
                await dialogService.ShowMessage("Se ha realizado la recarga de forma satisfactoria", string.Format("Código de tarjeta prepago: {0}, realizada por: {1}, Saldo actual:{2}",Codigo,usuario.Nombre,tarjetaPrepago.Saldo1));
                navigationService.SetMainPage(usuario);
                IsRunning = false;
                return;

            }
            IsRunning = false;
            await dialogService.ShowMessage("Error", response.Message);
        }

        
        #endregion
    }
}
