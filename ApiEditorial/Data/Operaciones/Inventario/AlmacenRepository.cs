using ApiEditorial.Models;
using ApiEditorial.Models.Inventario;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ApiEditorial.Data.Operaciones.Inventario
{
    public class AlmacenRepository
    {
        private readonly string _connectionString;
        public AlmacenRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("cn");
        }
        public async Task Insert(Almacen valor)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_registrar_almacen", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Nombre", valor.Nombre));
                    cmd.Parameters.Add(new SqlParameter("@Direccion", valor.Direccion));
                    cmd.Parameters.Add(new SqlParameter("@Descripcion", valor.Descripcion));
                    cmd.Parameters.Add(new SqlParameter("@IdCliente", valor.IdCLiente));
                    await sql.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }
        public async Task Update(Almacen valor)
        {
            using (SqlConnection sql=new SqlConnection(_connectionString))
            {
                using(SqlCommand cmd=new SqlCommand("sp_EditarAlmacen",sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@IdAlmacen", valor.IdAlmacen));
                    cmd.Parameters.Add(new SqlParameter("@Nombre", valor.Nombre));
                    cmd.Parameters.Add(new SqlParameter("@Direccion", valor.Direccion));
                    cmd.Parameters.Add(new SqlParameter("@Descripcion", valor.Descripcion));
                    cmd.Parameters.Add(new SqlParameter("@IdCliente", valor.IdCLiente));
                    await sql.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }
        public async Task<List<Almacen>> ListarAlmacen()
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_mostrar_Almacen", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    var response = new List<Almacen>();
                    await sql.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            response.Add(AgregarAlmacen(reader));
                        }
                    }
                    return response;
                }
            }
        }
        private Almacen AgregarAlmacen(SqlDataReader reader)
        {
            return new Almacen()
            {
                IdAlmacen = (int)reader["IdAlmacen"],
                Nombre = reader["Nombre"].ToString(),
                Direccion = reader["Direccion"].ToString(),
                Descripcion = reader["Descripcion"].ToString(),
                Estado = (bool)reader["Estado"],
                FechaRegistro = (DateTime)reader["FechaRegistro"]
            };
        }
    }
}
