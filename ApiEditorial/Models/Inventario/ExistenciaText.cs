using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiEditorial.Models.Inventario
{
    public class ExistenciaText
    {
        public int IdPersonal{ get; set; }
        public int IdLibros { get; set; }
        public int TotalLibrosEntregados { get; set; }
        public int Stock { get; set; }
    }
}
