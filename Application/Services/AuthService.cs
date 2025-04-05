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
        private readonly IEnderecoRepository _enderecoRepository;
        private readonly IAlunoRepository _alunoRepository;
        private readonly IJwtToken _jwtToken;
        private readonly ISenhaHash _senhaHash;
        private readonly ValidadorUtils _validadorUtils;

        public AuthService(IUsuarioRespository usuarioRespository, IJwtToken jwtToken, ISenhaHash senhaHash, ValidadorUtils validadorUtils, IPersonalRepository personalRepository, IAcademiaRepository academiaRepository, IAlunoRepository alunoRepository, IEnderecoRepository enderecoRepository)
        {
            _usuarioRespository = usuarioRespository;
            _jwtToken = jwtToken;
            _senhaHash = senhaHash;
            _validadorUtils = validadorUtils;
            _personalRepository = personalRepository;
            _academiaRepository = academiaRepository;
            _alunoRepository = alunoRepository;
            _enderecoRepository = enderecoRepository;
        }

        public async Task<AuthOutput> Login(AuthInput auth)
        {
            ValidarUsuario(auth);
            Usuario usuario = await _usuarioRespository.UsuarioPorEmail(auth.Email);
            if (usuario != null)
            {
                if (usuario.Tipo == TipoUsuario.TipoUsuarioEnum.Personal)
                {
                    usuario.Personal = await _personalRepository.PersonalPorId((Guid)usuario.PersonalId);
                }
                else if (usuario.Tipo == TipoUsuario.TipoUsuarioEnum.Academia)
                {
                    usuario.Academia = await _academiaRepository.AcademiaPorId((Guid)usuario.AcademiaId);
                    usuario.Academia.Endereco = await _enderecoRepository.EnderecoPorId((Guid)usuario.Academia.EnderecoId);
                }
                else
                {
                    usuario.Aluno = await _alunoRepository.AlunosPorId((Guid)usuario.AlunoId);
                }

                var token = _jwtToken.GerarToken(usuario);
                return AuthMap.MapAuth(usuario, token);
            }

            throw new Exception("Email ou/e Senha inválido");
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
