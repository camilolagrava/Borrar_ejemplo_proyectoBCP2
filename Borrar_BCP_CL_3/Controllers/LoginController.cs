using Borrar_BCP_CL_3.Models.Dto;
using Borrar_BCP_CL_3.Service.Iterfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Borrar_BCP_CL_3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IAuthService _authService;

        public LoginController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDto formulario)
        {
            var t = await _authService.Login(formulario);
            /*if (t == null)
            {
                return Unauthorized();
            }*/
            return Ok(t);
        }


    }
}
