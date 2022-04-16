using ApiEditorial.Models;
using ApiEditorial.Models.Inventario;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ApiEditorial.Data.Inventario
{
    public class InventarioInicialRepository
    {
        private readonly string _connectionString;
        public InventarioInicialRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("cn");
        }
        public async Task Insert(Inicial valor)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                
                try
                {
                    int resultado = 0;    //bool resultado = false;
                    using (SqlCommand cmd = new SqlCommand("sp_registrar_invInicial", sql))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@idUsuario", valor.idUsuario));
                        cmd.Parameters.Add(new SqlParameter("@cantidadInicial", valor.cantidaInicio));
                        cmd.Parameters.Add(new SqlParameter("@cantidaItems", valor.cantidadItem));
                        await sql.OpenAsync();
                        await cmd.ExecuteNonQueryAsync();
                        
                       resultado = 1;
                    }

                    if (resultado==1)
                    {
                        if (valor.ActualizacionStock != null)
                        {

                            foreach (var item in valor.ActualizacionStock)
                            {
                                 await InsertStock(item.idLibros, item.nuevaExistencia);
                            }
                        }
                    }
                }
                catch (Exception)
                {
                    throw;
                }
              

            }
        }
     
        public async Task InsertStock(int idLibros, int nuevaExistencia)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_ActStock", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@idLibros", idLibros));
                    cmd.Parameters.Add(new SqlParameter("@StockInicial", nuevaExistencia));
                    await sql.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                    return;
                }
            }
        }
        public async Task<List<Libros>> GetAll()
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_iniciar_libros", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    var response = new List<Libros>();
                    await sql.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            response.Add(MapToValue(reader));
                        }
                    }
                    return response;
                }
            }
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
                TotalAlmacen = (int)reader["TotalAlmacen"]
            };
        }
        public async Task Ajustes(ExistenciaAlm existencia)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_Actualizar_stock", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("IdExistenciaAlm", existencia.IdExistenciaAlm));
                    cmd.Parameters.Add(new SqlParameter("TotalAlmacen", existencia.TotalAlmacen));
                    await sql.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                    return;
                }
            }
        }
    }
}
