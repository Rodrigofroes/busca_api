using BackAppPersonal.Application.DTO.InputDto;
using BackAppPersonal.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace BackAppPersonal.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;
        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AuthInput auth)
        {
            try
            {
                var retorno = await _authService.Login(auth);
                return Ok(retorno);
            }
            catch (Exception e)
            {
                return BadRequest(new
                {
                    message = e.Message
                });
            }
        }
    }
}
