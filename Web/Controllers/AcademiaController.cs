using BackAppPersonal.Application.DTO.InputDto;
using BackAppPersonal.Application.DTO.OuputDto;
using BackAppPersonal.Application.Services;
using BackAppPersonal.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace BackAppPersonal.Web.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class AcademiaController : ControllerBase
    {
        private readonly AcademiaService _academiaService;

        public AcademiaController(AcademiaService academiaSerivce)
        {
            _academiaService = academiaSerivce;
        }

        [HttpGet]
        public async Task<IEnumerable<AcademiaOutput>> Get()
        {
            return await _academiaService.Academias();
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<AcademiaOutput> Get(Guid id)
        {
            return await _academiaService.AcademiaPorId(id);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AcademiaInput academia)
        {
            try
            {
                var result = await _academiaService.CriarAcademia(academia);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(new
                {
                    message = e.Message
                });
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] AcademiaInput academia)
        {
            try
            {
                var result = await _academiaService.AtualizarAcademia(academia);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(new
                {
                    message = e.Message
                });
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var result = await _academiaService.DeletarAcademia(id);
                return Ok(result);
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
        [Route("nome/{nome}")]
        public async Task<IActionResult> FiltrarPorAcademiaNome(string nome)
        {
            try
            {
                var retorno = await _academiaService.AcademiaPersonalPorAcademiaNome(nome);
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
