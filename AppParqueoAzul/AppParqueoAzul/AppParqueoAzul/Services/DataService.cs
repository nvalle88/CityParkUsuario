using AppParqueoAzul.Classes;
using AppParqueoAzul.Data;
using AppParqueoAzul.Models;
using AppParqueoAzul.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppParqueoAzul.Services
{
   public class DataService
    {


        public Response InsertarUsuario(Usuario usuario)
        {
            try
            {
                using (var db = new DataAccess())
                {
                    db.Insert(usuario);
                }

                return new Response
                {
                    IsSuccess = true,
                    Message = "Usuario Registrado Ok",
                    Result=usuario,
                };
            }
            catch (Exception ex)
            {

                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message,
                };
            }

        }

        public List<CarrosViewModel> listaCarros()
        {
            List<CarrosViewModel> lista= new List<CarrosViewModel>();
            using (var db = new DataAccess())
            {
              var ListaCarros= db.GetList<Carro>(false);
                foreach (var carro in ListaCarros)
                {
                    var carroViewModel = new CarrosViewModel();
                    //{
                    //    CarroId=carro.CarroId,
                    //    Placa=carro.Placa,
                    //    Marca=carro.Marca,
                    //    Modelo=carro.Modelo,
                    //    Color=carro.Color,
                    //};
                    lista.Add(carroViewModel);
                }
                return lista;
            }

        }
    }
}
