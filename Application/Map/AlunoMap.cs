using BackAppPersonal.Application.DTO.InputDto;
using BackAppPersonal.Application.DTO.OuputDto;
using BackAppPersonal.Domain.Entities;

namespace BackAppPersonal.Application.Map
{
    public static class AlunoMap
    {
        public static AlunoOutput MapAluno(this Aluno aluno)
        {
            return new AlunoOutput
            {
                Id = aluno.Id,
                Nome = aluno.Nome,
                Sobrenome = aluno.Sobrenome,
                Personal = aluno.Personal != null ? PersonalMap.MapPersonal(aluno.Personal) : null
            };
        }

        public static IEnumerable<AlunoOutput> MapAluno(this IEnumerable<Aluno> aluno)
        {
            return aluno.Select(x => x.MapAluno()).ToList();
        }

        public static Aluno MapAluno(this AlunoInput aluno)
        {
            return new Aluno
            {
                Id = (Guid)aluno.Id,
                Nome = aluno.Nome,
                Sobrenome = aluno.Sobrenome,
                PersonalId = aluno.PersonalId != null ? aluno.PersonalId : null,
            };
        }

        public static Aluno MapAluno(this AlunoOutput aluno)
        {
            return new Aluno
            {
                Id = aluno.Id,
                Nome = aluno.Nome,
                Sobrenome = aluno.Sobrenome,
                PersonalId = aluno.Personal.Id,
            };
        }

        public static IEnumerable<Aluno> MapAluno(this IEnumerable<AlunoOutput> aluno)
        {
            return aluno.Select(x => x.MapAluno()).ToList();
        }
    }
}
