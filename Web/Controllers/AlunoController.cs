using BackAppPersonal.Application.DTO.InputDto;
using BackAppPersonal.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace BackAppPersonal.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlunoController : ControllerBase
    {
        private readonly AlunoService _alunoService;

        public AlunoController(AlunoService alunoService)
        {
            _alunoService = alunoService;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AlunoInput aluno)
        {
            try
            {
                var retorno = await _alunoService.CriarAluno(aluno);
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
            return Ok(await _alunoService.Alunos());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            return Ok(await _alunoService.AlunosPorId(id));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            return Ok(await _alunoService.DeletarAluno(id));
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] AlunoInput aluno)
        {
            return Ok(await _alunoService.AtualizarAluno(aluno));
        }

    }
}
