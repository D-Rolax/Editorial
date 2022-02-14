using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiEditorial.Models.Usuarios
{
    public class Personal
    {
        public int IdPersonal { get; set; }
        public int IdCargo { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public int Celular { get; set; }
        public double Sueldo{ get; set; }
        public DateTime FechaIngreso { get; set; }
        public DateTime FechaRegistro { get; set; }
        public bool Estado { get; set; }
    }
}
