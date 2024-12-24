using BackAppPersonal.Domain.Entities;

namespace BackAppPersonal.Domain.Intefaces
{
    public interface IPersonalRepository
    {
        Task<IEnumerable<Personal>> Personals();
        Task<Personal> PersonalPorId(Guid id);
        Task<Personal> CriarPersonal(Personal personal);
        Task<Personal> AtualizarPersonal(Personal personal);
        Task<Personal> DeletarPersonal(Guid id);
    }
}