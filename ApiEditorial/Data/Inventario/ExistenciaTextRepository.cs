using ApiEditorial.Models.Inventario;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ApiEditorial.Data.Inventario
{
    public class ExistenciaTextRepository
    {
        private readonly string _connectionString;
        public ExistenciaTextRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("cn");
        }
        public async Task Insert(ExistenciaText valor)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd=new SqlCommand("sp_insertar_ExistenciaTextos",sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("IdPersonal", valor.IdLibros));
                    cmd.Parameters.Add(new SqlParameter("IdLibros", valor.IdPersonal));
                    cmd.Parameters.Add(new SqlParameter("TotalLibrosEntregados", valor.TotalLibrosEntregados));
                    await sql.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                    return;
                }
            }
        }

        public async Task Pedidos(Pedidos valor)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                try
                {
                    int result = 0;
                    using (SqlCommand cmd = new SqlCommand("sp_nuevo_pedido", sql))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("IdPersonal", valor.IdPersonal));
                        cmd.Parameters.Add(new SqlParameter("NumPedido", valor.NumPedido));
                        cmd.Parameters.Add(new SqlParameter("Cantidadtotal", valor.CantidadTotal));
                        await sql.OpenAsync();
                        await cmd.ExecuteNonQueryAsync();

                        result = 1;
                    }
                    if(result==1)
                    {
                        if (valor.detallePedidos!=null)
                        {
                            foreach (var item in valor.detallePedidos)
                            {
                                await insertDetalle(item.IdPedidos, item.IdExTextos, item.Cantidad);

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

        public async Task insertDetalle(int IdPedidos,int IdExTextos,int Cantidad)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("spInsertarDetallePedido", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("IdPedidos", IdPedidos));
                    cmd.Parameters.Add(new SqlParameter("IdExTextos", IdExTextos));
                    cmd.Parameters.Add(new SqlParameter("Cantidad", Cantidad));
                    await sql.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                    return;
                }
            }
        }
    }
}
