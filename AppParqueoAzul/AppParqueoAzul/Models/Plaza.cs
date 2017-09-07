using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppParqueoAzul.Models
{
    public class Plaza
    {
        public int PlazaId { get; set; }
        public string Nombre { get; set; }
        public string Barrio { get; set; }
        public string Direccion { get; set; }
        public Nullable<bool> Ocupado { get; set; }
    }
}
