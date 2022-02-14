using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiEditorial.Models
{
    public class Libros
    {
        public int IdLibros { get; set; }
        public string Nombre { get; set; }
        public string Nivel { get; set; }
        public decimal PrecioU { get; set; }
        public decimal PrecioM { get; set; }
        public decimal PrecioR { get; set; }
        public string Descripcion { get; set; }
        public int TotalAlmacen { get; set; }
        public DateTime FechaRegistro { get; set; }
        public bool Estado { get; set; }

    }
    public class Almacen
    {
        public int IdAlmacen { get; set; }
        public string Nombre { get; set; }
    }
}
