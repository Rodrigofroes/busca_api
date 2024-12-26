using BackAppPersonal.Application.DTO.InputDto;
using BackAppPersonal.Application.DTO.OuputDto;
using BackAppPersonal.Application.Map;
using BackAppPersonal.Domain.Entities;
using BackAppPersonal.Domain.Intefaces;
using BackAppPersonal.Utils;

namespace BackAppPersonal.Application.Services
{
    public class AuthService
    {
        private readonly IUsuarioRespository _usuarioRespository;
        private readonly IPersonalRepository _personalRepository;
        private readonly IAcademiaRepository _academiaRepository;
        private readonly IJwtToken _jwtToken;
        private readonly ISenhaHash _senhaHash;
        private readonly ValidadorUtils _validadorUtils;

        public AuthService(IUsuarioRespository usuarioRespository, IJwtToken jwtToken, ISenhaHash senhaHash, ValidadorUtils validadorUtils, IPersonalRepository personalRepository, IAcademiaRepository academiaRepository)
        {
            _usuarioRespository = usuarioRespository;
            _jwtToken = jwtToken;
            _senhaHash = senhaHash;
            _validadorUtils = validadorUtils;
            _personalRepository = personalRepository;
            _academiaRepository = academiaRepository;
        }

        public async Task<AuthOutput> Login(AuthInput auth)
        {
            ValidarUsuario(auth);
            Usuario usuario = await _usuarioRespository.UsuarioPorEmail(auth.Email);
            if (usuario != null)
            {
                if (usuario.TipoUsuarioId == Guid.Parse("2fc3f78f-84be-437a-8f00-826b701f4768"))
                {
                    usuario.Personal = await _personalRepository.PersonalPorId((Guid)usuario.PersonalId);
                }
                else
                {
                    usuario.Academia = await _academiaRepository.AcademiaPorId((Guid)usuario.Academia.Id);
                }
            }

            if (usuario == null)
                throw new Exception("Email ou/e Senha inválido");
            if (!_senhaHash.VerificarSenha(usuario.Senha, auth.Senha))
                throw new Exception("Email ou/e Senha inválido");
            var token = _jwtToken.GerarToken(usuario);
            usuario.TipoUsuario = await _usuarioRespository.TipoUsuarioPorId(usuario.TipoUsuarioId);
            return AuthMap.MapAuth(usuario, token);
        }

        private bool ValidarUsuario(AuthInput usuario)
        {
            if (!_validadorUtils.ValidarEmail(usuario.Email))
            {
                throw new Exception("Email ou/e Senha inválido");
            }
            if (!_validadorUtils.ValidarSenha(usuario.Senha))
            {
                throw new Exception("Email ou/e Senha inválido");
            }
            return true;
        }
    }
}
