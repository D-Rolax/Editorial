using ApiEditorial.Data.Inventario;
using ApiEditorial.Models.Inventario;
using ApiEditorial.Servicios;
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
    public class InventarioController : ControllerBase
    {
        private readonly InventarioInicialRepository _repository;

        public InventarioController(InventarioInicialRepository repository)
        {
            _repository = repository ?? throw new ArgumentException(nameof(repository));
        }
        [HttpPost]
        public async Task Post([FromBody] Inicial valor)
        {
            await _repository.Insert(valor);
            await _repository.Insert(valor);
        }
    }
}
