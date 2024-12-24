using BackAppPersonal.Application.DTO.InputDto;
using BackAppPersonal.Application.DTO.OuputDto;
using BackAppPersonal.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace BackAppPersonal.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AcademiaPersonalController
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
        public async Task<AcademiaPersonalOutput> AcademiaPersonalPorId(Guid id)
        {
            return await _academiaPersonalService.AcademiaPersonalPorId(id);
        }

        [HttpPost]
        public async Task<AcademiaPersonalOutput> CriarAcademiaPersonal(AcademiaPersonalInput academiaPersonal)
        {
            return await _academiaPersonalService.CriarAcademiaPersonal(academiaPersonal);
        }

        [HttpPut]
        public async Task<AcademiaPersonalOutput> AtualizarAcademiaPersonal(AcademiaPersonalInput academiaPersonal)
        {
            return await _academiaPersonalService.AtualizarAcademiaPersonal(academiaPersonal);
        }

        [HttpDelete]
        public async Task<AcademiaPersonalOutput> DeletarAcademiaPersonal(Guid id)
        {
            return await _academiaPersonalService.DeletarAcademiaPersonal(id);
        }
    }
}
