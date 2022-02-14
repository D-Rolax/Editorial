using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiEditorial.Models.Inventario
{
    public class DetallePedido
    {
        public int IdPedidos{ get; set; }
        public int IdExTextos{ get; set; }
        public int IdLibro { get; set; }
        public int Cantidad { get; set; }
    }
}
