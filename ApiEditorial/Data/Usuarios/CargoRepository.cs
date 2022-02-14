using ApiEditorial.Models.Usuarios;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ApiEditorial.Data.Usuarios
{
    public class CargoRepository
    {
        private readonly string _connectionString;
        public CargoRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("cn");
        }
        public async Task Insert(Cargo cargo)
        {
            using(SqlConnection sql= new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd=new SqlCommand("spInsertarCargo", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("Puesto", cargo.Puesto));
                    cmd.Parameters.Add(new SqlParameter("Descripcion", cargo.Descripcion));
                    await sql.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                    return;
                }
            }
        }
    }
}
