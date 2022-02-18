    using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiEditorial.Models.Usuarios
{
    public class Colegio
    {
        public int IdColegio { get; set; }
        public string Municipio { get; set; }
        public string Zona { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string NivelesAtencion { get; set; }
        public string Tipo { get; set; }
        public string Turno { get; set; }
        public bool Estado { get; set; }
    }
}
