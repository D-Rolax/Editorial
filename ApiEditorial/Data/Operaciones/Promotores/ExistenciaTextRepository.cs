using ApiEditorial.Models;
using ApiEditorial.Models.Inventario;
using ApiEditorial.Models.Usuarios;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ApiEditorial.Data.Inventario
{
    public class ExistenciaTextRepository
    {
        private readonly string _connectionString;
        public ExistenciaTextRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("cn");
        }
        public async Task NuevaExistencia(Personal personal)
        {
            if (personal.Existencia!= null)
            {
                foreach (var item in personal.Existencia)   
                {
                    await InsertarExistencia(item.IdLibros, item.IdPersonal, item.LibrosRecibidos);
                }
            }
        }
        public async Task InsertarExistencia(int IdLibros,int IdPersonal,int TotalLibrosEntregados)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd=new SqlCommand("sp_insertar_ExistenciaTextos",sql))
                {
                    
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@IdPersonal",IdPersonal));
                    cmd.Parameters.Add(new SqlParameter("IdLibros", IdLibros)); 
                    cmd.Parameters.Add(new SqlParameter("TotalLibrosPedidos", TotalLibrosEntregados));
                    await sql.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                    return;
                }
            }
        }
        public async Task Pedidos(Pedidos valor)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                try
                {
                    int result = 0;
                    using (SqlCommand cmd = new SqlCommand("sp_nuevo_pedido", sql))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("IdPersonal", valor.IdPersonal));
                        cmd.Parameters.Add(new SqlParameter("NumPedido", valor.NumPedido));
                        cmd.Parameters.Add(new SqlParameter("Cantidadtotal", valor.CantidadTotal));
                        cmd.Parameters.Add(new SqlParameter("Motivo", valor.Motivo));
                        cmd.Parameters.Add(new SqlParameter("Destino", valor.Destino));

                        await sql.OpenAsync();
                        await cmd.ExecuteNonQueryAsync();

                        result = 1;
                    }
                    if (result == 1)
                    {
                        if (valor.detallePedidos != null)
                        {
                            foreach (var item in valor.detallePedidos)
                            {
                                await insertDetalle(item.IdLibro, item.Cantidad);

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
        public async Task insertDetalle(int IdLibro, int Cantidad)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("spInsertarDetallePedido", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("IdLibros", IdLibro));
                    cmd.Parameters.Add(new SqlParameter("Cantidad", Cantidad));
                    await sql.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                    return;
                }
            }
        }
        public async Task<List<ExistenciaText>>MostrarExistencia(int Id)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using(SqlCommand cmd= new SqlCommand("sp_Mostrar_Existencia",sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("IdPersonal", Id));
                    var response = new List<ExistenciaText>();
                    await sql.OpenAsync();

                    using(var reader=await cmd.ExecuteReaderAsync())
                    {
                        while(await reader.ReadAsync())
                        {
                            response.Add(ListarExistencia(reader));
                        }
                    }
                    return response;
                }
            }
        }

        private ExistenciaText ListarExistencia(SqlDataReader reader)
        {
            return new ExistenciaText()
            {
                IdExistenciaTex = (int)reader["IdExistenciaTex"],
                IdLibros = (int)reader["IdLibros"],
                NombreDescripcion = reader["NombreDescripcion"].ToString(),
                LibrosRecibidos = (int)reader["LibrosRecibidos"],
                LibrosDevueltos = (int)reader["LibrosDevueltos"],
                LibrosGuias = (int)reader["LibrosGuias"],
                DeudaTotal = (decimal)reader["DeudaTotal"],
                FechaRegistro = (DateTime)reader["FechaRegistro"],
                Estado = (bool)reader["Estado"],
            };
        }
    }
}
