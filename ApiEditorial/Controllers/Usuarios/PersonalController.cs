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
    public class PersonalController : ControllerBase
    {
        private readonly PersonalRepository repository;

        public PersonalController(PersonalRepository repository)
        {
            this.repository = repository?? throw new ArgumentException(nameof(repository));
        }
        [HttpPost]
        public async Task post([FromBody]Personal personal)
        {
            await repository.Insert(personal);
        }
    }
}
