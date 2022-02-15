using ApiEditorial.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ApiEditorial.Data
{
    public class LibrosRepository
    {
        private readonly string _connectionString;
        public LibrosRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("cn");
        }
        public async Task<List<Libros>> GetAll()
        {
            using (SqlConnection sql =new SqlConnection(_connectionString))
            {
                using(SqlCommand cmd =new SqlCommand("sp_mostrar_Libros",sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    var response = new List<Libros>();
                    await sql.OpenAsync();

                    using(var reader = await cmd.ExecuteReaderAsync())
                    {
                        while(await reader.ReadAsync())
                        {
                            response.Add(MapToValue(reader));
                        }
                    }
                    return response;
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
        private Almacen AgregarAlmacen (SqlDataReader reader)
        {
            return new Almacen()
            {
                IdAlmacen = (int)reader["IdAlmacen"],
                Nombre = reader["Nombre"].ToString(),
            };
        }
        private Libros MapToValue(SqlDataReader reader)
        {
            return new Libros()
            {
                IdLibros = (int)reader["IdLibros"],
                Nombre = reader["Nombre"].ToString(),
                Nivel = reader["Nivel"].ToString(),
                PrecioU = (decimal)reader["PrecioU"],
                PrecioM = (decimal)reader["PrecioM"],
                PrecioR = (decimal)reader["PrecioR"],
                Descripcion = reader["Descripcion"].ToString(),
                FechaRegistro = (DateTime)reader["FechaRegistro"],
                Estado = (bool)reader["Estado"],
                TotalAlmacen= (int)reader["TotalAlmacen"]
            };
        }

        public async Task update (Libros libros)
        {
            using (SqlConnection sql=new SqlConnection(_connectionString))
            {
                using(SqlCommand cmd= new SqlCommand("sp_actualizar_libros", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("IdLibros", libros.IdLibros));
                    cmd.Parameters.Add(new SqlParameter("Nombre", libros.Nombre));
                    cmd.Parameters.Add(new SqlParameter("Nivel",libros.Nivel));
                    cmd.Parameters.Add(new SqlParameter("PrecioU", libros.PrecioU));
                    cmd.Parameters.Add(new SqlParameter("PrecioM", libros.PrecioM));
                    cmd.Parameters.Add(new SqlParameter("PrecioR", libros.PrecioR));
                    cmd.Parameters.Add(new SqlParameter("Descripcion", libros.Descripcion));
                    cmd.Parameters.Add(new SqlParameter("Estado", libros.Estado));
                    await sql.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                    return;
                }
            }
        }
        public async Task Insert(Libros libros)
        {
            using(SqlConnection sql=new SqlConnection(_connectionString))
            {
                using(SqlCommand cmd=new SqlCommand("sp_registrar_libros",sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("Nombre", libros.Nombre));
                    cmd.Parameters.Add(new SqlParameter("Nivel", libros.Nivel));
                    cmd.Parameters.Add(new SqlParameter("PrecioU", libros.PrecioU));
                    cmd.Parameters.Add(new SqlParameter("PrecioM", libros.PrecioM));
                    cmd.Parameters.Add(new SqlParameter("PrecioR", libros.PrecioR));
                    cmd.Parameters.Add(new SqlParameter("Descripcion", libros.Descripcion));
                    await sql.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                    return;
                }
            }
        }
        public Task DeletById(int Id)
        {
            throw new NotImplementedException();
        }
    }
}
