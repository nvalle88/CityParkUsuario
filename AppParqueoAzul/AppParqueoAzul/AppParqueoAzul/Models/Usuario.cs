using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppParqueoAzul.Models
{
   public class Usuario
    {
        public int UsuarioId { get; set; }
        public string Nombre { get; set; }
        public string Contrasena { get; set; }

        
        public virtual List<Carro> Carro { get; set; }

        public virtual List<Parqueo> Parqueo { get; set; }
        
        public virtual List<Saldo> Saldo { get; set; }
        
        public virtual List<TarjetaCredito> TarjetaCredito { get; set; }
        
        public virtual List<UsuarioTarjetaPrepago> UsuarioTarjetaPrepago { get; set; }
    }
}
