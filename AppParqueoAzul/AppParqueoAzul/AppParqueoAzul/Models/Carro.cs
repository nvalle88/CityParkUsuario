using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppParqueoAzul.Models
{
   public class Carro
    {
        public int MarcaId { get; set; }
        public int CarroId { get; set; }
        public int ModeloId { get; set; }
        public int UsuarioId { get; set; }
        public string Placa { get; set; }
        public string Color { get; set; }

        public virtual Marca Marca { get; set; }
        public virtual Modelo Modelo { get; set; }
        public virtual Usuario Usuario { get; set; }
        public virtual List<Parqueo> Parqueo { get; set; }
    }
}
