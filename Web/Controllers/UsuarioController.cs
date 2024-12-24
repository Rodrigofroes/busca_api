using BackAppPersonal.Application.DTO.InputDto;
using BackAppPersonal.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace BackAppPersonal.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly UsuarioService _usuarioService;

        public UsuarioController(UsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UsuarioInput usuario)
        {
            try
            {
                var retorno = await _usuarioService.CriarUsuario(usuario);
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

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _usuarioService.Usuarios());
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            return Ok(await _usuarioService.UsuarioPorId(id));
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            return Ok(await _usuarioService.DeletarUsuario(id));
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UsuarioInput usuario)
        {
            try
            {
                var retorno = await _usuarioService.AtualizarUsuario(usuario);
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
