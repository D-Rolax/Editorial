using ApiEditorial.Models.Caja;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ApiEditorial.Data.Operaciones.Caja
{
    public class AmortizacionRepository
    {
        private readonly string conectionString;

        public AmortizacionRepository(IConfiguration configuration)
        {
            conectionString= configuration.GetConnectionString("cn");
        }
        public async Task Insert(Amortizacion values)
        {
            using (SqlConnection sql = new SqlConnection(conectionString))
            {
                try
                {
                    int result = 0;
                    using (SqlCommand cmd = new SqlCommand("sp_nueva_amortizacion", sql))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("IdPersonal", values.IdPersonal));
                        cmd.Parameters.Add(new SqlParameter("MontoTotal", values.MontoTotal));
                        await sql.OpenAsync();
                        await cmd.ExecuteNonQueryAsync();
                        result = 1;

                    }
                    if (result == 1)
                    {
                        if (values.DetalleAmortizacion != null)
                        {
                            foreach (var item in values.DetalleAmortizacion)
                            {
                                await InsertDetalle(item.NumContrato, item.NumRecibo, item.Monto);
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
        private async Task InsertDetalle(int NumContrato,int NumRecibo,decimal Monto)
        {
            using (SqlConnection sql = new SqlConnection(conectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_detalle_amortizacion", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("NumContrato", NumContrato));
                    cmd.Parameters.Add(new SqlParameter("NumRecibo", NumRecibo));
                    cmd.Parameters.Add(new SqlParameter("Monto", Monto));
                    await sql.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                    return;
                }
            }
        }
    }
}
