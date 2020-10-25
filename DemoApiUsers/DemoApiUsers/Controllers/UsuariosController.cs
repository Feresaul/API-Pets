using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API_Pets.models;
using API_Pets.services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_Pets.Controllers
{
    [Route("api/usuarios")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private IDbService_Usuarios _servicioBD;

        public UsuariosController(IDbService_Usuarios servicioBD)
        {
            _servicioBD = servicioBD;
        }

        [HttpPost]
        [Route("logIn")]
        public async Task<IActionResult> LogIn([FromBody] Usuario usuario)
        {
            var result = await _servicioBD.logIn(usuario);
            return Ok(result);
        }

        [HttpPost]
        [Route("registro")]
        public async Task<IActionResult> Registro([FromBody] Usuario usuario)
        {
            var result = await _servicioBD.registro(usuario);
            return Ok(result);
        }
    }
}
