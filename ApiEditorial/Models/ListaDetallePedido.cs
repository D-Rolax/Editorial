using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiEditorial.Models
{
    public class ListaDetallePedido
    {
        public int idDetallePedido { get; set; }
        public int IdPedidos { get; set; }
        public int IdLibro { get; set; }
        public int Cantidad { get; set; }
        public int CantRecibida { get; set; }
        public string nombreDescripcion { get; set; }
    }
    public class ListarPedidos
    {
        public int IdPedidos { get; set; }
        public int IdPersonal { get; set; }
        public int NumPedido { get; set; }
        public int CantidadTotal { get; set; }
        public int TotalRecibidos { get; set; }
        public DateTime FechaRegistro { get; set; }
        public string Motivo { get; set; }
        public string Destino { get; set; }
        public string Estado { get; set; }
    }
}
