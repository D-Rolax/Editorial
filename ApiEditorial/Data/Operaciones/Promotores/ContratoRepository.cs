using ApiEditorial.Models.Usuarios;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ApiEditorial.Data.Operaciones.Promotores
{
    public class ContratoRepository
    {
        private readonly string _connectionString;
        public ContratoRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("cn");
        }
        public async Task Insert(Contrato valor)
        {
            using(SqlConnection sql =new SqlConnection(_connectionString))
            {
                try
                {
                    int result = 0;
                    using (SqlCommand cmd = new SqlCommand("sp_nuevo_Contrato", sql))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("IdCliente", valor.IdCliente));
                        cmd.Parameters.Add(new SqlParameter("IdPersonal", valor.IdPersonal));
                        cmd.Parameters.Add(new SqlParameter("NumContrato", valor.NumContrato));
                        cmd.Parameters.Add(new SqlParameter("TotalTextos", valor.TotalTextos));
                        cmd.Parameters.Add(new SqlParameter("TotalDeuda", valor.TotalDeuda));
                        await sql.OpenAsync();
                        await cmd.ExecuteNonQueryAsync();
                        result = 1;
                    }
                    if (result==1)
                    {
                        if (valor.DetalleTextos!= null)
                        {
                            foreach (var item in valor.DetalleTextos)
                            {
                                await insertDetalleTexto(item.IdLibros,item.Cantidad,item.Precio,item.Observaciones);
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

        private async Task insertDetalleTexto(int idLibros, int cantidad, decimal precio, string observaciones)
        {
            using(SqlConnection sql=new SqlConnection(_connectionString))
            {
                using(SqlCommand cmd=new SqlCommand("sp_nuevo_detalleContrato",sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("IdLibros", idLibros));
                    cmd.Parameters.Add(new SqlParameter("Cantidad", cantidad));
                    cmd.Parameters.Add(new SqlParameter("Precio", precio));
                    cmd.Parameters.Add(new SqlParameter("Observaciones", observaciones));
                    await sql.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                    return;
                }
            }
        }
    }
}
