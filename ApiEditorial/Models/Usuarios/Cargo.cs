using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiEditorial.Models.Usuarios
{
    public class Cargo
    {
        public int IdCargo { get; set; }
        public string Puesto { get; set; }
        public string Descripcion { get; set; }
        public DateTime FecgaRegistro { get; set; }
        public bool Estado { get; set; }
    }
}
