using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiEditorial.Models.Inventario
{
    public class Almacen
    {
        public int IdAlmacen { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string Descripcion { get; set; }
        public bool Estado { get; set; }
        public DateTime FechaRegistro { get; set; }
        public int IdCLiente { get; set; }
    }
}
