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
    [Route("api/logIn")]
    public class UsuarioLogInController : Controller
    {
        private IDbService_LogIn _servicioBD;

        public UsuarioLogInController(IDbService_LogIn servicioBD)
        {
            _servicioBD = servicioBD;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> RegisterUser([FromBody] Usuario_LogIn modelo)
        {
            var result = await _servicioBD.RegisterUser(modelo);
            return Ok(result);
        }
    }
}
