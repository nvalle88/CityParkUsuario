using AppParqueoAzul.ViewModels;
using Plugin.Geolocator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace AppParqueoAzul.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ParquearPage : ContentPage
    {
        private NuevoParqueoViewModel parqueo;

        #region Singleton

        static ParquearPage instance;
       public  Plugin.Geolocator.Abstractions.Position  Location;

        public static ParquearPage GetInstance()
        {
            if (instance == null)
            {
                instance = new ParquearPage();
            }

            return instance;
        }
        #endregion

        public ParquearPage()
        {
            InitializeComponent();
            parqueo = new NuevoParqueoViewModel();
            instance = this;
            Location = new Plugin.Geolocator.Abstractions.Position();

            Locator();
        }

        private void OnCantHorasChanged(object sender, ValueChangedEventArgs e)
        {
            parqueo.UpdateCantidadHoras(sender, (int)e.NewValue);
        }


        private async void Locator()
        {
            var locator = CrossGeolocator.Current;
            locator.DesiredAccuracy = 50;

            var location = await locator.GetPositionAsync(timeoutMilliseconds: 7000);
            var position = new Position(location.Latitude, location.Longitude);
            MyMap.MoveToRegion(MapSpan.FromCenterAndRadius(position, Distance.FromMiles(.3)));
            Location = location;
        }

    }
}