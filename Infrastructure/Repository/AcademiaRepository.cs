using BackAppPersonal.Domain.Entities;
using BackAppPersonal.Domain.Intefaces;
using BackAppPersonal.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BackAppPersonal.Infrastructure.Repository
{
    public class AcademiaRepository : IAcademiaRepository
    {
       private readonly AppDbContext _context;

        public AcademiaRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Academia>> Academias()
        {
            return await _context.Academias.ToListAsync();
        }

        public async Task<Academia> AcademiaPorId(Guid id)
        {
            return await _context.Academias.FirstOrDefaultAsync(x => x.Id == new Guid(id.ToString()));
        }

        public async Task<Academia> CriarAcademia(Academia academia)
        {
            _context.Academias.Add(academia);
            _context.SaveChanges();
            return academia;
        }

        public async Task<Academia> AtualizarAcademia(Academia academia)
        {
            _context.Academias.Update(academia);
            await _context.SaveChangesAsync();
            return academia;
        }

        public async Task<Academia> DeletarAcademia(Guid id)
        {
            Academia academia = await _context.Academias.FirstOrDefaultAsync(x => x.Id == new Guid(id.ToString()));
            _context.Academias.Remove(academia!);
            await _context.SaveChangesAsync();
            return academia!;
        }

        public async Task<IEnumerable<Academia>> AcademiaPersonalPorNome(string nome)
        {
            return await _context.Academias.Where(x => x.Nome.Contains(nome)).ToListAsync();
        }

    }
}
