using ApiEditorial.Models.Caja;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ApiEditorial.Data.Operaciones.Caja
{
    public class VentasRepository
    {
        private readonly string connectionString;

        public VentasRepository(IConfiguration configuration)
        {
            connectionString= configuration.GetConnectionString("cn");
        }
        public async Task insert(Ventas values)
        {
            using (SqlConnection sql =new SqlConnection(connectionString))
            {
                try
                {
                    int result = 0;
                    using (SqlCommand cmd = new SqlCommand("sp_nueva_venta", sql))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("IdCliente", values.IdCliente));
                        cmd.Parameters.Add(new SqlParameter("IdUsuario", values.IdUsuario));
                        cmd.Parameters.Add(new SqlParameter("Cantidad", values.Cantidad));
                        cmd.Parameters.Add(new SqlParameter("LibroGuia", values.LibroGuia));
                        cmd.Parameters.Add(new SqlParameter("Total", values.Total));
                        cmd.Parameters.Add(new SqlParameter("NombreAlumno", values.NombreAlumno));
                        cmd.Parameters.Add(new SqlParameter("IdComision", values.IdComision));
                        await sql.OpenAsync();
                        await cmd.ExecuteNonQueryAsync();
                        result = 1;
                    }
                    if (result==1)
                    {
                        if (values.DetalleVentas!=null)
                        {
                            foreach (var item in values.DetalleVentas)
                            {
                                await InsertDetalle(item.IdLibro, item.Cantidad, item.Precio, item.SubTotal, values.IdCliente);
                            }
                        }
                    }
                }
                catch (Exception)
                {

                    throw;
                }

            }
        }

        private async Task InsertDetalle(int idLibro, int cantidad, decimal precio, decimal subTotal, int idPersonal)
        {
            using (SqlConnection sql=new SqlConnection(connectionString))
            {
                using (SqlCommand cmd=new SqlCommand("spNuevoDetalleVenta",sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("IdLibro", idLibro));
                    cmd.Parameters.Add(new SqlParameter("Cantidad", cantidad));
                    cmd.Parameters.Add(new SqlParameter("Precio", precio));
                    cmd.Parameters.Add(new SqlParameter("SubTotal", subTotal));
                    cmd.Parameters.Add(new SqlParameter("IdPersonal", idPersonal));
                    await sql.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                    return;
                }
            }
        }
    }
}
