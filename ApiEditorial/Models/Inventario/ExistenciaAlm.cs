using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiEditorial.Models.Inventario
{
    public class ExistenciaAlm
    {
        public int IdExistenciaAlm { get; set; }
        public int IdLibros { get; set; }
        public int StockInicial { get; set; }
        public int Recepcion { get; set; }
        public int Envios { get; set; }
        public int TotalAlmacen { get; set; }
        public DateTime FechaRegistro { get; set; }
        public bool Estado { get; set; }
    }
}
