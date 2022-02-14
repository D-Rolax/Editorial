using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiEditorial.Data.Enties
{
    public class LibroEntity
    {
        [Key]
        public int IdLibros { get; set; }
        public string Nombre { get; set; }
        public string Nivel { get; set; }
        public decimal PrecioU { get; set; }
        public decimal PrecioM { get; set; }
        public decimal PrecioR { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaRegistro { get; set; }
        public bool Estado { get; set; }
    }
}
