using ApiEditorial.Data.Inventario;
using ApiEditorial.Models;
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
        }
        [HttpGet]
        public async Task<List<Libros>> Get()
        {
            return await _repository.GetAll();
        }
        [HttpPut]
        public async Task Put([FromBody] ExistenciaAlm valor)
        {
            await _repository.Ajustes(valor);
        }
    }
}
