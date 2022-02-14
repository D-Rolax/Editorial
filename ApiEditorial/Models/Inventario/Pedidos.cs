using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiEditorial.Models.Inventario
{
    public class Pedidos
    {
        public int IdPersonal { get; set; }
        public int NumPedido { get; set; }
        public int CantidadTotal { get; set; }
        public string Motivo { get; set; }
        public string Destino { get; set; }
        public List<DetallePedido> detallePedidos { get; set; }
    }
}
