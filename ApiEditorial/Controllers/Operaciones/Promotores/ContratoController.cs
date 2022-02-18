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
    public class ContratoController : ControllerBase
    {
        private readonly ContratoRepository _repository;
        private readonly ClienteRepository _cliente;
        private readonly ColegioRepository _colegio;

        public ContratoController(ContratoRepository repository, ClienteRepository cliente,ColegioRepository colegio)
        {
            _repository = repository ?? throw new ArgumentException(nameof(repository));
            _cliente = cliente ?? throw new ArgumentException(nameof(cliente));
            _colegio = colegio ?? throw new ArgumentException(nameof(colegio));
        }
        [HttpPost]
        public async Task InsertContrato(Contrato contrato,Cliente cliente)
        {
            await _repository.Insert(contrato);
        }
    }
}
