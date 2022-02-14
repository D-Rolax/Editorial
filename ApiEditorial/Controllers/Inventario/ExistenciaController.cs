using ApiEditorial.Data.Inventario;
using ApiEditorial.Models.Inventario;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiEditorial.Controllers.Inventario
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExistenciaController : ControllerBase
    {
        private readonly ExistenciaTextRepository repository;

        public ExistenciaController(ExistenciaTextRepository repository)
        {
            this.repository = repository?? throw new ArgumentException(nameof(repository));
        }
        [HttpPost]
        public async Task post([FromBody]ExistenciaText existencia)
        {
            await repository.Insert(existencia);
        }
        [HttpPut]
        public async Task put([FromBody] Pedidos pedidos)
        {
            await repository.Pedidos(pedidos);
        }
    }
}
