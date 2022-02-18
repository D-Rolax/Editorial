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
    public class ColegioController : ControllerBase
    {
        private readonly ColegioRepository _repository;

        public ColegioController(ColegioRepository repository)
        {
            _repository = repository?? throw new ArgumentException(nameof(_repository));
        }
        [HttpGet]
        public async Task<List<Colegio>> Get()
        {
            return await _repository.Mostrar();
        }
        [HttpPost]
        public async Task RegistrarColegio(Colegio colegio)
        {
            await _repository.insert(colegio);
        }
    }
}
