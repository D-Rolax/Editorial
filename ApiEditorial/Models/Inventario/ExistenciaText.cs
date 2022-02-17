using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiEditorial.Models.Inventario
{
    public class ExistenciaText
    {
        public int IdExistenciaTex { get; set; }
        public int IdPersonal { get; set; }
        public int IdLibros { get; set; }
        public string NombreDescripcion { get; set; }
        public decimal PrecioM { get; set; }
        public int LibrosRecibidos { get; set; }
        public int LibrosDevueltos { get; set; }
        public int LibrosGuias { get; set; }
        public decimal DeudaTotal { get; set; }
        public int Stock { get; set; }
        public DateTime FechaRegistro { get; set; }
        public Boolean Estado { get; set; }
    }
}
