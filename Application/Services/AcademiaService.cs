using System.Collections.Generic;
using BackAppPersonal.Application.DTO.InputDto;
using BackAppPersonal.Application.DTO.OuputDto;
using BackAppPersonal.Application.Map;
using BackAppPersonal.Domain.Entities;
using BackAppPersonal.Domain.Exceptions;
using BackAppPersonal.Domain.Intefaces;

namespace BackAppPersonal.Application.Services
{
    public class AcademiaService
    {
        private readonly IAcademiaRepository _academiaRepository;
        private readonly IEnderecoRepository _enderecoRepository;
        private readonly IOpenStreetMap _openStreetMap;

        public AcademiaService(IAcademiaRepository academiaRepository, IEnderecoRepository enderecoRepository, IOpenStreetMap openStreetMap)
        {
            _academiaRepository = academiaRepository;
            _enderecoRepository = enderecoRepository;
            _openStreetMap = openStreetMap;
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

            Academia academia = await _academiaRepository.AcademiaPorId(id);
            academia.Endereco = await _enderecoRepository.EnderecoPorId(academia.EnderecoId);
            return AcademiaMap.MapAcademia(academia);
        }

        public async Task<Academia> CriarAcademia(AcademiaInput academia)
        {
            Academia map = AcademiaMap.MapAcademia(academia);

            string logradouro = $"{academia.Endereco.Logradouro + "," + academia.Endereco.Cidade}";
            List<OpenStreetMapResponse> response = await _openStreetMap.ConsultarLogradouro(logradouro);

            map.Endereco.Latitude = response[0].Lat;
            map.Endereco.Longitude = response[0].Lon;

            Endereco endereco = await _enderecoRepository.CriarEndereco(map.Endereco);
            if(endereco != null)
            {
                map.EnderecoId = endereco.Id;
                return await _academiaRepository.CriarAcademia(map);
            }

            throw new ExceptionService(" Não foi possível criar a academia, verifique os dados e tente novamente.") ;
        }

        public async Task<Academia> AtualizarAcademia(AcademiaInput academia)
        {
            Academia map = AcademiaMap.MapAcademia(academia);
            string logradouro = $"{academia.Endereco.Logradouro + "," + academia.Endereco.Cidade}";
            List<OpenStreetMapResponse> response = await _openStreetMap.ConsultarLogradouro(logradouro);

            map.Endereco.Latitude = response[0].Lat;
            map.Endereco.Longitude = response[0].Lon;

            Endereco endereco = await _enderecoRepository.AtualizarEndereco(map.Endereco);
            if (endereco != null)
            {
                map.EnderecoId = endereco.Id;
                return await _academiaRepository.AtualizarAcademia(map);
            }

            throw new ExceptionService(" Não foi possível atualizar a academia, verifique os dados e tente novamente.");
        }

        public async Task<Academia> DeletarAcademia(Guid id)
        {
            return await _academiaRepository.DeletarAcademia(id);
        }

    }
}
