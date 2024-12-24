using BackAppPersonal.Domain.Entities;

namespace BackAppPersonal.Domain.Intefaces
{
    public interface IAcademiaRepository
    {
        Task<IEnumerable<Academia>> Academias();
        Task<Academia> AcademiaPorId(Guid id);
        Task<Academia> CriarAcademia(Academia academia);
        Task<Academia> AtualizarAcademia(Academia academia);
        Task<Academia> DeletarAcademia(Guid id);
    }
}