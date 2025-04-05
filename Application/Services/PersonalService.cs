using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BackAppPersonal.Application.DTO.InputDto;
using BackAppPersonal.Application.DTO.OuputDto;
using BackAppPersonal.Application.Map;
using BackAppPersonal.Domain.Entities;
using BackAppPersonal.Domain.Intefaces;
using BackAppPersonal.Utils;

namespace BackAppPersonal.Application.Services
{
    public class PersonalService
    {
        private readonly IPersonalRepository _personalRepository;
        private readonly ValidadorUtils _validadorUtils;
        public PersonalService(IPersonalRepository personalRepository, ValidadorUtils validadorUtils)
        {
            _validadorUtils = validadorUtils;
            _personalRepository = personalRepository;
        }

        public async Task<IEnumerable<PersonalOutput>> Personals()
        {
            IEnumerable<Personal> retorno = await _personalRepository.Personals();
            return PersonalMap.MapPersonal(retorno);
        }

        public async Task<PersonalOutput> PersonalPorId(Guid id)
        {
            ValidarId(id);
            Personal retorno = await _personalRepository.PersonalPorId(id);
            return PersonalMap.MapPersonal(retorno);
        }

        public async Task<PersonalOutput> CriarPersonal(PersonalInput personal)
        {
            ValidarDados(personal);
            Personal map = PersonalMap.MapPersonal(personal);
            Personal retorno =  await _personalRepository.CriarPersonal(map);
            return PersonalMap.MapPersonal(retorno);
        }

        public async Task<PersonalOutput> AtualizarPersonal(PersonalInput personal)
        {
            ValidarAtulizacao(personal);
            Personal map = PersonalMap.MapPersonal(personal);
            Personal retorno = await _personalRepository.AtualizarPersonal(map);
            return PersonalMap.MapPersonal(retorno);
        }

        public async Task<PersonalOutput> DeletarPersonal(Guid id)
        {
            ValidarId(id);
            Personal retorno =  await _personalRepository.DeletarPersonal(id);
            return PersonalMap.MapPersonal(retorno);
        }

        private void ValidarAtulizacao(PersonalInput personal)
        {
            if(!_validadorUtils.ValidarGuid((Guid)personal.Id))
            {
                throw new ValidationException("ID do Personal é inválido.");
            }

            string erro = ValidarDadosComuns(personal);
            if (erro != null)
            {
                throw new ValidationException(erro);
            }
        }

        private void ValidarId(Guid id)
        {
            if (!_validadorUtils.ValidarGuid(id))
            {
                throw new ValidationException("ID do Personal é inválido.");
            }
        }

        private void ValidarDados(PersonalInput personal)
        {
            string erro = ValidarDadosComuns(personal);
            if (erro != null)
            {
                throw new ValidationException(erro);
            }
        }

        private string ValidarDadosComuns(PersonalInput personal)
        {
            if (!_validadorUtils.ValidarString(personal.Nome)) return "Nome é obrigatório.";
            if (!_validadorUtils.ValidarCREF(personal.CREF)) return "CREF inválido.";
            if (!_validadorUtils.ValidarHoraAula(personal.ValorHora)) return "Valor da hora inválido.";
            if (!_validadorUtils.ValidarListaString(personal.Especialidades)) return "Especialidades inválidas.";
            return null;
        }

    }
}
