using BackAppPersonal.Domain.Entities;
using BackAppPersonal.Domain.Intefaces;
using BackAppPersonal.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BackAppPersonal.Infrastructure.Repository
{
    public class AlunoRepository : IAlunoRepository
    {
        private readonly AppDbContext _context;
        public AlunoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Aluno>> Alunos()
        {
            return await _context.Alunos.ToListAsync();
        }

        public async Task<Aluno> AlunosPorId(Guid id)
        {
            return await _context.Alunos.FirstOrDefaultAsync(x => x.Id == new Guid(id.ToString()));
        }

        public async Task<Aluno> CriarAluno(Aluno aluno)
        {
            await _context.Alunos.AddAsync(aluno);
            await _context.SaveChangesAsync();
            return aluno;
        }

        public async Task<Aluno> AtualizarAluno(Aluno aluno)
        {
            _context.Alunos.Update(aluno);
            await _context.SaveChangesAsync();
            return aluno;
        }

        public async Task<Aluno> DeletarAluno(Guid id)
        {
            Aluno aluno = await _context.Alunos.FirstOrDefaultAsync(x => x.Id == new Guid(id.ToString()));
            _context.Alunos.Remove(aluno);
            await _context.SaveChangesAsync();
            return aluno;
        }
    }
}
