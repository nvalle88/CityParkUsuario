using AppParqueoAzul.Pages;
using AppParqueoAzul.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppParqueoAzul.Services
{
   public class NavigationService
    {
        public void  NavigateBack()
        {
             App.Navigator.PopAsync();
        }

        public async Task Navigate(string pageName)
        {
            App.Master.IsPresented = false;
            
            switch (pageName)
            {
               

                case "CarrosPage":
                    await App.Navigator.PushAsync(new CarrosPage());
                    break;

                case "ParquearPage":
                    var main = MainViewModel.GetInstance();
                    main.NuevoParqueo = new NuevoParqueoViewModel();
                  //  main.NuevoParqueo.CantidadMinutos =;
                    await App.Navigator.PushAsync(new ParquearPage());
                   
                    
                    break;

                case "MetodoPago":
                    await App.Navigator.PushAsync(new MetodoPago());
                    break;

                case "TarjetasPage":
                    await App.Navigator.PushAsync(new TarjetasCreditosPage());
                    break;

                case "PrepagoPage":
                    await App.Navigator.PushAsync(new MetodoPago());
                    break;

                case "NuevaTarjetaPage":
                    var mainTarjetaCredito = MainViewModel.GetInstance();
                    mainTarjetaCredito.NuevaTarjetaCredito = new NuevaTarjetaCreditoViewModel();
                    await App.Navigator.PushAsync(new NuevaTarjetaCreditoPage());
                    break;

                case "PlazaPage":
                    await App.Navigator.PushAsync(new PlazaPage());
                    break;


                //case "NuevoParqueoPage":
                //    await App.Navigator.PushAsync(new NuevoParqueoPage());
                //    break;

                case "NuevoCarroPage":
                    var mainCarro = MainViewModel.GetInstance();
                    mainCarro.NuevoCarro = new NuevoCarroViewModel();
                    await App.Navigator.PushAsync(new NuevoCarroPage());
                    break;

                case "ComprarSaldoPage":
                    await App.Navigator.PushAsync(new ComprarSaldoPage());
                    break;

                    
                case "PromocionesPage":
                    await App.Navigator.PushAsync(new PromocionesPage());
                    break;

                case "MainPage":
                    await App.Navigator.PopToRootAsync();
                    break;


                default: break;
            }
        }

        internal void SetMainPage(UsuarioViewModel usuarioActual)
        {
            App.UsuarioActual = usuarioActual;
            App.Current.MainPage = new MasterPage();
        }

        public UsuarioViewModel  GetUsuarioActual()
        {
            return App.UsuarioActual;
        }
    }
}
