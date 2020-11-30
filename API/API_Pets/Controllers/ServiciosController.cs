using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.models;
using DemoApiUsers.models;
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

        [HttpPost]
        [Route("obtenerHoras")]
        public async Task<IActionResult> ObtenerHoras([FromBody] ServicioHoras servicio)
        {
            var result = await _servicioBD.ObtenerHoras(servicio);
            return Ok(result);
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

        [HttpPost]
        [Route("updateService")]
        public async Task<IActionResult> UpdateService([FromBody] DetailedService service)
        {
            var result = await _servicioBD.updateService(service);
            return Ok(result);
        }

        [HttpPost]
        [Route("addService")]
        public async Task<IActionResult> AddService([FromBody] DetailedService service)
        {
            var result = await _servicioBD.addService(service);
            return Ok(result);
        }

        [HttpPost]
        [Route("deleteService")]
        public async Task<IActionResult> DeleteService(int id)
        {
            var result = await _servicioBD.deleteService(id);
            return Ok(result);
        }

        [HttpGet]
        [Route("getServices")]
        public async Task<IActionResult> GetServices()
        {
            var result = await _servicioBD.getServices();
            return Ok(result);
        }

        [HttpGet]
        [Route("getSales")]
        public async Task<IActionResult> GetSales()
        {
            var result = await _servicioBD.getSales();
            return Ok(result);
        }

        [HttpPost]
        [Route("getSalesPerDate")]
        public async Task<IActionResult> GetSalesPerDate(string start, string end)
        {
            var result = await _servicioBD.getSalesPerDate(start, end);
            return Ok(result);
        }
    }
}
