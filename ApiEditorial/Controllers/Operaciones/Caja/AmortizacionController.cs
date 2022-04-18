using ApiEditorial.Data.Operaciones.Caja;
using ApiEditorial.Models.Caja;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiEditorial.Controllers.Operaciones.Caja
{
    [Route("api/[controller]")]
    [ApiController]
    public class AmortizacionController : ControllerBase
    {
        private readonly AmortizacionRepository _repository;

        public AmortizacionController(AmortizacionRepository repository)
        {
            _repository = repository?? throw new ArgumentException(nameof(repository));
        }
        [HttpPost]
        public async Task Post([FromBody]Amortizacion amortizacion)
        {
            await _repository.Insert(amortizacion);
        }
        [HttpGet]
        public async Task<List<Amortizacion>> Get()
        {
            return await _repository.GetAll();
        }
        [HttpGet]
        [Route("{Id}/Detalle")]
        public async Task<List<DetalleAmortizacion>> GetDetalle(int Id)
        {
            return await _repository.GetDetalle(Id);
        }
    }
}
