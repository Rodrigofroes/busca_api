using BackAppPersonal.Domain.Entities;

namespace BackAppPersonal.Domain.Intefaces
{
    public interface IEnderecoRepository
    {
        Task<IEnumerable<Endereco>> Enderecos();
        Task<Endereco> EnderecoPorId(Guid id);
        Task<Endereco> CriarEndereco(Endereco endereco);
        Task<Endereco> AtualizarEndereco(Endereco endereco);
        Task<Endereco> DeletarEndereco(Guid id);
    }
}
