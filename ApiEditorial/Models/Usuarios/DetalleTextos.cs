using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiEditorial.Models
{
    public class DetalleTextos
    {
        public int IdContrato { get; set; }
        public int IdLibros { get; set; }
        public int IdDevolucion { get; set; }
        public int Cantidad { get; set; }
        public decimal Precio { get; set; }
        public int LibroGuia{ get; set; }
    }
}
