﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiEditorial.Models.Usuarios
{
    public class Contrato
    {
        public int idContrato { get; set; }
        public int NumContrato { get; set; }
        public DateTime Fecha { get; set; }
        public int IdCliente { get; set; }
        public int IdPersonal { get; set; }
        public int TotalTextos { get; set; }
        public decimal TotalDeuda { get; set; }
        public decimal Saldo { get; set; }
        public DateTime FechaRegistro { get; set; }
        public string Estado { get; set; }
        public int LibroGuia { get; set; }
        public string NombreCompleto { get; set; }
        public string Nombre { get; set; }
        public int Celular { get; set; }
        public int Ci { get; set; }
        public int IdColegio { get; set; }
        public string Zona { get; set; }
        public List<DetalleTextos> DetalleTextos{ get; set; }
    }
}
