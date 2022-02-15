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
        public async Task<List<ListaDetallePedido>> ListaDetallePedido(int idPedidos)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_listarDetallePedidos", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("IdPedidos", idPedidos));
                    var response = new List<ListaDetallePedido>();
                    await sql.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            response.Add(AgregarDetallePedido(reader));
                        }
                    }
                    return response;
                }
            }
        }
        private ListaDetallePedido AgregarDetallePedido(SqlDataReader reader)
        {
            return new ListaDetallePedido()
            {
                IdPedidos = (int)reader["IdPedidos"],
                IdLibro = (int)reader["IdLibro"],
                CantRecibida = (int)reader["CantRecibida"],
                Cantidad = (int)reader["Cantidad"],
                nombreDescripcion=(string)reader["nombreDescripcion"].ToString()
            };
        }
        public async Task<List<ListarPedidos>> ListarPedidos(int idPersonal)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_listarPedidos", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("idPersonal", idPersonal));
                    var response = new List<ListarPedidos>();
                    await sql.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            response.Add(AgregarPedido(reader));
                        }
                    }
                    return response;
                }
            }
        }
        private ListarPedidos AgregarPedido(SqlDataReader reader)
        {
            return new ListarPedidos()
            {
                IdPedidos = (int)reader["IdPedidos"],
                IdPersonal = (int)reader["IdPersonal"],
                
                NumPedido = (int)reader["NumPedido"],
                CantidadTotal = (int)reader["CantidadTotal"],
                TotalRecibidos = (int)reader["TotalRecibidos"],
                FechaRegistro = (DateTime)reader["FechaRegistro"],
                Motivo = reader["Motivo"].ToString(),
                Destino = reader["Destino"].ToString(),
                Estado = reader["Estado"].ToString(),
            };
        }
       
      
    }
}
