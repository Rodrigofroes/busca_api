using BackAppPersonal.Domain.Entities;
using BackAppPersonal.Domain.Intefaces;
using BackAppPersonal.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BackAppPersonal.Infrastructure.Repository
{
    public class PersonalRespository : IPersonalRepository
    {
        private readonly AppDbContext _appDbContext;

        public PersonalRespository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<IEnumerable<Personal>> Personals()
        {
            return await _appDbContext.Personais.ToListAsync();
        }

        public async Task<Personal> PersonalPorId(Guid id)
        {
            return await _appDbContext.Personais.FirstOrDefaultAsync(p => p.Id == new Guid(id.ToString()));
        }

        public async Task<Personal> CriarPersonal(Personal personal)
        {
            _appDbContext.Personais.Add(personal);
            await _appDbContext.SaveChangesAsync();
            return personal;
        }

        public async Task<Personal> AtualizarPersonal(Personal personal)
        {
            _appDbContext.Personais.Update(personal);
            await _appDbContext.SaveChangesAsync();
            return personal;
        }

        public async Task<Personal> DeletarPersonal(Guid id)
        {
            Personal personal = await _appDbContext.Personais.FirstOrDefaultAsync(p => p.Id == new Guid(id.ToString()));
            _appDbContext.Personais.Remove(personal);
            await _appDbContext.SaveChangesAsync();
            return personal;
        }

    }
}
