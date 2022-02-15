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
    public class LibrosController : ControllerBase
    {
        //private readonly DataContext _context;
        private readonly LibrosRepository _repository;

        public LibrosController(LibrosRepository repository)
        {
            _repository = repository ?? throw new ArgumentException(nameof(repository));
        }
    
        [HttpGet]
        public async Task<List<Libros>>Get()
        {
            return await _repository.GetAll();
        }

        [HttpGet]
        [Route("ListarAlmacen")]
        public async Task<List<Almacen>> ListarAlmacen()
        {
            return await _repository.ListarAlmacen();
        }
        [HttpPost]
        public async Task Post([FromBody] Libros libros)
        {
            await _repository.Insert(libros);
        }
        [HttpPut]
        public async Task Put([FromBody] Libros libros)
        {
            await _repository.update(libros);
        }
    }
}
