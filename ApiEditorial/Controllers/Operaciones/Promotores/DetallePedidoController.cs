using ApiEditorial.Data.Inventario;
using ApiEditorial.Data.Operaciones.Promotores;
using ApiEditorial.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiEditorial.Controllers.Operaciones.Promotores
{
    [Route("api/[controller]")]
    [ApiController]
    public class DetallePedidoController : ControllerBase
    {
        private readonly DetallePedidoRepository repository;

        public DetallePedidoController(DetallePedidoRepository repository)
        {
            this.repository = repository ?? throw new ArgumentException(nameof(repository));
        }
        [HttpGet("{Id}")]
        public async Task<List<ListaDetallePedido>> DetallePedido(int Id)
        {
            var response = await repository.DetallePedido(Id);
            if (response == null) { NotFound(); }
            return response;
        }
        //[HttpGet("{Id}")]
        //public async Task<List<ListaDetallePedido>> DetallePedido(int Id)
        //{
        //    var response = await repository.DetallePedido(Id);
        //    if (response == null) { NotFound(); }
        //    return response;
        //}
    }
}
