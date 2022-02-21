using ApiEditorial.Models;
using ApiEditorial.Models.Usuarios;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ApiEditorial.Data.Operaciones.Promotores
{
    public class ContratoRepository
    {
        private readonly string _connectionString;
     

        public ContratoRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("cn");
        }
        public async Task Insert(Contrato contrato)
        {
            using(SqlConnection sql =new SqlConnection(_connectionString))
            {
                try
                {
                    int result = 0;
                    using (SqlCommand cmd = new SqlCommand("sp_nuevo_Contrato", sql))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("IdCliente", contrato.IdCliente));
                        cmd.Parameters.Add(new SqlParameter("IdPersonal", contrato.IdPersonal));
                        cmd.Parameters.Add(new SqlParameter("NumContrato", contrato.NumContrato ));
                        cmd.Parameters.Add(new SqlParameter("TotalTextos", contrato.TotalTextos));
                        cmd.Parameters.Add(new SqlParameter("TotalDeuda", contrato.TotalDeuda));
                        cmd.Parameters.Add(new SqlParameter("Estado", contrato.Estado));
                        cmd.Parameters.Add(new SqlParameter("LibroGuia", contrato.LibroGuia));
                        cmd.Parameters.Add(new SqlParameter("NombreCompleto", contrato.NombreCompleto));
                        cmd.Parameters.Add(new SqlParameter("Celular", contrato.Celular));
                        cmd.Parameters.Add(new SqlParameter("Ci", contrato.Ci));
                        cmd.Parameters.Add(new SqlParameter("IdColegio", contrato.IdColegio));
                        cmd.Parameters.Add(new SqlParameter("Nombre", contrato.Nombre));
                        cmd.Parameters.Add(new SqlParameter("Zona", contrato.Zona));
                        await sql.OpenAsync();
                        await cmd.ExecuteNonQueryAsync();
                        result = 1;
                    }
                    if (result==1)
                    {
                        if (contrato.DetalleTextos!= null)
                        {
                            if (contrato.Estado=="Entregado")
                            {
                                foreach (var item in contrato.DetalleTextos)
                                {
                                    await insertDetalleTexto(item.IdLibros, item.Cantidad, item.Precio, item.LibroGuia);
                                    await ConfirmarContrato(contrato.IdPersonal, item.IdLibros, item.Cantidad);
                                }
                            }
                            else
                            {
                                foreach (var item in contrato.DetalleTextos)
                                {
                                    await insertDetalleTexto(item.IdLibros, item.Cantidad, item.Precio, item.LibroGuia);
                                }
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

        private async Task insertDetalleTexto(int idLibros, int cantidad, decimal precio, int LibroGuia)
        {
            using(SqlConnection sql=new SqlConnection(_connectionString))
            {
                using(SqlCommand cmd=new SqlCommand("sp_nuevo_detalleContrato",sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("IdLibros", idLibros));
                    cmd.Parameters.Add(new SqlParameter("Cantidad", cantidad));
                    cmd.Parameters.Add(new SqlParameter("Precio", precio));
                    cmd.Parameters.Add(new SqlParameter("LibroGuia", LibroGuia));
                    await sql.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                    return;
                }
            }
        }
        private async Task ActualizarDetalleTexto(int idDetalle, int cantidad)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("spActualizarDetalleTextos", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("IdDetalle", idDetalle));
                    cmd.Parameters.Add(new SqlParameter("Cantidad", cantidad));
                    await sql.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                    return;
                }
            }
        }
        private async Task ConfirmarContrato(int idPersonal,int idLibro,int cantidad)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using(SqlCommand cmd=new SqlCommand("sp_confirmar_contrato",sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("IdPersonal", idPersonal));
                    cmd.Parameters.Add(new SqlParameter("IdLibros", idLibro));  
                    cmd.Parameters.Add(new SqlParameter("Cantidad", cantidad));
                    await sql.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                    return;
                }
            }
        }
        public async Task<List<Contrato>> Mostrar(int Id)
        {
            using (SqlConnection sql=new SqlConnection(_connectionString))
            {
                using(SqlCommand cmd =new SqlCommand("sp_mostrar_contrato",sql))
                {
                    
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@idPersona", Id));
                    var response = new List<Contrato>();
                    await sql.OpenAsync();
                    using(var reader=await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            response.Add(ListaContrato(reader));              
                        }
                    }
                    return response;
                }
            }
        }
        public async Task<List<DetalleTextos>> MostrarDetalle(int Id)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_mostrar_detalle_contrato", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("IdContrato", Id));
                    var response = new List<DetalleTextos>();
                    await sql.OpenAsync();
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            response.Add(ListaDetalle(reader));
                        }
                    }
                    return response;
                }
            }
        }
       
        private Contrato ListaContrato(SqlDataReader reader)
        {
            var Detalle1 = new List<DetalleTextos>();
            return new Contrato()
            {
                idContrato = (int)reader["IdContrato"],
                NumContrato = (int)reader["NumContrato"],
                Fecha = (DateTime)reader["Fecha"],
                IdCliente = (int)reader["IdCliente"],
                NombreCompleto = reader["NombreCompleto"].ToString(),
                Celular = reader["Celular"].ToString(),
                Ci = reader["Ci"].ToString(),
                Asignatura = reader["Asignatura"].ToString(),
                IdColegio = (int)reader["IdColegio"],
                Nombre = reader["Nombre"].ToString(),
                Municipio = reader["Municipio"].ToString(),
                Direccion = reader["Direccion"].ToString(),
                NivelesAtencion = reader["NivelesAtencion"],
                Tipo = reader["Tipo"].ToString(),
                Turno = reader["Turno"].ToString(),
                Zona = reader["Zona"].ToString(),
                TotalTextos = (int)reader["TotalTextos"],
                TotalDeuda = (decimal)reader["TotalDeuda"],
                Saldo = (decimal)reader["Saldo"],
                Estado = reader["Estado"].ToString(),
                LibroGuia = (int)reader["LibroGuia"],
                IdPersonal = (int)reader["IdPersonal"],
                NombrePersonal = reader["NombrePersonal"].ToString(),
                DetalleTextos = Detalle1
            };
        }

        private DetalleTextos ListaDetalle(SqlDataReader reader)
        {
            return new DetalleTextos()
            {
                IdContrato=(int)reader["IdContrato"],
                IdLibros=(int)reader["IdLibros"],
                Nombre=reader["Nombre"].ToString(),
                Cantidad=(int)reader["Cantidad"],
                Precio=(decimal)reader["Precio"],
                LibroGuia=(int)reader["LibroGuia"]
            };
        }
        public async Task<List<Contrato>> VerContrato()
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_ver_NumComtrato", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    var response = new List<Contrato>();
                    await sql.OpenAsync();
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            response.Add(ListaNumContrato(reader));
                        }
                    }
                    return response;
                }
            }
        }

        private Contrato ListaNumContrato(SqlDataReader reader)
        {
            return new Contrato()
            {
                NumContrato = (int)reader["NumContrato"]
            };
        }
        public async Task Update(Contrato contrato)
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                try
                {
                    int result = 0;
                    using (SqlCommand cmd = new SqlCommand("spCambiarEstadoContrato", sql))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("IdContrato", contrato.idContrato));
                        cmd.Parameters.Add(new SqlParameter("Estado", contrato.Estado));
                        cmd.Parameters.Add(new SqlParameter("IdPersonal", contrato.IdPersonal));
                        await sql.OpenAsync();
                        await cmd.ExecuteNonQueryAsync();
                        result = 1;
                    }
                    if (result == 1)
                    {
                        if (contrato.DetalleTextos != null)
                        {
                            foreach (var item in contrato.DetalleTextos)
                            {
                                await ActualizarDetalleTexto(item.IdDetalle,item.Cantidad);
                                await ConfirmarContrato(contrato.IdPersonal, item.IdLibros, item.Cantidad);
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
    }
}
