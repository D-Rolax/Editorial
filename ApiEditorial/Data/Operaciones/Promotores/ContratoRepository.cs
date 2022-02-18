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
                        cmd.Parameters.Add(new SqlParameter("NumContrato", contrato.NumContrato));
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
                                    await insertDetalleTexto(item.IdLibros, item.Cantidad, item.Precio, item.Observaciones);
                                    await ConfirmarContrato(contrato.IdPersonal, item.IdLibros, item.Cantidad);
                                }
                            }
                            else
                            {
                                foreach (var item in contrato.DetalleTextos)
                                {
                                    await insertDetalleTexto(item.IdLibros, item.Cantidad, item.Precio, item.Observaciones);
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

        private async Task insertDetalleTexto(int idLibros, int cantidad, decimal precio, string observaciones)
        {
            using(SqlConnection sql=new SqlConnection(_connectionString))
            {
                using(SqlCommand cmd=new SqlCommand("sp_nuevo_detalleContrato",sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("IdLibros", idLibros));
                    cmd.Parameters.Add(new SqlParameter("Cantidad", cantidad));
                    cmd.Parameters.Add(new SqlParameter("Precio", precio));
                    cmd.Parameters.Add(new SqlParameter("Observaciones", observaciones));
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
    }
}
