using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppParqueoAzul.Models
{
   public class TarjetaPrepago
    {
        public int TarjetaPrepagoId { get; set; }
        public string Numero { get; set; }
        public decimal Saldo { get; set; }
        public DateTime FechaVence { get; set; }
        public bool Activa { get; set; }

        
        public virtual List<UsuarioTarjetaPrepago> UsuarioTarjetaPrepago { get; set; }
    }
}
