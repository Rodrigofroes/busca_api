using BackAppPersonal.Application.DTO.InputDto;
using BackAppPersonal.Application.DTO.OuputDto;
using BackAppPersonal.Domain.Entities;
using static BackAppPersonal.Domain.Entities.TipoUsuario;

namespace BackAppPersonal.Application.Map
{
    public static class UsuarioMap
    {
        public static UsuarioOutput MapUsuario(this Usuario usuario)
        {
            return new UsuarioOutput
            {
                Id = usuario.Id,
                Email = usuario.Email,
                Personal = usuario.Personal != null ? PersonalMap.MapPersonal(usuario.Personal) : null,
                Academia = usuario.Academia != null ? AcademiaMap.MapAcademia(usuario.Academia) : null,
                Aluno = usuario.Aluno != null ? AlunoMap.MapAluno(usuario.Aluno) : null,
                Tipo = usuario.Tipo.ToString()
            };
        }

        public static IEnumerable<UsuarioOutput> MapUsuario(this IEnumerable<Usuario> usuario)
        {
            return usuario.Select(x => x.MapUsuario()).ToList();
        }

        public static Usuario MapUsuario(this UsuarioInput usuario)
        {
            return new Usuario
            {
                Email = usuario.Email,
                Senha = usuario.Senha,
                PersonalId = usuario.Personal?.Id,
                AcademiaId = usuario.Academia?.Id,
                Academia = usuario.Academia != null ? AcademiaMap.MapAcademia(usuario.Academia) : null,
                Personal = usuario.Personal != null ? PersonalMap.MapPersonal(usuario.Personal) : null,
                Aluno = usuario.Aluno != null ? AlunoMap.MapAluno(usuario.Aluno) : null,
                Tipo = Enum.Parse<TipoUsuarioEnum>(usuario.Tipo)
            };
        }

        public static List<Usuario> MapUsuario(this List<UsuarioInput> usuario)
        {
            return usuario.Select(x => x.MapUsuario()).ToList();
        }
    }
}
