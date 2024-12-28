using BackAppPersonal.Application.DTO.InputDto;
using BackAppPersonal.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace BackAppPersonal.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonalController : ControllerBase
    {
        private readonly PersonalService _personalService;

        public PersonalController(PersonalService personalService)
        {
            _personalService = personalService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _personalService.Personals());
        }

        [HttpPost]
        public async Task<IActionResult> Post(PersonalInput personal)
        {
            try
            {
                var retorno = await _personalService.CriarPersonal(personal);
                return Ok(retorno);
            }
            catch (Exception ex)
            {
                return BadRequest(new {
                    message = ex.Message
                });
            };
        }

        [HttpPut]
        public async Task<IActionResult> Put(PersonalInput personal)
        {
            return Ok(await _personalService.AtualizarPersonal(personal));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return Ok(await _personalService.DeletarPersonal(id));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            return Ok(await _personalService.PersonalPorId(id));
        }




    }
}
