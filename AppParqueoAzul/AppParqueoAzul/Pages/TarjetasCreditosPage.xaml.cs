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
    public partial class TarjetasCreditosPage : ContentPage
    {
        public TarjetasCreditosPage()
        {
            InitializeComponent();


            var main = (MainViewModel)this.BindingContext;
            base.Appearing += (object sender, EventArgs e) =>
            {
                main.RefreshTarjetasCommand.Execute(this);
            };
        }
    }
}