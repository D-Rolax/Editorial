using ApiEditorial.Data.Operaciones.Promotores;
using ApiEditorial.Models.Usuarios;
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
    public class ClienteController : ControllerBase
    {
        private readonly ClienteRepository repository;

        public ClienteController(ClienteRepository _repository)
        {
            repository = _repository?? throw new ArgumentException(nameof(repository));
        }
        [HttpGet]
        public async Task<List<Cliente>> Get()
        {
            return await repository.MostrarCliente();
        }
        [HttpPost]
        public async Task NuevoCliente(Cliente cliente)
        {
            await repository.Insertar(cliente);
        }
    }
}
