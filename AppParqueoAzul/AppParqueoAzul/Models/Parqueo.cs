using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppParqueoAzul.Models
{
   public class Parqueo
    {
        public int ParqueoId { get; set; }
        public int? UsuarioId { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public double? Latitud { get; set; }
        public double? Longitud { get; set; }
        public int? TarjetaCreditoId { get; set; }
        public int? CarroId { get; set; }
        public int? PlazaId { get; set; }

        public virtual Carro Carro { get; set; }
        public virtual TarjetaCredito TarjetaCredito { get; set; }
        public virtual Usuario Usuario { get; set; }

    }
}
