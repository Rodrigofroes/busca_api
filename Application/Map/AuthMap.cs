using BackAppPersonal.Application.DTO.InputDto;
using BackAppPersonal.Application.DTO.OuputDto;
using BackAppPersonal.Domain.Entities;

namespace BackAppPersonal.Application.Map
{
    public static class AuthMap
    {
        public static AuthOutput MapAuth(this Usuario usuario, string token)
        {
            return new AuthOutput
            {
                Token = token,
                Usuario = UsuarioMap.MapUsuario(usuario)
            };
        }

        public static IEnumerable<AuthOutput> MapAuth(this IEnumerable<Usuario> usuarios, string token)
        {
            return usuarios.Select(x => x.MapAuth(token)).ToList();
        }
    }
}
