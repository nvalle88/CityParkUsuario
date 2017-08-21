using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppParqueoAzul.Models
{
    public class Saldo
    {
        public int SaldoId { get; set; }
        public decimal? Saldo1 { get; set; }
        public int UsuarioId { get; set; }

        public virtual Usuario Usuario { get; set; }

    }
}
