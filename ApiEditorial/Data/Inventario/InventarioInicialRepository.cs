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
    
    }
}
