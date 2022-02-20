using ApiEditorial.Data.Operaciones.Promotores;
using ApiEditorial.Models;
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

        public ContratoController(ContratoRepository repository)
        {
            _repository = repository ?? throw new ArgumentException(nameof(repository));
        }
        [HttpPost]
        public async Task InsertContrato(Contrato contrato)
        {
            await _repository.Insert(contrato);
        }
        [HttpGet("{Id}")]
        public async Task<List<Contrato>>get(int Id)
        {
            return await _repository.Mostrar(Id);
        }
        [HttpGet]
        [Route("{Id}/Detalle")]
        public async Task<List<DetalleTextos>> getDetall(int Id)
        {
            return await _repository.MostrarDetalle(Id);
        }
        [HttpGet]
        [Route("NumContrato")]
        public async Task<List<Contrato>> GetNumCon()
        {
            return await _repository.VerContrato();
        }
    }
}
