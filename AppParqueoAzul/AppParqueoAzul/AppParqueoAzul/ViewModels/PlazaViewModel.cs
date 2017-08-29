using AppParqueoAzul.Models;
using AppParqueoAzul.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppParqueoAzul.ViewModels
{
   public class PlazaViewModel:Plaza//,INotifyPropertyChanged
    {
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
        public ObservableCollection<Barrios> Barrios { get; set; }
        public ObservableCollection<PlazaView> Plazas { get; set; }


        public PlazaViewModel()
        {
            Barrios = new ObservableCollection<Barrios>();
            Plazas = new ObservableCollection<PlazaView>();
            dialogService = new DialogService();
            apiService = new ApiService();
            navigationService = new NavigationService();
            LoadBarrios();
        }

        private async void LoadBarrios()
        {
            var listaBarrios = await apiService.GetBarrios();

            Barrios.Clear();

            foreach (var barrio in listaBarrios)
            {
                Barrios.Add(barrio);
            }
        }

        Barrios barrioSeleccionado;
        public Barrios BarrioSelectedItem
        {
            get
            {
                return barrioSeleccionado;
            }
            set
            {
                // marcaseleccionada = ;
                LoadPlazas(value);
                barrioSeleccionado = value;
            }
        }

        private async void LoadPlazas(Barrios _barrio)
        {

            var listaPlazas = await apiService.GetPlazaByBarrio(_barrio);

            Plazas.Clear();

            foreach (var plaza in listaPlazas)
            {
                string imagen = "verde.png";
                if(plaza.Ocupado==true)
                {
                    imagen = "rojo.png";
                }

                Plazas.Add(new PlazaView {
                    PlazaId=plaza.PlazaId,
                    Barrio=plaza.Barrio,
                    Direccion=plaza.Direccion,
                    Nombre=plaza.Nombre,
                    Imagen=imagen,                    
                });
            }
            IsRunning = false;
        }











    }
}
