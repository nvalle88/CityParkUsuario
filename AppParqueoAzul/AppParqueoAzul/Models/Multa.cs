using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppParqueoAzul.Models
{
   public class Multa
    {
        public int MultaId { get; set; }
        public string Foto { get; set; }
       
        public decimal? Valor { get; set; }
        public DateTime Fecha { get; set; }
        public int? AgenteId { get; set; }
        public Nullable<double> Longitud { get; set; }
        public Nullable<double> latitud { get; set; }
        public string Placa { get; set; }

        public virtual Agente Agente { get; set; }
       

    }
}
