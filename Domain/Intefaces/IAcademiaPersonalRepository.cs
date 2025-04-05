using BackAppPersonal.Domain.Entities;

namespace BackAppPersonal.Domain.Intefaces
{
    public interface IAcademiaPersonalRepository
    {
        Task<IEnumerable<AcademiaPersonal>> AcademiaPersonals();
        Task<AcademiaPersonal> AcademiaPersonalPorId(Guid id);
        Task<AcademiaPersonal> CriarAcademiaPersonal(AcademiaPersonal academiaPersonal);
        Task<AcademiaPersonal> AtualizarAcademiaPersonal(AcademiaPersonal academiaPersonal);
        Task<AcademiaPersonal> DeletarAcademiaPersonal(Guid id);
        Task<IEnumerable<AcademiaPersonal>> AcademiaPersonalPorAcademia(Guid id);
    }
}
