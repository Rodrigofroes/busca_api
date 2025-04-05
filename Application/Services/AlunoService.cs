using System.ComponentModel.DataAnnotations;
using BackAppPersonal.Application.DTO.InputDto;
using BackAppPersonal.Application.DTO.OuputDto;
using BackAppPersonal.Application.Map;
using BackAppPersonal.Domain.Entities;
using BackAppPersonal.Domain.Intefaces;
using BackAppPersonal.Utils;
using Newtonsoft.Json.Bson;

namespace BackAppPersonal.Application.Services
{
    public class AlunoService
    {
        private readonly IAlunoRepository _alunoRepository;
        private readonly IPersonalRepository _personalRepository;
        private readonly ValidadorUtils _validadorUtils;
        public AlunoService(IAlunoRepository alunoRepository, IPersonalRepository personalRepository, ValidadorUtils validadorUtils)
        {
            _alunoRepository = alunoRepository;
            _personalRepository = personalRepository;
            _validadorUtils = validadorUtils;
        }

        public async Task<IEnumerable<AlunoOutput>> Alunos()
        {
            IEnumerable<Aluno> retorno = await _alunoRepository.Alunos();
            foreach (var aluno in retorno)
            {
                if (aluno.PersonalId != null)
                {
                    aluno.Personal = await _personalRepository.PersonalPorId((Guid)aluno.PersonalId);
                }
            }
            return AlunoMap.MapAluno(retorno);
        }

        public async Task<AlunoOutput> AlunosPorId(Guid id)
        {
            ValidarId(id);
            Aluno aluno = await _alunoRepository.AlunosPorId(id);
            if (aluno.PersonalId != null)
            {
                aluno.Personal = await _personalRepository.PersonalPorId((Guid)aluno.PersonalId);
            }
            return AlunoMap.MapAluno(aluno);
        }

        public async Task<Aluno> CriarAluno(AlunoInput aluno)
        {
            ValidarDados(aluno);
            Aluno map = AlunoMap.MapAluno(aluno);
            return await _alunoRepository.CriarAluno(map);
        }

        public async Task<Aluno> AtualizarAluno(AlunoInput aluno)
        {
            ValidarAtualizacao(aluno);
            Aluno map = AlunoMap.MapAluno(aluno);
            return await _alunoRepository.AtualizarAluno(map);
        }

        public async Task<Aluno> DeletarAluno(Guid id)
        {
            ValidarId(id);
            return await _alunoRepository.DeletarAluno(id);
        }

        private void ValidarId(Guid id)
        {
            if (!_validadorUtils.ValidarGuid(id))
            {
                throw new ValidationException("ID do Aluno é inválido.");
            }
        }

        private void ValidarAtualizacao(AlunoInput aluno)
        {
            if (!_validadorUtils.ValidarGuid((Guid)aluno.Id))
            {
                throw new ValidationException("ID do Aluno é inválido.");
            }

            string erro = ValidarDadosComuns(aluno);
            if (erro != null)
            {
                throw new ValidationException(erro);
            }
        }

        private void ValidarDados(AlunoInput aluno)
        {
            string erro = ValidarDadosComuns(aluno);
            if (erro != null)
            {
                throw new ValidationException(erro);
            }
        }

        private string ValidarDadosComuns(AlunoInput aluno)
        {
            if (!_validadorUtils.ValidarString(aluno.Nome)) return "Nome é obrigatório";
            if (!_validadorUtils.ValidarString(aluno.Sobrenome)) return "Sobrenome é obrigatório.";
            return null;
        }
    }
}
