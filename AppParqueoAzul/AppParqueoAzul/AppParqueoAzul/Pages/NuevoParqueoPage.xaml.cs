using AppParqueoAzul.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppParqueoAzul.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NuevoParqueoPage : ContentPage
    {
       
        public NuevoParqueoPage()
        {
            InitializeComponent();
            
        }

        private void OnCantHorasChanged(object sender, ValueChangedEventArgs e)
        {
            //var instance = ParquearViewModel.GetInstance();
            //instance.UpdateCantHoras(sender, (int)e.NewValue);    
        }

    }
}