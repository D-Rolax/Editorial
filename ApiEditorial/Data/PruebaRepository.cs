using ApiEditorial.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ApiEditorial.Data
{
    public class PruebaRepository
    {
        private readonly string _connectionString;

        public PruebaRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("cn");
        }
        public async Task<List<Cabecera>> List()
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_Detalle_prueba", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    var response = new List<Cabecera>();
                    await sql.OpenAsync();
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            response.Add(Mostrar(reader));
                        }
                    }
                    return response;
                }
            }
        }

        private Cabecera Mostrar(SqlDataReader reader)
        {
            return new Cabecera()
            {
                IdCabecera=(int)reader["IdCabecera"],
                Cliente=reader["Cliente"].ToString(),
                Detalle = new List<Detalle>()
                {

                }
            };
        }
    }
}
