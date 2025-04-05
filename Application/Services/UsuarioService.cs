using System.ComponentModel.DataAnnotations;
using BackAppPersonal.Application.DTO.InputDto;
using BackAppPersonal.Application.DTO.OuputDto;
using BackAppPersonal.Application.Map;
using BackAppPersonal.Domain.Entities;
using BackAppPersonal.Domain.Exceptions;
using BackAppPersonal.Domain.Intefaces;
using BackAppPersonal.Utils;
using System.Transactions;

namespace BackAppPersonal.Application.Services
{
    public class UsuarioService
    {
        private readonly IUsuarioRespository _usuarioRepository;
        private readonly IPersonalRepository _personalRepository;
        private readonly IAcademiaRepository _academiaRepository;
        private readonly IAlunoRepository _alunoRepository;
        public readonly IEnderecoRepository _enderecoRepository;
        public readonly EnderecoService _enderecoService;
        public readonly IMinioStorage _minioStorage;
        private readonly ISenhaHash _senhaHash;
        private readonly ValidadorUtils _validadorUtils;

        public UsuarioService(IUsuarioRespository usuarioRepository, IPersonalRepository personalRepository, ValidadorUtils validadorUtils, ISenhaHash senhaHash, IAcademiaRepository academiaRepository, IEnderecoRepository enderecoRepository, IMinioStorage minioStorage, IAlunoRepository alunoRepository, EnderecoService enderecoService)
        {
            _personalRepository = personalRepository;
            _usuarioRepository = usuarioRepository;
            _validadorUtils = validadorUtils;
            _senhaHash = senhaHash;
            _academiaRepository = academiaRepository;
            _enderecoRepository = enderecoRepository;
            _minioStorage = minioStorage;
            _alunoRepository = alunoRepository;
            _enderecoService = enderecoService;
        }

        public async Task<IEnumerable<UsuarioOutput>> Usuarios()
        {
            IEnumerable<Usuario> usuarios = await _usuarioRepository.Usuarios();

            foreach (var usuario in usuarios)
            {
                if (usuario.PersonalId != null)
                {
                    usuario.Personal = await _personalRepository.PersonalPorId((Guid)usuario.PersonalId);
                }
                else if (usuario.AlunoId != null)
                {
                    usuario.Aluno = await _alunoRepository.AlunosPorId((Guid)usuario.AlunoId);
                    if(usuario.Aluno.PersonalId != null)
                    {
                        usuario.Aluno.Personal = await _personalRepository.PersonalPorId((Guid)usuario.Aluno.PersonalId);
                    }
                }
                else
                {
                    usuario.Academia = await _academiaRepository.AcademiaPorId((Guid)usuario.AcademiaId);
                    usuario.Academia.Endereco = await _enderecoRepository.EnderecoPorId((Guid)usuario.Academia.EnderecoId);
                }
            }
            return UsuarioMap.MapUsuario(usuarios);
        }

        public async Task<UsuarioOutput> UsuarioPorId(Guid id)
        {
            Usuario usuario = await _usuarioRepository.UsuarioPorId(id);
            usuario.Personal = await _personalRepository.PersonalPorId((Guid)usuario.PersonalId);
            return UsuarioMap.MapUsuario(usuario);
        }

        public async Task<UsuarioOutput> CriarUsuario(UsuarioInput usuario)
        {
            ValidarCadastro(usuario);
            Usuario map = UsuarioMap.MapUsuario(usuario);
            string path = "";

            if (map.Tipo == TipoUsuario.TipoUsuarioEnum.Personal)
            {
                ValidarDadosPersonal(usuario);
                path = "personal";
                string url = await UploadImagem(usuario.Personal.Foto, path);
                map.Personal.Url = url;
                Personal personal = await _personalRepository.CriarPersonal(map.Personal);
                map.PersonalId = personal.Id;
            }
            if (map.Tipo == TipoUsuario.TipoUsuarioEnum.Academia)
            {
                ValidarDadosAcademia(usuario);
                path = "academia";
                string url = await UploadImagem(usuario.Academia.Foto, path);
                map.Academia.Url = url;
                Endereco endereco = await _enderecoService.CriarEndereco(map.Academia.Endereco);
                map.Academia.EnderecoId = endereco.Id;
                Academia academia = await _academiaRepository.CriarAcademia(map.Academia);
                map.AcademiaId = academia.Id;
            }
            if (map.Tipo == TipoUsuario.TipoUsuarioEnum.Aluno)
            {
                ValidarAluno(usuario);
                path = "aluno";
                string url = await UploadImagem(usuario.Aluno.Foto, path);
                map.Aluno.Url = url;
                Aluno aluno = await _alunoRepository.CriarAluno(map.Aluno);
                map.AlunoId = aluno.Id;
            }

            map.Senha = _senhaHash.HashSenha(map.Senha);
            Usuario retorno = await _usuarioRepository.CriarUsuario(map);            
            return UsuarioMap.MapUsuario(retorno);
        }

