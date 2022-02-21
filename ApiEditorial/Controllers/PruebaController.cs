using ApiEditorial.Data;
using ApiEditorial.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiEditorial.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PruebaController : ControllerBase
    {
        private readonly PruebaRepository repository;

        public PruebaController(PruebaRepository repository)
        {
            this.repository = repository ?? throw new ArgumentException(nameof(repository));
        }
        [HttpGet]
        public async Task<List<Cabecera>> Get()
        {
            return await repository.List();
        }
    }
}
