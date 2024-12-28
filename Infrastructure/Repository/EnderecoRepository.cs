using BackAppPersonal.Domain.Entities;
using BackAppPersonal.Domain.Intefaces;
using BackAppPersonal.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BackAppPersonal.Infrastructure.Repository
{
    public class EnderecoRepository : IEnderecoRepository
    { 
        private readonly AppDbContext _context;

        public EnderecoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Endereco>> Enderecos()
        {
            return await _context.Enderecos.ToListAsync();
        }

        public async Task<Endereco> EnderecoPorId(Guid id)
        {
            return await _context.Enderecos.FirstOrDefaultAsync(x => x.Id == new Guid(id.ToString()));
        }

        public async Task<Endereco> CriarEndereco(Endereco endereco)
        {
            _context.Enderecos.Add(endereco);
            await _context.SaveChangesAsync();
            return endereco;
        }

        public async Task<Endereco> AtualizarEndereco(Endereco endereco)
        {
            _context.Enderecos.Update(endereco);
            await _context.SaveChangesAsync();
            return endereco;
        }

        public async Task<Endereco> DeletarEndereco(Guid id)
        {
            Endereco endereco = await _context.Enderecos.FirstOrDefaultAsync(x => x.Id == new Guid(id.ToString()));
            _context.Enderecos.Remove(endereco);
            await _context.SaveChangesAsync();
            return endereco;
        }
    }
}
