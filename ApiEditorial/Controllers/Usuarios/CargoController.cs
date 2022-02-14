using ApiEditorial.Data.Usuarios;
using ApiEditorial.Models.Usuarios;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiEditorial.Controllers.Usuarios
{
    [Route("api/[controller]")]
    [ApiController]
    public class CargoController : ControllerBase
    {
        private readonly CargoRepository repository;

        public CargoController(CargoRepository repository)
        {
            this.repository = repository?? throw new ArgumentException(nameof(repository));
        }
        [HttpPost]
        public async Task Post([FromBody]Cargo cargo)
        {
            await repository.Insert(cargo);
        }
    }
}
