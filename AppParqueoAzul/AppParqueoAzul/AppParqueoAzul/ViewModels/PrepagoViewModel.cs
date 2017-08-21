using AppParqueoAzul.Models;
using AppParqueoAzul.Services;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AppParqueoAzul.ViewModels
{
   public class SaldoViewModel:Saldo
    {
       

        private NavigationService navigationService;
        private DialogService dialogService;

        

        public SaldoViewModel()
        {
            
            navigationService = new NavigationService();
            dialogService = new DialogService();
        }


        public ICommand ComprarSaldoCommand { get { return new RelayCommand(ComprarSaldoAsync); } }

        private  void ComprarSaldoAsync()
        {
            


        }
    }


}
