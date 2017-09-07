using AppParqueoAzul.Classes;
using AppParqueoAzul.Models;
using AppParqueoAzul.Services;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AppParqueoAzul.ViewModels
{
   public class NuevoCarroViewModel:Carro,INotifyPropertyChanged
    {
        private ApiService apiService;
        private NavigationService navigationService;
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

        public ObservableCollection<ModeloViewModel> Modelos { get; set; }
        public ObservableCollection<MarcaViewModel> Marcas { get; set; }


        public NuevoCarroViewModel()
        {
            IsRunning = true;
                Modelos = new ObservableCollection<ModeloViewModel>();
                Marcas = new ObservableCollection<MarcaViewModel>();
                apiService = new ApiService();
                navigationService = new NavigationService();
                dialogService = new DialogService();
               // LoadModelos();
            LoadMarcas();
        }

        private async void LoadModelos(MarcaRequest _marca)
        {
            
            var listaModelos = await apiService.GetModeloByMarca(_marca);

            Modelos.Clear();

            foreach (var modelo in listaModelos)
            {
                Modelos.Add(new ModeloViewModel
                {
                   ModeloId=modelo.ModeloId,
                   Nombre = modelo.Nombre,
                  
                });

            }
            IsRunning = false;
        }
        
        private async void LoadMarcas()
        {
            var listaMarcas = await apiService.GetMarcas();
            Marcas.Clear();
            foreach (var marca in listaMarcas)
            { if (marca.Nombre!=null)
                Marcas.Add(new MarcaViewModel
                {
                    MarcaId =marca.MarcaId,
                    Nombre=marca.Nombre,                               
                });

            }
            IsRunning = false;

        }
        

        MarcaRequest marcaseleccionada;
        public MarcaRequest MarcaSelectedItem
        {
            get
            {
                return marcaseleccionada;
            }
            set
            {
               // marcaseleccionada = ;
                LoadModelos(value);
                marcaseleccionada = value;
            }
        }

        public ICommand SalvarCarroCommand { get { return new RelayCommand(SalvarCarro); } }       

        private async void SalvarCarro()
        {
            IsRunning = true;
            var main = MainViewModel.GetInstance();
            var carro = new Carro
            {   ModeloId = ModeloId,
                Placa = Placa,
                UsuarioId = navigationService.GetUsuarioActual().UsuarioId,
                Color = Color,
            };
            var response = await apiService.NuevoCarro(carro);
            if (response.IsSuccess)
            {
                await dialogService.ShowMessage("OK", response.Message);
                navigationService.NavigateBack();
                main.LoadMenu();
                return;
            }

            await dialogService.ShowMessage("Error", "No");


        }
    }
}
