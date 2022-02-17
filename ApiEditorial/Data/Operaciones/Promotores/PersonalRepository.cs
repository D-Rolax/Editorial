using ApiEditorial.Models.Usuarios;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ApiEditorial.Data.Usuarios
{
    public class PersonalRepository
    {
        private readonly string _connectionString;
        public PersonalRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("cn");
        }
        public async Task Insert(Personal personal)
        {
            using (SqlConnection sql= new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("spInsertarPersonal", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("IdCargo", personal.IdCargo));
                    cmd.Parameters.Add(new SqlParameter("Nombres", personal.Nombres));
                    cmd.Parameters.Add(new SqlParameter("Apellidos", personal.Apellidos));
                    cmd.Parameters.Add(new SqlParameter("Celular", personal.Celular));
                    cmd.Parameters.Add(new SqlParameter("Sueldo", personal.Sueldo));
                    cmd.Parameters.Add(new SqlParameter("FechaIngreso", personal.FechaIngreso));
                    await sql.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                    return;
                }
            }
        }
        public async Task<List<Cliente>> MostrarCliente()
        {
            using(SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd= new SqlCommand("sp_mostrar_cliente", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    var response = new List<Cliente>();
                    await sql.OpenAsync();
                    using(var reader =await cmd.ExecuteReaderAsync())
                    {
                        while(await reader.ReadAsync())
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
                IdColegio = (int)reader["IdColegio"],
                Colegio = reader["Colegio"].ToString(), 
                Zona = reader["Zona"].ToString(),
                Asugnatura = reader["Asignatura"].ToString(),
                Celular = (int)reader["Celular"],
            };
        }
    }
}
