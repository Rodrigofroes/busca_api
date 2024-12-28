using BackAppPersonal.Domain.Entities;
using BackAppPersonal.Domain.Intefaces;
using BackAppPersonal.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BackAppPersonal.Infrastructure.Repository
{
    public class AcademiaPersonalRepository : IAcademiaPersonalRepository
    {
        private readonly AppDbContext _context;

        public AcademiaPersonalRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<AcademiaPersonal>> AcademiaPersonals()
        {
            return await _context.AcademiaPersonais.ToListAsync();
        }

        public async Task<AcademiaPersonal> AcademiaPersonalPorId(Guid id)
        {
            return await _context.AcademiaPersonais.FirstOrDefaultAsync(x => x.Id == new Guid(id.ToString()));
        }

        public async Task<AcademiaPersonal> CriarAcademiaPersonal(AcademiaPersonal academiaPersonal)
        {
            await _context.AcademiaPersonais.AddAsync(academiaPersonal);
            await _context.SaveChangesAsync();
            return academiaPersonal;
        }

        public async Task<AcademiaPersonal> AtualizarAcademiaPersonal(AcademiaPersonal academiaPersonal)
        {
            _context.AcademiaPersonais.Update(academiaPersonal);
            await _context.SaveChangesAsync();
            return academiaPersonal;
        }

        public async Task<AcademiaPersonal> DeletarAcademiaPersonal(Guid id)
        {
            AcademiaPersonal academiaPersonal = await _context.AcademiaPersonais.FirstOrDefaultAsync(x => x.Id == new Guid(id.ToString()));
            _context.AcademiaPersonais.Remove(academiaPersonal);
            await _context.SaveChangesAsync();
            return academiaPersonal;
        }
    }
}
