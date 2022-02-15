using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiEditorial.Models
{
    public class ListaDetallePedido
    {
        public int IdPedDev { get; set; }
        public int IdPersonal { get; set; }
        public string NombreCompleto { get; set; }
        public int NumPedido { get; set; }
        public DateTime FechaRegistro { get; set; }
        public string Estado { get; set; }
        public string Nombre { get; set; }
        public int Cantidad { get; set; }
        public string Criterio { get; set; }
        public string Atributo { get; set; }
    }
}
