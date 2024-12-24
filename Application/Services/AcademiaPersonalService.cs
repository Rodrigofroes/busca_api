using BackAppPersonal.Application.DTO.InputDto;
using BackAppPersonal.Application.DTO.OuputDto;
using BackAppPersonal.Application.Map;
using BackAppPersonal.Domain.Entities;
using BackAppPersonal.Domain.Intefaces;
using BackAppPersonal.Infrastructure.Repository;

namespace BackAppPersonal.Application.Services
{
    public class AcademiaPersonalService
    {
        private readonly IAcademiaPersonalRepository _academiaPersonalRepository;
        private readonly IAcademiaRepository _academiaRepository;
        private readonly IPersonalRepository _personalRepository;

        public AcademiaPersonalService(IAcademiaPersonalRepository academiaPersonalRepository, IAcademiaRepository academiaRepository, IPersonalRepository personalRepository)
        {
            _academiaPersonalRepository = academiaPersonalRepository;
            _academiaRepository = academiaRepository;
            _personalRepository = personalRepository;
        }

        public async Task<IEnumerable<AcademiaPersonalOutput>> AcademiaPersonals()
        {
            IEnumerable<AcademiaPersonal> academiaPersonals = await _academiaPersonalRepository.AcademiaPersonals();

            foreach (var academiaPersonal in academiaPersonals)
            {
                academiaPersonal.Academia = await _academiaRepository.AcademiaPorId(academiaPersonal.AcademiaId);
                academiaPersonal.Personal = await _personalRepository.PersonalPorId(academiaPersonal.PersonalId);
            }

            return AcademiaPersonalMap.MapAcademiaPersonal(academiaPersonals);
        }

        public async Task<AcademiaPersonalOutput> AcademiaPersonalPorId(Guid id)
        {
            AcademiaPersonal academiaPersonal = await _academiaPersonalRepository.AcademiaPersonalPorId(id);
            academiaPersonal.Academia = await _academiaRepository.AcademiaPorId(academiaPersonal.AcademiaId);
            academiaPersonal.Personal = await _personalRepository.PersonalPorId(academiaPersonal.PersonalId);
            return AcademiaPersonalMap.MapAcademiaPersonal(academiaPersonal);
        }

        public async Task<AcademiaPersonalOutput> CriarAcademiaPersonal(AcademiaPersonalInput academiaPersonal)
        {
            AcademiaPersonal retorno = AcademiaPersonalMap.MapAcademiaPersonal(academiaPersonal);
            retorno = await _academiaPersonalRepository.CriarAcademiaPersonal(retorno);
            return AcademiaPersonalMap.MapAcademiaPersonal(retorno);
        }

        public async Task<AcademiaPersonalOutput> AtualizarAcademiaPersonal(AcademiaPersonalInput academiaPersonal)
        {
            AcademiaPersonal retorno = AcademiaPersonalMap.MapAcademiaPersonal(academiaPersonal);
            retorno = await _academiaPersonalRepository.AtualizarAcademiaPersonal(retorno);
            return AcademiaPersonalMap.MapAcademiaPersonal(retorno);
        }

        public async Task<AcademiaPersonalOutput> DeletarAcademiaPersonal(Guid id)
        {
            AcademiaPersonal retorno = await _academiaPersonalRepository.DeletarAcademiaPersonal(id);
            return AcademiaPersonalMap.MapAcademiaPersonal(retorno);
        }


    }
}
