using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiEditorial.Models.Caja
{
    public class Ventas
    {
        public int IdCliente { get; set; }
        public int IdUsuario { get; set; }
        public int NroRecibo { get; set; }
        public int Cantidad { get; set; }
        public int LibroGuia { get; set; }
        public int Total { get; set; }
        public string NombreAlumno { get; set; }
        public int IdComision { get; set; }
        public List<DetalleVentas>DetalleVentas { get; set; }
    }
    public class DetalleVentas
    {
        public int IdLibro { get; set; }
        public int Cantidad { get; set; }
        public decimal Precio { get; set; }
        public decimal SubTotal{ get; set; }
    }
}
