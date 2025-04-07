using BackAppPersonal.Application.DTO.InputDto;
using BackAppPersonal.Application.DTO.OuputDto;
using BackAppPersonal.Application.Services;
using BackAppPersonal.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BackAppPersonal.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AcademiaPersonalController : ControllerBase
    {
        private readonly AcademiaPersonalService _academiaPersonalService;

        public AcademiaPersonalController(AcademiaPersonalService academiaPersonalService)
        {
            _academiaPersonalService = academiaPersonalService;
        }

        [HttpGet]
        public async Task<IEnumerable<AcademiaPersonalOutput>> AcademiaPersonals()
        {
            return await _academiaPersonalService.AcademiaPersonals();
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<AcademiaPersonalOutput> AcademiaPersonalPorId(Guid id)
        {
            return await _academiaPersonalService.AcademiaPersonalPorId(id);
        }

        [HttpPost]
        public async Task<AcademiaPersonal> CriarAcademiaPersonal(AcademiaPersonalInput academiaPersonal)
        {
            return await _academiaPersonalService.CriarAcademiaPersonal(academiaPersonal);
        }

        [HttpPut]
        [Authorize]
        public async Task<AcademiaPersonalOutput> AtualizarAcademiaPersonal(AcademiaPersonalInput academiaPersonal)
        {
            return await _academiaPersonalService.AtualizarAcademiaPersonal(academiaPersonal);
        }

        [HttpDelete]
        [Authorize]
        public async Task<AcademiaPersonalOutput> DeletarAcademiaPersonal(Guid id)
        {
            return await _academiaPersonalService.DeletarAcademiaPersonal(id);
        }

        [HttpGet]
        [Route("academia/{id}")]
        public async Task<IActionResult> FiltrarPorAcademia(Guid id)
        {
            try
            {
                var retorno = await _academiaPersonalService.AcademiaPersonalPorAcademia(id);
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
