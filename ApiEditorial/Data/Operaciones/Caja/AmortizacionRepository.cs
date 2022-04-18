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
        
        public async Task<List<Amortizacion>> GetAll()
        {
            using (SqlConnection sql = new SqlConnection(conectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_MostrarAmortizacion", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    var response = new List<Amortizacion>();
                    await sql.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            response.Add(MapToValue(reader));
                        }
                    }
                    return response;
                }
            }
        }

        private Amortizacion MapToValue(SqlDataReader reader)
        {
            return new Amortizacion()
            {
                IdAmortizacion = (int)reader["IdAmortizacion"],
                IdPersonal = (int)reader["IdPromotor"],
                NumAmortizacion = (int)reader["NumComprobante"],
                MontoTotal = (decimal)reader["MontoTotal"],
                Fecha = (DateTime)reader["Fecha"],
                Estado= (bool)reader["Estado"],
                IdVenta = (int)reader["IdVenta"]
            };
        }
        public async Task<List<DetalleAmortizacion>> GetDetalle(int IdAmortizacion)
        {
            using (SqlConnection sql = new SqlConnection(conectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_MostrarDetalleAmortizacion", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("IdAmortizacion", IdAmortizacion));
                    var response = new List<DetalleAmortizacion>();
                    await sql.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            response.Add(ListaDetalle(reader));
                        }
                    }
                    return response;
                }
            }
        }
        private DetalleAmortizacion ListaDetalle(SqlDataReader reader)
        {
            return new DetalleAmortizacion()
            {
                IdAmortizacion = (int)reader["IdAmortizacion"],
                NumContrato = (int)reader["NumContrato"],
                NumRecibo = (int)reader["NumRecibo"],
                Monto = (decimal)reader["Monto"]
            };
        }
    }
}
