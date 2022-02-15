using ApiEditorial.Models;
using ApiEditorial.Models.Inventario;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ApiEditorial.Data.Operaciones.Promotores
{
    public class DetallePedidoRepository
    {
        private readonly string _connectionString;

        public DetallePedidoRepository(IConfiguration configuration)
        {
            this._connectionString = configuration.GetConnectionString("cn");
        }



        public async Task<List<ListaDetallePedido>> DetallePedido(int Id)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                var Criterio="pedidos";
                using (SqlCommand cmd = new SqlCommand("spMostrarDetallePedidos", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("Criterio", Criterio));
                    cmd.Parameters.Add(new SqlParameter("Atributo", Id));
                    var response = new List<ListaDetallePedido>();
                    await sql.OpenAsync();

                    if (Criterio == "todos" || Criterio == "Personal")
                    {
                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                response.Add(ListaPedido(reader));
                            }
                        }
                    }
                    else if (Criterio=="Pedido")
                    {
                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                response.Add(ListaDetalle(reader));
                            }
                        }
                    }
                    return response;
                }
            }
        }
        private ListaDetallePedido ListaDetalle(SqlDataReader reader)
        {
            return new ListaDetallePedido()
            {
                IdPedDev = (int)reader["IdPedDev"],
                IdPersonal = (int)reader["IdPersonal"],
                NumPedido = (int)reader["NumPedido"],
                Nombre = reader["Nombre"].ToString(),
                Cantidad = (int)reader["Cantidad"],
            };
        }
        private ListaDetallePedido ListaPedido(SqlDataReader reader)
        {
            return new ListaDetallePedido()
            {
                IdPedDev = (int)reader["IdPedDev"],
                IdPersonal = (int)reader["IdPersonal"],
                NombreCompleto=reader["NombreCompleto"].ToString(),
                NumPedido = (int)reader["NumPedido"],
                FechaRegistro = (DateTime)reader["FechaRegistro"],
                Estado = reader["Cantidad"].ToString(),
            };
        }
    }
}
