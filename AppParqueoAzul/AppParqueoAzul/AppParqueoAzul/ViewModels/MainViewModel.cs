using GalaSoft.MvvmLight.Command;
using AppParqueoAzul.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms.Maps;
using System.ComponentModel;
using AppParqueoAzul.Models;
using Xamarin.Forms;
using System.Diagnostics;
using AppParqueoAzul.Classes;

namespace AppParqueoAzul.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        #region Singleton

        static MainViewModel instance;

        public static MainViewModel GetInstance()
        {
            if (instance == null)
            {
                instance = new MainViewModel();
            }

            return instance;
        }



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

        #endregion

        #region Propeties
        public ObservableCollection<MenuItemViewModel> Menu { get; set; }

        public UsuarioViewModel UsuarioActual { get; set; }

        public LoginViewModel NewLogin { get; set; }

        public SaldoViewModel SaldoDisponibre { get; set; }

        public ObservableCollection<Pin> Pins { get; set; }

        public UsuarioViewModel UsuarioRegister { get; set; }

        public Double tiempoRestante { get; set; }
        public Double tiempoComprado { get; set; }

        public ObservableCollection<TarjetaCreditoViewModel> TarjetasCreditos { get; set; }

        public ObservableCollection<CarrosViewModel> Carros { get; set; }

        
        public NuevoCarroViewModel NuevoCarro { get; set; }
        public BuscarTarjetaPrepagoViewModel BuscarSaldo { get; set; }
        public NuevaTarjetaCreditoViewModel NuevaTarjetaCredito { get; set; }
        public NuevoParqueoViewModel NuevoParqueo { get; set; }
        public PlazaViewModel PlazaVM { get; set; }

        public bool IsRefreshing
        {
            set
            {
                if (isRefreshing != value)
                {
                    isRefreshing = value;

                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsRefreshing"));
                }
            }
            get
            {
                return isRefreshing;
            }
        }

        #endregion

        #region Attributes
        private bool isRefreshing = false;
        private NavigationService navigationService;
        private DataService dataService;
   

        #endregion

        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Services

        public ApiService apiService;

        #endregion

        #region Constructors
        public  MainViewModel()
        {
            instance = this;
            UsuarioActual = new UsuarioViewModel();
            
            Pins = new ObservableCollection<Pin>();
            apiService = new ApiService();
            BuscarSaldo = new BuscarTarjetaPrepagoViewModel();
            PlazaVM = new PlazaViewModel();
            //NuevoCarro = new NuevoCarroViewModel(0);
            //// var lista= apiService.Get<TarjetaViewModel>("TarjetaCreditoes");
            //NuevaTarjetaCredito = new NuevaTarjetaCreditoViewModel();
            Menu = new ObservableCollection<MenuItemViewModel>();
            TarjetasCreditos = new ObservableCollection<TarjetaCreditoViewModel>();
            Carros = new ObservableCollection<CarrosViewModel>();
            dataService = new DataService();
            navigationService = new NavigationService();
            NewLogin = new LoginViewModel();
            SaldoDisponibre = new SaldoViewModel
            {
                Saldo1 = 123,
            };
            
        }

       


        private void LoadSaldo()
        {
            var response = apiService.ConsultarSaldo(UsuarioActual.UsuarioId).Result;

            Saldo saldo =(Saldo)response.Result;
            SaldoDisponibre = new SaldoViewModel
            {
                Saldo1=saldo.Saldo1,
                SaldoId=saldo.SaldoId,
                Usuario=saldo.Usuario,
                UsuarioId=saldo.UsuarioId,
            } ;
        }

        private void LoadTarjetas()
        {
           
        }


        public void GetGeolotation()
        {
            var position1 = new Position(6.2652880, -75.5098530);
            var pin1 = new Pin
            {
                Type = PinType.Place,
                Position = position1,
                Label = "Pin1",
                Address = "prueba pin1"
            };
            Pins.Add(pin1);

            var position2 = new Position(6.2652880, -75.4598530);
            var pin2 = new Pin
            {
                Type = PinType.Place,
                Position = position2,
                Label = "Pin2",
                Address = "prueba pin2"
            };
            Pins.Add(pin2);

            var position3 = new Position(6.2652880, -75.4898530);
            var pin3 = new Pin
            {
                Type = PinType.Place,
                Position = position3,
                Label = "Pin3",
                Address = "prueba pin3"
            };
            Pins.Add(pin3);
        }

        private async void LoadCarros()
        {
            IsRunning = true;
            var carros = await apiService.GetCarros(navigationService.GetUsuarioActual().UsuarioId.ToString());

            Carros.Clear();

            foreach (var carro in carros)
            {
                Carros.Add(new CarrosViewModel
                {
                    CarroId = carro.CarroId,
                    Color = carro.Color,
                    Modelo = carro.Modelo,
                    ModeloId = carro.ModeloId,
                    NombreMarca = carro.Modelo.Marca.Nombre,
                    NombreModelo = carro.Modelo.Nombre,
                    FullName = string.Format("{0} {1} {2}",carro.Placa,carro.Modelo.Marca.Nombre,carro.Modelo.Nombre),
                    Placa=carro.Placa,
                    Usuario=carro.Usuario,
                    UsuarioId=carro.UsuarioId,
                }
                );
            }

            IsRunning = false;
        }
        #endregion

        #region Commands


        public ICommand ParquearCommand { get { return new RelayCommand(Parquear); } }

        public async void Parquear()
        {

            await navigationService.Navigate("ParquearPage");

        }

        public ICommand CarrosCommand { get { return new RelayCommand(CarrosMenu); } }

        public async void CarrosMenu()
        {

            await navigationService.Navigate("CarrosPage");

        }

        public ICommand PrepagoCommand { get { return new RelayCommand(Prepago); } }

        public async void Prepago()
        {

            await navigationService.Navigate("ComprarSaldoPage");

        }

        public ICommand RefreshCarrosCommand { get { return new RelayCommand(RefreshCarros); } }

        public void RefreshCarros()
        {

            LoadCarros();
            
        }

        public ICommand RefreshTarjetasCommand { get { return new RelayCommand(RefreshTarjetas); } }

        public async void RefreshTarjetas()
        {
           await LoadTarjetasCreditosAsync();
            
            IsRefreshing = false;
        }

        private async Task LoadTarjetasCreditosAsync()
        {
            IsRunning = true;
            var tarjetaCreditos = await apiService.GetTarjetaCredito(navigationService.GetUsuarioActual().UsuarioId.ToString());

            TarjetasCreditos.Clear();

            foreach (var tarjeta in tarjetaCreditos)
            {
                TarjetasCreditos.Add(new TarjetaCreditoViewModel
                {
                    Apellido = tarjeta.Apellido,
                    CVV_CVC = tarjeta.CVV_CVC,
                    Nombre = tarjeta.Nombre,
                    Numero = tarjeta.Numero,
                    Parqueo=tarjeta.Parqueo,
                    TarjetaCreditoId=tarjeta.TarjetaCreditoId,
                    Usuario=tarjeta.Usuario,
                    UsuarioId=tarjeta.UsuarioId,
                }
                
                );
            }
            IsRunning = false;
        }

        public ICommand NuevaTarjetaCommand { get { return new RelayCommand(NevaTarjetaAsync); } }

        private async void NevaTarjetaAsync()
        {
            await navigationService.Navigate("NuevaTarjetaPage");
        }

        public ICommand NevoPrepagoCommand { get { return new RelayCommand(NevoPrepagoAsync); } }

        private async void NevoPrepagoAsync()
        {
            await navigationService.Navigate("");
        }

        public ICommand NuevoParqueoCommand { get { return new RelayCommand(NuevoParqueoP); } }

        private async void NuevoParqueoP()
        {
            await navigationService.Navigate("NuevoParqueoPage");
        }

        public ICommand NuevoCarroCommand { get { return new RelayCommand(NuevoCarroP); } }

        private async void NuevoCarroP()
        {
            await navigationService.Navigate("NuevoCarroPage");
        }

        public ICommand VerificaPlazasCommand { get { return new RelayCommand(VerificarPlazas); } }

        private async void VerificarPlazas()
        {
            await navigationService.Navigate("PlazaPage");
        }
        #endregion

        #region Methods
        public void LoadMenu()
        {
            Menu.Clear();
            Menu.Add(new MenuItemViewModel
            {
                PageName = "ParquearPage",
                Icon= "ic_Comprar.png",
                Title = "Compra de tiempo ahora",
                SubTitle = "",

            });

            Menu.Add(new MenuItemViewModel
            {
                PageName = "CarrosPage",
                Icon= "ic_Carro.png",
                Title = "Administración de Vehículos ",
                SubTitle = Carros.Count.ToString(),
            });

            Menu.Add(new MenuItemViewModel
            {

                PageName = "ComprarSaldoPage",
                Icon= "ic_Recarga_Prepago.png",
                Title = "Recarga de tarjeta prepago",
                SubTitle = string.Format("Saldo disponible:"),
            });

            Menu.Add(new MenuItemViewModel
            {

                PageName = "PromocionesPage",
                Icon= "ic_Historial_Compras.png",
                Title = "Historial de compras",
                SubTitle = "",
            });

            Menu.Add(new MenuItemViewModel
            {

                PageName = "TarjetasPage",
                Icon= "ic_Tarjeta_Credito.png",
                Title = "Administraciòn tarjetas de crédito",
                SubTitle = string.Format("Tarjetas registradas"),
            });

        }

        public async void LoadTiempo()
        {
            var tiempo = await apiService.ConsultarTiempo(new UsuarioRequest { UsuarioId = App.UsuarioActual.UsuarioId.ToString(), });

            if (tiempo.Restante > new TimeSpan(0))
            {
                Debug.WriteLine("si tiene tiempo disponible, {0}",tiempo.Restante);
                //  Xamarin.Forms.Device.StartTimer(TimeSpan.FromSeconds(.02), OnTimer);

                tiempoRestante = (tiempo.Restante.Hours + tiempo.Restante.Minutes / 100.0 + tiempo.Restante.Seconds / 10000.0) * (tiempo.Restante > TimeSpan.Zero ? 1 : -1);
                tiempoComprado = (tiempo.Comprado.Hours + tiempo.Comprado.Minutes / 100.0 + tiempo.Comprado.Seconds / 10000.0) * (tiempo.Comprado > TimeSpan.Zero ? 1 : -1);

                // tiempoRestante = Convert.ToDouble(tiempo.Restante);
                //   tiempoComprado = Convert.ToDouble(tiempo.Comprado);

            }
            else
            {
                Debug.WriteLine("no tiene tiempo disponible, {0}", tiempo.Restante);
            }

        }
        

    
        #endregion


    }
}
