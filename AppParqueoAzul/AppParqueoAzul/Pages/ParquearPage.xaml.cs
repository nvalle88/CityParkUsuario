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
using AppParqueoAzul.Services;

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

            var location = await locator.GetPositionAsync();
            var position = new Position(location.Latitude, location.Longitude);
            MyMap.MoveToRegion(MapSpan.FromCenterAndRadius(position, Distance.FromMiles(.3)));
            Location = location;
        }

        private void CircleButton_Clicked(object sender, EventArgs e)
        {
            int minutos=0;
            
            CircleButton myobject = sender as CircleButton;
            Button30m.BackgroundColor = Color.White;
            Button1h.BackgroundColor = Color.White;
            Button1h30m.BackgroundColor = Color.White;
            Button2h.BackgroundColor = Color.White;
            Button30m.TextColor = Color.Black;
            Button1h.TextColor = Color.Black;
            Button1h30m.TextColor = Color.Black;
            Button2h.TextColor = Color.Black;
            if (myobject.Text=="30m")
            {
                Button30m.BackgroundColor = Color.Blue;
                Button30m.TextColor = Color.White;
            }
            if (myobject.Text == "1h")
            {
                Button1h.BackgroundColor = Color.Blue;
                Button1h.TextColor = Color.White;
            }
            if (myobject.Text == "1h:30m")
            {              
                Button1h30m.BackgroundColor = Color.Blue;
                Button1h30m.TextColor = Color.White;
            }
            if (myobject.Text == "2h")
            {                
                Button2h.BackgroundColor = Color.Blue;
                Button2h.TextColor = Color.White;
            }

            
           //  parqueo.UpdateCantidadMinutos(sender, minutos);


        }

      

        //private void Button_Clicked(object sender, EventArgs e)
        //{
        //   parqueo.SalvarParqueo();
        //}
    }
}