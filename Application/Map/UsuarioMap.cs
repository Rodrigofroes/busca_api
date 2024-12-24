using BackAppPersonal.Application.DTO.InputDto;
using BackAppPersonal.Application.DTO.OuputDto;
using BackAppPersonal.Domain.Entities;

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
                TipoUsuario = TipoUsuarioMap.MapTipoUsuario(usuario.TipoUsuario)
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
                Id = usuario.Id,
                Email = usuario.Email,
                Senha = usuario.Senha,
                PersonalId = usuario.Personal?.Id,
                AcademiaId = usuario.Academia?.Id,
                Academia = usuario.Academia != null ? AcademiaMap.MapAcademia(usuario.Academia) : null,
                Personal = usuario.Personal != null ? PersonalMap.MapPersonal(usuario.Personal) : null,
                TipoUsuarioId = usuario.TipoUsuario

            };
        }

        public static List<Usuario> MapUsuario(this List<UsuarioInput> usuario)
        {
            return usuario.Select(x => x.MapUsuario()).ToList();
        }
    }
}
