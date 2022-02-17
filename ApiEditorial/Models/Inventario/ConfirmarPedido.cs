using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiEditorial.Models.Inventario
{
    public class ConfirmarPedido
    {
        public int IdPedido { get; set; }
        public string Estado { get; set; }

        public List<ConfirmarDetallePedido> ConfirmarDetallePedido { get; set; }
        public ConfirmarPedido()
        {
            this.ConfirmarDetallePedido = new List<ConfirmarDetallePedido>();
        }

        
    }
    public class ConfirmarDetallePedido
    {
        public int idDetallePedido { get; set; }
        public int idLibro { get; set; }
        public int cantidadRecibida { get; set; }
        public int idPersonal { get; set; }
    }
}
