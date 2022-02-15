using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiEditorial.Models.Usuarios
{
    public class Cliente
    {
        public int IdCliente { get; set; }
        public int IdColegio { get; set; }
        public string NombreCompleto { get; set; }
        public string Asugnatura { get; set; }
        public int Celular { get; set; }
        public string Comentario { get; set; }
        public DateTime Fecha { get; set; }
        public bool Estado { get; set; }
    }
}
