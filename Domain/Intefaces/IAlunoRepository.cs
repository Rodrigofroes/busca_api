using BackAppPersonal.Domain.Entities;

namespace BackAppPersonal.Domain.Intefaces
{
    public interface IAlunoRepository
    {
        Task<IEnumerable<Aluno>> Alunos();
        Task<Aluno> AlunosPorId(Guid id);
        Task<Aluno> CriarAluno(Aluno usuario);
        Task<Aluno> AtualizarAluno(Aluno usuario);
        Task<Aluno> DeletarAluno(Guid id);
    }
}
