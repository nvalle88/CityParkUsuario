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
using ButtonCircle.FormsPlugin.Abstractions;

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

        //private void OnCantHorasChanged(object sender, ValueChangedEventArgs e)
        //{

        //    parqueo.UpdateCantidadHoras(sender, (int)e.NewValue);
        //}


        private async void Locator()
        {
            var locator = CrossGeolocator.Current;
            locator.DesiredAccuracy = 50;

            var location = await locator.GetPositionAsync(timeoutMilliseconds: 7000);
            var position = new Position(location.Latitude, location.Longitude);
            MyMap.MoveToRegion(MapSpan.FromCenterAndRadius(position, Distance.FromMiles(.3)));
            Location = location;
        }

        private void CircleButton_Clicked(object sender, EventArgs e)
        {
            int minutos=0;
            
            CircleButton myobject = sender as CircleButton;
            if (myobject.Text=="30m")
            {
                Button30m.BackgroundColor = Color.Blue;
                Button1h.BackgroundColor = Color.White;
                Button1h30m.BackgroundColor = Color.White;
                Button2h.BackgroundColor = Color.White;
                minutos = 30;
            }
            if (myobject.Text == "1h")
            {
                Button30m.BackgroundColor = Color.White;
                Button1h.BackgroundColor = Color.Blue;
                Button1h30m.BackgroundColor = Color.White;
                Button2h.BackgroundColor = Color.White;
                minutos = 30*2;
            }
            if (myobject.Text == "1h:30m")
            {
                Button30m.BackgroundColor = Color.White;
                Button1h.BackgroundColor = Color.White;
                Button1h30m.BackgroundColor = Color.Blue;
                Button2h.BackgroundColor = Color.White;
                minutos = 30 * 3;
            }
            if (myobject.Text == "2h")
            {
                Button30m.BackgroundColor = Color.White;
                Button1h.BackgroundColor = Color.White;
                Button1h30m.BackgroundColor = Color.White;
                Button2h.BackgroundColor = Color.Blue;
                minutos = 30 * 4;

            }
             parqueo.UpdateCantidadMinutos(sender, minutos);
            

        }

        private void Button_Clicked(object sender, EventArgs e)
        {

            parqueo.SalvarParqueo();

        }
    }
}