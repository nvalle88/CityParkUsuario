using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppParqueoAzul.Pages;
using AppParqueoAzul.ViewModels;
using Xamarin.Forms;
using System.Diagnostics;

namespace AppParqueoAzul
{
    public partial class App : Application
    {
        public static MasterPage Master { get; internal set; }
        public static NavigationPage Navigator { get; internal set; }
        public static UsuarioViewModel UsuarioActual { get; internal set; }

        public App()
        {
            InitializeComponent();
            MainPage = new LoginPage();
         

        }

        protected override void OnStart()
        {
           
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
