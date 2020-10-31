using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.models;
using DemoApiUsers.services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DemoApiUsers.Controllers
{
    [Route("api/servicios")]
    [ApiController]
    public class ServiciosController : ControllerBase
    {
        private IDbService_Services _servicioBD;

        public ServiciosController(IDbService_Services servicioBD)
        {
            _servicioBD = servicioBD;
        }

        [HttpGet]
        [Route("obtenerPorTipo")]
        public async Task<IActionResult> ObtenerPorTipo(string tipo)
        {
            var result = await _servicioBD.ObtenerPorTipo(tipo);
            return Ok(result);
        }

        [HttpGet]
        [Route("historial")]
        public async Task<IActionResult> ObtenerHistorial(int idUsuario)
        {
            var result = await _servicioBD.ObtenerHistorial(idUsuario);
            return Ok(result);
        }

        [HttpGet]
        [Route("citasPendientes")]
        public async Task<IActionResult> ObtenerCitasPendientes(int idUsuario)
        {
            var result = await _servicioBD.ObtenerCitasPendientes(idUsuario);
            return Ok(result);
        }

        [HttpDelete]
        [Route("cancelarCita")]
        public async Task<IActionResult> CancelarCita(int idCita)
        {
            var result = await _servicioBD.CancelarCita(idCita);
            return Ok(result);
        }

        [HttpPost]
        [Route("agregarCita")]
        public async Task<IActionResult> AgregarCita([FromBody] Cita cita)
        {
            var result = await _servicioBD.AgregarCita(cita);
            return Ok(result);
        }
    }
}
