using ApiEditorial.Data.Operaciones.Inventario;
using ApiEditorial.Models.Inventario;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiEditorial.Controllers.Operaciones.Inventario
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlmacenController : ControllerBase
    {
        private readonly AlmacenRepository _repository;
        public AlmacenController(AlmacenRepository repository)
        {
            _repository = repository ?? throw new ArgumentException(nameof(repository));
        }
        [HttpGet]
        public async Task<List<Almacen>> ListarAlmacen()
        {
            return await _repository.ListarAlmacen();
        }
        [HttpPost]
        public async Task Post([FromBody] Almacen valor)
        {
            await _repository.Insert(valor);
        }
        [HttpPut]
        public async Task Put ([FromBody] Almacen valor)
        {
            await _repository.Update(valor);
        }
    }
}
