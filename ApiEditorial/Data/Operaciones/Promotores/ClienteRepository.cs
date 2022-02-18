using ApiEditorial.Models.Usuarios;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ApiEditorial.Data.Operaciones.Promotores
{
    public class ClienteRepository
    {
        private readonly string _stringConnection;
        public ClienteRepository(IConfiguration configuration)
        {

            _stringConnection = configuration.GetConnectionString("cn");
        }

        public async Task<List<Cliente>> MostrarCliente()
        {
            using (SqlConnection sql = new SqlConnection(_stringConnection))
            {
                using (SqlCommand cmd = new SqlCommand("sp_mostrar_cliente", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    var response = new List<Cliente>();
                    await sql.OpenAsync();
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            response.Add(ListaCliente(reader));
                        }
                    }
                    return response;
                }
            }
        }

        private Cliente ListaCliente(SqlDataReader reader)
        {
            return new Cliente()
            {
                IdCliente = (int)reader["IdCliente"],
                NombreCompleto = reader["NombreCompleto"].ToString(),
                Ci = (int)reader["Ci"],
                IdColegio = (int)reader["IdColegio"],
                Colegio = reader["Colegio"].ToString(),
                Zona = reader["Zona"].ToString(),
                Asignatura = reader["Asignatura"].ToString(),
                Celular = (int)reader["Celular"],
            };
        }
        public async Task Insertar(Cliente cliente)
        {
            using(SqlConnection sql =new SqlConnection(_stringConnection))
            {
                using(SqlCommand cmd=new SqlCommand("sp_nuevo_cliente",sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("IdColegio", cliente.IdColegio));
                    cmd.Parameters.Add(new SqlParameter("NombreCompleto", cliente.NombreCompleto));
                    cmd.Parameters.Add(new SqlParameter("Asignatura", cliente.Asignatura));
                    cmd.Parameters.Add(new SqlParameter("Celular", cliente.Celular));
                    cmd.Parameters.Add(new SqlParameter("Comentario", cliente.Comentario));
                    cmd.Parameters.Add(new SqlParameter("Ci", cliente.Ci));
                    await sql.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                    return;
                }
            }
             
        }
    }
}
