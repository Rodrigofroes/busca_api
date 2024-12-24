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

        public async Task<Personal> CriarPersonal(Personal personal)
        {
            return await _personalRepository.CriarPersonal(personal);
        }

        public async Task<Personal> AtualizarPersonal(Personal personal)
        {
            return await _personalRepository.AtualizarPersonal(personal);
        }

        public async Task<Personal> DeletarPersonal(Guid id)
        {
            return await _personalRepository.DeletarPersonal(id);
        }
    }
}
