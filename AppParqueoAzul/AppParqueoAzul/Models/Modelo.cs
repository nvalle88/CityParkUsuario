using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppParqueoAzul.Models
{
  public  class Modelo
    {
        public int ModeloId { get; set; }
        public string Nombre { get; set; }
        public int MarcaId { get; set; }
        public int PlazaId { get; set; }


        public virtual List<Carro> Carro { get; set; }
        public virtual Marca Marca { get; set; }

    }
}
