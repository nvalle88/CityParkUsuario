using AppParqueoAzul.Models;
using AppParqueoAzul.Pages;
using AppParqueoAzul.Services;
using GalaSoft.MvvmLight.Command;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;


namespace AppParqueoAzul.ViewModels
{
   public class NuevoParqueoViewModel: Parqueo//,INotifyPropertyChanged
    {
        Position Location;
        int cantidadMinutos;

        private ApiService apiService;
        private NavigationService navigationService;
        private DialogService dialogService;

        public event PropertyChangedEventHandler PropertyChanged;


        public ObservableCollection<CarrosViewModel> Carros { get; set; }
        public ObservableCollection<Plaza> Plazas { get; set; }


        public bool isRunning;

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
      
        public NuevoParqueoViewModel()
        {
            IsRunning = true;
            Carros = new ObservableCollection<CarrosViewModel>();
            Plazas = new ObservableCollection<Plaza>();

            apiService = new ApiService();
            navigationService = new NavigationService();
            dialogService = new DialogService();
            Location = new Position();
            Location.Latitude = -1;
            Location.Longitude = -1;

            ObtenerPlazas();
            LoadCarros();
            IsRunning = false;

        }




        //public void UpdateCantidadMinutos(object sender, int value)
        //{
        //    CantidadMinutos = value;
        //}

        private async void Locator()
        {
            var locator = CrossGeolocator.Current;
            locator.DesiredAccuracy = 50;
             Location = await locator.GetPositionAsync();

        }

        private async void ObtenerPlazas()
        {
            var locator = CrossGeolocator.Current;
            locator.DesiredAccuracy = 50;
            var location = await locator.GetPositionAsync();

            Classes.Posicion posicion = new Classes.Posicion
            { latitude= location.Latitude,
                longitude =location.Longitude,
            };
            var _Plazas = await apiService.GetPlazaByPosition(posicion);

            Plazas.Clear();

            foreach (var plaza in _Plazas)
            {
                Plazas.Add(plaza);
            }


        }

        Plaza PlazaSeleccionada;
        public Plaza PlazaSelectedItem
        {
            get
            {
                return PlazaSeleccionada;
            }
            set
            {
                // marcaseleccionada = ;
                PlazaSeleccionada = value;
            }
        }




        private async void LoadCarros()
        {
            var listaCarros = await apiService.GetCarros(navigationService.GetUsuarioActual().UsuarioId.ToString());

            Carros.Clear();

            foreach (var carro in listaCarros)
            {
                Carros.Add(new CarrosViewModel
                {
                    FullName= string.Format("{0} {1} {2}",carro.Modelo.Marca.Nombre,carro.Modelo.Nombre,carro.Placa),
                    CarroId=carro.CarroId,
                    Placa=carro.Placa,
                    UsuarioId=carro.UsuarioId,
                    Color=carro.Color,
                    Modelo=carro.Modelo,
                    ModeloId=carro.ModeloId,
                    NombreMarca=carro.Modelo.Marca.Nombre,
                    NombreModelo=carro.Modelo.Nombre,
                    Usuario=carro.Usuario,  
                    
                });

            }
        }


       public ICommand SalvarParqueoCommand { get { return new RelayCommand(SalvarParqueo); } }
        private ICommand _updateminutosCommand;

        public ICommand UpdateminutosCommand
        {
            get
            {
                if (_updateminutosCommand == null)
                    _updateminutosCommand = new RelayCommand<string>(i => ActualizarMinutos(i));
                return _updateminutosCommand;
            }

        }

        public  void ActualizarMinutos(string i)
        {

            cantidadMinutos = 30* Int32.Parse(i);
            ;
            return;
        }

        public async void SalvarParqueo()
        {            
            IsRunning = true;
            //Debug.WriteLine(PlazaName);
            //Plaza plaza = new Plaza
            //{
            //    Nombre = PlazaName,
            //};
            //var Pza = await apiService.GetPlazaByNombre(plaza);
            
            var PP = ParquearPage.GetInstance();



            var parqueo = new Parqueo
            {

                FechaInicio = DateTime.Now.ToLocalTime(),
                FechaFin = DateTime.Now.AddMinutes(cantidadMinutos).ToLocalTime(),
                Latitud = PP.Location.Latitude,
                CarroId = CarroId,
                Longitud = PP.Location.Longitude,
                UsuarioId = navigationService.GetUsuarioActual().UsuarioId,
                PlazaId = PlazaSeleccionada.PlazaId,
            };
            PlazaSeleccionada.Ocupado = true;

            var response =await apiService.NuevaParqueo(parqueo);
            
            if (response.IsSuccess)
            {
                var newParqueo = (Parqueo)response.Result;
                await dialogService.ShowMessage(" El parqueo ha sido establecido satisfactoriamente."
                                                                , string.Format("Vehículo: {0}, con placa: {1}, Hora de Inicio: {2} Hora Final: {3} LATITUD{4}, LONGITUD{5}, Usuario {6}"
                                                                                , newParqueo.Carro.Modelo.Marca.Nombre, newParqueo.Carro.Placa, 
                                                                                newParqueo.FechaInicio.ToString(), newParqueo.FechaFin.ToString(), 
                                                                                newParqueo.Latitud, newParqueo.Longitud, navigationService.GetUsuarioActual().Nombre));
                await apiService.UpdatePlaza(PlazaSeleccionada);

                IsRunning = false;
                navigationService.SetMainPage(navigationService.GetUsuarioActual());
                return;
            }
            await dialogService.ShowMessage("El parqueo no ha sido establecido satisfactoriamente", response.Message);
            IsRunning = false;
            return;     
        }


        public void EntryCompleted()
        {
           
        }

        /// <summary>

        /// Gets the UnfocusedCommand.

        /// </summary>






    }
}
