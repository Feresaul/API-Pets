using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DemoApiUsers.models;
using DemoApiUsers.services;
using Microsoft.AspNetCore.Mvc;

namespace DemoApiUsers.Controllers
{
    [Produces("application/json")]
    [Route("api/Admin")]
    public class UsuarioController : Controller
    {
        private IDbService _servicioBD;

        public UsuarioController(IDbService servicioBD)
        {
            _servicioBD = servicioBD;
        }

        [HttpGet]
        [Route("ObtenerUsuarios")]
        public async Task<IActionResult> ObtenerUsuarios()
        {
            var result = await _servicioBD.ObtenerUsuario();
            return Ok(result);
        }

        [HttpGet]
        [Route("ObtenerUsuarioPorId")]
        public async Task<IActionResult> ObtenerUsuarioPorId(int id)
        {
            var result = await _servicioBD.ObtenerUsuarioPorId(id);
            return Ok(result);
        }

        [HttpDelete]
        [Route("BorrarUsuario")]
        public async Task<IActionResult> BorrarUsuario(int id)
        {
            var result = await _servicioBD.EliminarUsuario(id);
            return Ok(result);
        }

        [HttpPost]
        [Route("AgregarUsuario")]
        public async Task<IActionResult> AgregarUsuario([FromBody] Usuario modelo)
        {
            var result = await _servicioBD.GuardarUsuario(modelo);
            return Ok(result);
        }
    }
}
