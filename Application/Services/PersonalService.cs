using BackAppPersonal.Application.DTO.InputDto;
using BackAppPersonal.Application.Map;
using BackAppPersonal.Domain.Entities;
using BackAppPersonal.Domain.Intefaces;

namespace BackAppPersonal.Application.Services
{
    public class PersonalService
    {
        private readonly IPersonalRepository _personalRepository;

        public PersonalService(IPersonalRepository personalRepository)
        {
            _personalRepository = personalRepository;
        }

        public async Task<IEnumerable<Personal>> Personals()
        {
            return await _personalRepository.Personals();
        }

        public async Task<Personal> PersonalPorId(Guid id)
        {
            return await _personalRepository.PersonalPorId(id);
        }

        public async Task<Personal> CriarPersonal(PersonalInput personal)
        {
            Personal map = PersonalMap.MapPersonal(personal);
            return await _personalRepository.CriarPersonal(map);
        }

        public async Task<Personal> AtualizarPersonal(PersonalInput personal)
        {
            Personal map = PersonalMap.MapPersonal(personal);
            return await _personalRepository.AtualizarPersonal(map);
        }

        public async Task<Personal> DeletarPersonal(Guid id)
        {
            return await _personalRepository.DeletarPersonal(id);
        }
    }
}
