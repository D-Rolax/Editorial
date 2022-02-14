using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiEditorial.Models.Inventario
{
    public class Inicial
    {
        public int idUsuario { get; set; }
        public int cantidaInicio { get; set; }
        public int cantidadItem { get; set; }
        public List<ActualizacionStock> ActualizacionStock { get; set; }
        public Inicial()
        {
            this.ActualizacionStock = new List<ActualizacionStock>();
        }

    }
    public class ActualizacionStock
    {
        public int idLibros { get; set; }
        public int nuevaExistencia { get; set; }
    }
}
