using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BackAppPersonal.Application.DTO.InputDto;
using BackAppPersonal.Application.DTO.OuputDto;
using BackAppPersonal.Application.Map;
using BackAppPersonal.Domain.Entities;
using BackAppPersonal.Domain.Exceptions;
using BackAppPersonal.Domain.Intefaces;
using BackAppPersonal.Infrastructure.Repository;
using BackAppPersonal.Utils;

namespace BackAppPersonal.Application.Services
{
    public class AcademiaService
    {
        private readonly IAcademiaRepository _academiaRepository;
        private readonly IEnderecoRepository _enderecoRepository;
        private readonly IPersonalRepository _personalRepository;
        private readonly ValidadorUtils _validadorUtils;
        private readonly IOpenStreetMap _openStreetMap;

        public AcademiaService(IAcademiaRepository academiaRepository, IEnderecoRepository enderecoRepository, IOpenStreetMap openStreetMap, ValidadorUtils validadorUtils, IPersonalRepository personalRepository)
        {
            _academiaRepository = academiaRepository;
            _enderecoRepository = enderecoRepository;
            _personalRepository = personalRepository;
            _openStreetMap = openStreetMap;
            _validadorUtils = validadorUtils;
        }

        public async Task<IEnumerable<AcademiaOutput>> Academias()
        {
            IEnumerable<Academia> academias = await _academiaRepository.Academias();

            foreach (var academia in academias)
            {
                academia.Endereco = await _enderecoRepository.EnderecoPorId(academia.EnderecoId);
            }
            return AcademiaMap.MapAcademia(academias);
        }

        public async Task<AcademiaOutput> AcademiaPorId(Guid id)
        {
            ValidarId(id);
            Academia academia = await _academiaRepository.AcademiaPorId(id);
            academia.Endereco = await _enderecoRepository.EnderecoPorId(academia.EnderecoId);
            return AcademiaMap.MapAcademia(academia);
        }

        public async Task<AcademiaOutput> CriarAcademia(AcademiaInput academia)
        {
            ValidarDados(academia);
            Academia map = AcademiaMap.MapAcademia(academia);
            string logradouro = $"{academia.Endereco.Logradouro + "," + academia.Endereco.Cidade}";
            List<OpenStreetMapResponse> response = await _openStreetMap.ConsultarLogradouro(logradouro);

            map.Endereco.Latitude = response[0].Lat;
            map.Endereco.Longitude = response[0].Lon;

            Endereco endereco = await _enderecoRepository.CriarEndereco(map.Endereco);
            if(endereco != null)
            {
                map.EnderecoId = endereco.Id;
                return AcademiaMap.MapAcademia(await _academiaRepository.CriarAcademia(map));
            }

            throw new ExceptionService(" Não foi possível criar a academia, verifique os dados e tente novamente.") ;
        }

        public async Task<AcademiaOutput> AtualizarAcademia(AcademiaInput academia)
        {
            validarDadosAtualizar(academia);
            Academia map = AcademiaMap.MapAcademia(academia);
            string logradouro = $"{academia.Endereco.Logradouro + "," + academia.Endereco.Cidade}";
            List<OpenStreetMapResponse> response = await _openStreetMap.ConsultarLogradouro(logradouro);

            map.Endereco.Latitude = response[0].Lat;
            map.Endereco.Longitude = response[0].Lon;

            Endereco endereco = await _enderecoRepository.AtualizarEndereco(map.Endereco);
            if (endereco != null)
            {
                map.EnderecoId = endereco.Id;
                return AcademiaMap.MapAcademia(await _academiaRepository.AtualizarAcademia(map));
            }

            throw new ExceptionService(" Não foi possível atualizar a academia, verifique os dados e tente novamente.");
        }

        public async Task<AcademiaOutput> DeletarAcademia(Guid id)
        {
            ValidarId(id);
            Academia academia = await _academiaRepository.DeletarAcademia(id);
            academia.Endereco = await _enderecoRepository.DeletarEndereco(academia.EnderecoId);
            return AcademiaMap.MapAcademia(academia);
        }

        public async Task<IEnumerable<AcademiaOutput>> AcademiaPersonalPorAcademiaNome(string nome)
        {
            IEnumerable<Academia> academias = await _academiaRepository.AcademiaPorNome(nome);

            foreach (var academia in academias)
            {
                academia.Endereco = await _enderecoRepository.EnderecoPorId(academia.EnderecoId);
            }

            return AcademiaMap.MapAcademia(academias);
        }

        private void validarDadosAtualizar(AcademiaInput academia)
        {
            if (!_validadorUtils.ValidarGuid((Guid)academia.Id))
            {
                throw new ValidationException("ID da Academia é inválido.");
            }

            if(!_validadorUtils.ValidarGuid((Guid)academia.Endereco.Id))
            {
                throw new ValidationException("ID do Endereço é inválido.");
            }

            string erro = ValidarDadosComuns(academia);
            if (erro != null)
            {
                throw new ValidationException(erro);
            }
        }

        private void ValidarId(Guid id)
        {
            if (!_validadorUtils.ValidarGuid(id))
            {
                throw new ValidationException("ID do Academia é inválido.");
            }
        }

        private void ValidarDados(AcademiaInput academia)
        {
            string erro = ValidarDadosComuns(academia);
            if (erro != null)
            {
                throw new ValidationException(erro);
            }
        }

        private string ValidarDadosComuns(AcademiaInput academia)
        {
            if (!_validadorUtils.ValidarString(academia.Nome)) return "Nome é obrigatório.";
            if (!_validadorUtils.ValidarString(academia.Endereco.Cidade)) return "Cidade é obrigatório.";
            if (!_validadorUtils.ValidarString(academia.Endereco.Bairro)) return "Bairro é obrigatório.";
            if (!_validadorUtils.ValidarString(academia.Endereco.Logradouro)) return "Logradouro é obrigatório.";
            if (!_validadorUtils.ValidarString(academia.Endereco.CEP)) return "CEP é obrigatório.";
            if (!_validadorUtils.ValidarString(academia.Endereco.UF)) return "UF é obrigatório.";
            if (!_validadorUtils.ValidarString(academia.Endereco.Numero)) return "Numero é obrigatório.";
            return null;
        }
    }
}
