using System.Runtime.CompilerServices;
using BackAppPersonal.Domain.Entities;
using BackAppPersonal.Domain.Intefaces;

namespace BackAppPersonal.Application.Services
{
    public class EnderecoService
    {
        private readonly IEnderecoRepository _enderecoRepository;
        private readonly IOpenStreetMap _openStreetMap;

        public EnderecoService(IEnderecoRepository enderecoRepository, IOpenStreetMap openStreetMap)
        {
            _enderecoRepository = enderecoRepository;
            _openStreetMap = openStreetMap;
        }

        public async Task<IEnumerable<Endereco>> Enderecos()
        {

            return await _enderecoRepository.Enderecos();
        }

        public async Task<Endereco> EnderecoPorId(Guid id)
        {
            return await _enderecoRepository.EnderecoPorId(id);
        }

        public async Task<Endereco> CriarEndereco(Endereco endereco)
        {
            string logradouro = $"{endereco.Logradouro + "," + endereco.Cidade}";
            List<OpenStreetMapResponse> openStreetMapResponse = await _openStreetMap.ConsultarLogradouro(logradouro);
            Console.WriteLine(openStreetMapResponse);
            return await _enderecoRepository.CriarEndereco(endereco);
        }

        public async Task<Endereco> AtualizarEndereco(Endereco endereco)
        {
            return await _enderecoRepository.AtualizarEndereco(endereco);
        }

        public async Task<Endereco> DeletarEndereco(Guid id)
        {
            return await _enderecoRepository.DeletarEndereco(id);
        }
    }
}
