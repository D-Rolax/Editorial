using ApiEditorial.Models.Usuarios;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ApiEditorial.Data.Operaciones.Promotores
{
    public class ColegioRepository
    {
        private readonly string _stringConnection;
        public ColegioRepository(IConfiguration configuration)
        {
            _stringConnection = configuration.GetConnectionString("cn");
        }
        public async Task<List<Colegio>>Mostrar()
        {
            using(SqlConnection sql=new SqlConnection(_stringConnection))
            {
                using(SqlCommand cmd=new SqlCommand("sp_mostrar_colegio",sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    var response = new List<Colegio>(); 
                    await sql.OpenAsync();
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            response.Add(MostrarColegio(reader));
                        }
                    }
                    return response;
                }
            }
        }

        private Colegio MostrarColegio(SqlDataReader reader)
        {
            return new Colegio()
            {
                IdColegio = (int)reader["IdColegio"],
                Municipio = reader["Municipio"].ToString(),
                Zona = reader["Zona"].ToString(),
                Nombre = reader["Nombre"].ToString(),
                NivelesAtencion = reader["NivelesAtencion"].ToString(),
                Tipo = reader["Tipo"].ToString(),
                Turno = reader["Turno"].ToString(),
                Estado = (bool)reader["Estado"]
            };
        }
        public async Task insert(Colegio colegio)
        {
            using (SqlConnection sql=new SqlConnection(_stringConnection))
            {
                using (SqlCommand cmd= new SqlCommand("sp_registrar_colegio",sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("Municipio", colegio.Municipio));
                    cmd.Parameters.Add(new SqlParameter("Zona", colegio.Zona));
                    cmd.Parameters.Add(new SqlParameter("Nombre", colegio.Nombre));
                    cmd.Parameters.Add(new SqlParameter("Direccion", colegio.Direccion));
                    cmd.Parameters.Add(new SqlParameter("NivelesAtencion", colegio.NivelesAtencion));
                    cmd.Parameters.Add(new SqlParameter("Tipo", colegio.Tipo));
                    cmd.Parameters.Add(new SqlParameter("Turno", colegio.Turno));
                    await sql.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                    return;
                }

            }
        }
    }
}
