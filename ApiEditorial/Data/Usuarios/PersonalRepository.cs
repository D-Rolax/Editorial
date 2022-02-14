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
    }
}
