using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiEditorial.Models.Usuarios
{
    public class Cliente
    {
        public int IdCliente { get; set; }
        public string NombreCompleto { get; set; }
        public int IdColegio { get; set; }
        public string Colegio { get; set; }
        public string Zona{ get; set; }
        public string Asignatura { get; set; }
        public string Celular { get; set; }
        public string Comentario { get; set; }
        public DateTime Fecha { get; set; }
        public bool Estado { get; set; }
        public string Ci { get; set; }
    }
}
