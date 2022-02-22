using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiEditorial.Models.Caja
{
    public class Amortizacion
    {
        public int IdAmortizacion { get; set; }
        public int IdPersonal { get; set; }
        public int NumAmortizacion { get; set; }
        public int MontoTotal { get; set; }
        public DateTime Fecha { get; set; }
        public DateTime FechaRegistro { get; set; }
        public bool Estado { get; set; }
        public List<DetalleAmortizacion> DetalleAmortizacion{ get; set; }

    }
    public class DetalleAmortizacion
    {
        public int IdAmortizacion { get; set; }
        public int NumContrato { get; set; }
        public int NumRecibo { get; set; }
        public decimal Monto { get; set; }
    }
}
