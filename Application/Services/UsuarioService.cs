using System.ComponentModel.DataAnnotations;
using BackAppPersonal.Application.DTO.InputDto;
using BackAppPersonal.Application.DTO.OuputDto;
using BackAppPersonal.Application.Map;
using BackAppPersonal.Domain.Entities;
using BackAppPersonal.Domain.Exceptions;
using BackAppPersonal.Domain.Intefaces;
using BackAppPersonal.Infrastructure.Repository;
using BackAppPersonal.Utils;
using static System.Net.Mime.MediaTypeNames;

namespace BackAppPersonal.Application.Services
{
    public class UsuarioService
    {
        private readonly IUsuarioRespository _usuarioRepository;
        private readonly IPersonalRepository _personalRepository;
        private readonly IAcademiaRepository _academiaRepository;
        public readonly IEnderecoRepository _enderecoRepository;
        public readonly IMinioStorage _minioStorage;
        private readonly ISenhaHash _senhaHash;
        private readonly ValidadorUtils _validadorUtils;

        public UsuarioService(IUsuarioRespository usuarioRepository, IPersonalRepository personalRepository, ValidadorUtils validadorUtils, ISenhaHash senhaHash, IAcademiaRepository academiaRepository, IEnderecoRepository enderecoRepository, IMinioStorage minioStorage)
        {
            _personalRepository = personalRepository;
            _usuarioRepository = usuarioRepository;
            _validadorUtils = validadorUtils;
            _senhaHash = senhaHash;
            _academiaRepository = academiaRepository;
            _enderecoRepository = enderecoRepository;
            _minioStorage = minioStorage;
        }

        public async Task<IEnumerable<UsuarioOutput>> Usuarios()
        {
            IEnumerable<Usuario> usuarios = await _usuarioRepository.Usuarios();

            foreach (var usuario in usuarios)
            {
                if(usuario.PersonalId != null)
                {
                    usuario.Personal = await _personalRepository.PersonalPorId((Guid)usuario.PersonalId);
                }else
                {
                    usuario.Academia = await _academiaRepository.AcademiaPorId((Guid)usuario.AcademiaId);
                    usuario.Academia.Endereco = await _enderecoRepository.EnderecoPorId((Guid)usuario.Academia.EnderecoId);
                }
                
                usuario.TipoUsuario = await _usuarioRepository.TipoUsuarioPorId((Guid)usuario.TipoUsuarioId);
            }
            return UsuarioMap.MapUsuario(usuarios);
        }

        public async Task<UsuarioOutput> UsuarioPorId(Guid id)
        {
            Usuario usuario = await _usuarioRepository.UsuarioPorId(id);
            usuario.Personal = await _personalRepository.PersonalPorId((Guid)usuario.PersonalId);
            return UsuarioMap.MapUsuario(usuario);
        }

        public async Task<Usuario> CriarUsuario(UsuarioInput usuario)
        {
            ValidarCadastro(usuario);
            Usuario map = UsuarioMap.MapUsuario(usuario);
            string path = "";

            if (usuario.Personal != null)
            {
                Personal personal = await _personalRepository.CriarPersonal(map.Personal);
                map.PersonalId = personal.Id;
                path = "personal";
            }
            else
            {
                Academia academia = await _academiaRepository.CriarAcademia(map.Academia);
                map.AcademiaId = academia.Id;
                path = "academia";
            }

            map.Senha = _senhaHash.HashSenha(map.Senha);
            string url = await UploadImagem(usuario.Imagem, path);
            map.Url = url;
            return await _usuarioRepository.CriarUsuario(map);

            throw new ExceptionService(" Não foi possível criar um usuário, verifique os dados e tente novamente.");
        }

        public async Task<Usuario> AtualizarUsuario(UsuarioInput usuario)
        {
            //ValidarAtualizacao(usuario);
            Usuario map = UsuarioMap.MapUsuario(usuario);
            Personal personal = await _personalRepository.AtualizarPersonal(map.Personal);
            if (personal != null)
            {
                map.PersonalId = personal.Id;
                map.Senha = _senhaHash.HashSenha(map.Senha);
                return await _usuarioRepository.AtualizarUsuario(map);
            }
            throw new ExceptionService("Não foi possível atualizar o usuário, verifique os dados e tente novamente.");
        }

        public async Task<Usuario> DeletarUsuario(Guid id)
        {
            return await _usuarioRepository.DeletarUsuario(id);
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

            if(usuario.Personal != null)
            {
                if (!_validadorUtils.ValidarGuid((Guid)usuario.Personal.Id))
                {
                    throw new ValidationException("ID do personal é inválido.");
                }

            }

            if (usuario.Academia != null)
            {
                if (!_validadorUtils.ValidarGuid((Guid)usuario.Academia.Id))
                {
                    throw new ValidationException("ID da academia é inválido.");
                }


            }

            string erro = ValidarDadosComuns(usuario);
            if (erro != null)
            {
                throw new ValidationException(erro);
            }
        }

        private string ValidarDadosComuns(UsuarioInput usuario)
        {
            if (!_validadorUtils.ValidarEmail(usuario.Email)) return "E-mail inválido.";
            if (!_validadorUtils.ValidarSenha(usuario.Senha)) return "Senha inválida.";
            if (!_validadorUtils.ValidarImagem(usuario.Imagem)) return "Imagem inválida.";
            return null;
        }


        private async Task<string> UploadImagem(IFormFile imagem, string path)
        {
            string bucketName = "projetobuscapersonal";
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
    }
}
