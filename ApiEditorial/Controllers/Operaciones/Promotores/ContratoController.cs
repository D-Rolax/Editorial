﻿using ApiEditorial.Data.Operaciones.Promotores;
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
    }
}
