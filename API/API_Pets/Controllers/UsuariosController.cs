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

        [HttpPost]
        [Route("registerEmployee")]
        public async Task<IActionResult> RegisterEmployee([FromBody] Usuario usuario)
        {
            var result = await _servicioBD.registerEmployee(usuario);
            return Ok(result);
        }

        [HttpPost]
        [Route("updateUser")]
        public async Task<IActionResult> UpdateUser([FromBody] Usuario user)
        {
            var result = await _servicioBD.updateUser(user);
            return Ok(result);
        }

        [HttpPost]
        [Route("deleteUser")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var result = await _servicioBD.deleteUser(id);
            return Ok(result);
        }

        [HttpPost]
        [Route("getUser")]
        public async Task<IActionResult> GetUser(int id)
        {
            var result = await _servicioBD.getUser(id);
            return Ok(result);
        }

        [HttpPost]
        [Route("getUsers")]
        public async Task<IActionResult> GetUsers(int id)
        {
            var result = await _servicioBD.getUsers(id);
            return Ok(result);
        }

    }
}
