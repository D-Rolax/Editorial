using ApiEditorial.Data.Inventario;
using ApiEditorial.Data.Operaciones.Promotores;
using ApiEditorial.Models;
using ApiEditorial.Models.Inventario;
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
        [HttpPost]
        [Route("ConfirmarPedido")]
        public async Task ConfirmarPedido([FromBody] ConfirmarPedido pedidos)
        {
            await repository.ConfirmarPedido(pedidos);
        }

        [HttpGet]
        [Route("ListarPedidos/{Id}")]
        public async Task<List<ListarPedidos>> ListarPedidos(int Id)
        {
            var response = await repository.ListarPedidos(Id);
            if (response == null) { NotFound(); }
            return response;
        }
        [HttpGet]
        //[Route("RecibirDatosAnimales/{CodR_Animal}")]
        [Route("ListarDetallePedidos/{idPedidos}")]
        public async Task<List<ListaDetallePedido>> ListarDetallePedidos(int idPedidos)
        {
            var response =  await repository.ListaDetallePedido(idPedidos);
            if (response == null) { NotFound(); }
            return response;
            //await repository.Pedidos(pedidos);
        }
   
    }
}
