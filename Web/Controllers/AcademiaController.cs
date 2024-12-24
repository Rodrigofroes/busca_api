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
        public async Task<Academia> Post([FromBody] AcademiaInput academia)
        {
            return await _academiaService.CriarAcademia(academia);
        }

        [HttpPut]
        public async Task<Academia> Put([FromBody] AcademiaInput academia)
        {
            return await _academiaService.AtualizarAcademia(academia);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<Academia> Delete(Guid id)
        {
            return await _academiaService.DeletarAcademia(id);
        }
    }
}
