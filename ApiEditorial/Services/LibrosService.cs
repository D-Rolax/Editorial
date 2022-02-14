using ApiEditorial.Data.Inventario;
using ApiEditorial.Models;
using ApiEditorial.Models.Inventario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiEditorial.Servicios
{
    public class LibrosService
    {
        private readonly InventarioInicialRepository datos;

      
        public LibrosService(InventarioInicialRepository inventarioInicialRepository)
        {
            this.datos = inventarioInicialRepository;
        }
        public bool ActStock( Inicial parametro)
        {
          
            try
            {


               // var res = datos.RegistrarInvInicial(1,21,21);

                if (parametro.ActualizacionStock != null)
                {
                    foreach (var item in parametro.ActualizacionStock)
                    {
                       
                    }
                }
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
        //public DecesoAnimales_AS servicio;
        //public DecescoAnimales_Models()
        //{
        //    servicio = new DecesoAnimales_AS();
        //}
        //public Respuesta ListarLibros()
        //{
        //    //    var result = new Respuesta();
        //    //    result.Exito = 0;
        //    //    try
        //    //    {
        //    //        LibrosDto librosDto = new LibrosDto();
        //    //        var lst = librosDto.ListarLibros();
        //    //        result.Exito = 1;
        //    //        var valor =
        //    //         (from DataRow row in lst.Tables[0].Rows
        //    //          select new LibrosM
        //    //          {
        //    //              IdLibros = Convert.ToInt32(row["IdLibros"]),

        //    //              Nombre = row["Nombre"].ToString(),
        //    //              Descripcion = row["Descripcion"].ToString(),
        //    //              Nivel = row["Nivel"].ToString(),
        //    //              PrecioU = Convert.ToDecimal(row["PrecioU"]),
        //    //              PrecioM = Convert.ToDecimal(row["PrecioM"]),
        //    //              PrecioR = Convert.ToDecimal(row["PrecioR"]),
        //    //              FechaRegistro = Convert.ToDateTime(row["FechaRegistro"]),
        //    //              Estado = Convert.ToBoolean(row["Estado"])

        //    //          }
        //    //          ).ToList();
        //    //        result.Data = valor;
        //    //    }
        //    //    catch (Exception ex)
        //    //    {
        //    //        result.Mensage = ex.Message;
        //    //    }
        //    //    return result;
        //}
    }
}