        public async Task<UsuarioOutput> AtualizarUsuario(UsuarioInput usuario)
        {
            ValidarAtualizacao(usuario);
            Usuario map = UsuarioMap.MapUsuario(usuario);
            Personal personal = await _personalRepository.AtualizarPersonal(map.Personal);
            if (personal != null)
            {
                map.PersonalId = personal.Id;
                map.Senha = _senhaHash.HashSenha(map.Senha);
                Usuario retorno = await _usuarioRepository.AtualizarUsuario(map);
                return UsuarioMap.MapUsuario(retorno);
            }
            return null;
        }

        public async Task<UsuarioOutput> DeletarUsuario(Guid id)
        {
            Usuario retorno = await _usuarioRepository.DeletarUsuario(id);
            return UsuarioMap.MapUsuario(retorno);
        }

        private async Task<string> UploadImagem(IFormFile imagem, string path)
        {
            string bucketName = "treinospersonais";
            string type = imagem.ContentType;
            string typeImg = type.Split('/')[1];
            string objectName = $"{path}/{Guid.NewGuid()}.{typeImg}";
            using (var stream = imagem.OpenReadStream())
            {
                string url = await _minioStorage.UploadFileAsync(bucketName, objectName, stream, type);
                if (url != null)
                {
                    return url;
                }

                throw new ExceptionService("Não foi possível fazer o upload da imagem, tente novamente.");

            }
        }

        private void ValidarCadastro(UsuarioInput usuario)
        {
            string erro = ValidarDadosComuns(usuario);
            if (erro != null)
            {
                throw new ValidationException(erro);
            }
        }

        private void ValidarAtualizacao(UsuarioInput usuario)
        {
            if (!_validadorUtils.ValidarGuid((Guid)usuario.Id))
            {
                throw new ValidationException("ID do usuário é inválido.");
            }
            if (usuario.Personal != null)
            {
                if (!_validadorUtils.ValidarGuid((Guid)usuario.Personal.Id))
                {
                    throw new ValidationException("ID do personal é inválido.");
                }
                ValidarDadosPersonal(usuario);
            }
            if (usuario.Academia != null)
            {
                if (!_validadorUtils.ValidarGuid((Guid)usuario.Academia.Id))
                {
                    throw new ValidationException("ID da academia é inválido.");
                }
                ValidarDadosAcademia(usuario);
            }

            string erro = ValidarDadosComuns(usuario);
            if (erro != null)
            {
                throw new ValidationException(erro);
            }
        }

        private void ValidarDadosPersonal(UsuarioInput usuario)
        {
            string erro = ValidarPersonal(usuario);
            if (erro != null)
            {
                throw new ValidationException(erro);
            }
        }

        private void ValidarDadosAcademia(UsuarioInput usuario)
        {
            string erro = ValidarAcademia(usuario);
            if (erro != null)
            {
                throw new ValidationException(erro);
            }
        }

        private void ValidarAluno(UsuarioInput usuario)
        {
            string erro = ValidarDadosAluno(usuario);
            if (erro != null)
            {
                throw new ValidationException(erro);
            }
        }

        private string ValidarDadosAluno(UsuarioInput usuario)
        {
            if (!_validadorUtils.ValidarString(usuario.Aluno.Nome)) return "Nome é obrigatório";
            if (!_validadorUtils.ValidarString(usuario.Aluno.Sobrenome)) return "Sobrenome é obrigatório.";
            return null;
        }

        private string ValidarAcademia(UsuarioInput usuario)
        {
            if (!_validadorUtils.ValidarString(usuario.Academia.Nome)) return "Nome é obrigatório.";
            if (!_validadorUtils.ValidarString(usuario.Academia.Endereco.Cidade)) return "Cidade é obrigatório.";
            if (!_validadorUtils.ValidarString(usuario.Academia.Endereco.Bairro)) return "Bairro é obrigatório.";
            if (!_validadorUtils.ValidarString(usuario.Academia.Endereco.Logradouro)) return "Logradouro é obrigatório.";
            if (!_validadorUtils.ValidarString(usuario.Academia.Endereco.CEP)) return "CEP é obrigatório.";
            if (!_validadorUtils.ValidarString(usuario.Academia.Endereco.UF)) return "UF é obrigatório.";
            if (!_validadorUtils.ValidarString(usuario.Academia.Endereco.Numero)) return "Numero é obrigatório.";
            return null;
        }

        private string ValidarPersonal(UsuarioInput personal)
        {
            if (!_validadorUtils.ValidarString(personal.Personal.Nome)) return "Nome é obrigatório.";
            if (!_validadorUtils.ValidarCREF(personal.Personal.CREF)) return "CREF inválido.";
            if (!_validadorUtils.ValidarHoraAula(personal.Personal.ValorHora)) return "Valor da hora inválido.";
            if (!_validadorUtils.ValidarListaString(personal.Personal.Especialidades)) return "Especialidades inválidas.";
            return null;
        }

        private string ValidarDadosComuns(UsuarioInput usuario)
        {
            if (!_validadorUtils.ValidarEmail(usuario.Email)) return "E-mail inválido.";
            if (!_validadorUtils.ValidarSenha(usuario.Senha)) return "Senha inválida.";
            //if (!_validadorUtils.ValidarImagem(usuario.Imagem)) return "Imagem inválida.";
            return null;
        }
    }
}
