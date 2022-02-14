using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiEditorial.Models
{
    public class Respuesta
    {
        public int Exito { get; set; }
        public string Mensage { get; set; }
        public object Data { get; set; }
    }
}
