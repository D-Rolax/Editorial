using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiEditorial.Models
{
    public class Cabecera
    {
        public int IdCabecera { get; set; }
        public string Cliente { get; set; }
        public List<Detalle> Detalle{ get; set; }
    }
    public class Detalle
    {
        public int IdDetalle { get; set; }
        public int IdCabecera { get; set; }
        public string Producto { get; set; }
    }
}
