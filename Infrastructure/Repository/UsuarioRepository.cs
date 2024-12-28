using BackAppPersonal.Domain.Entities;
using BackAppPersonal.Domain.Intefaces;
using BackAppPersonal.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BackAppPersonal.Infrastructure.Repository
{
    public class UsuarioRepository : IUsuarioRespository
    {
        private readonly AppDbContext _appDbContext;

        public UsuarioRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<IEnumerable<Usuario>> Usuarios()
        {
            return await _appDbContext.Usuarios.ToListAsync();
        }

        public async Task<Usuario> UsuarioPorId(Guid id)
        {
            return await _appDbContext.Usuarios.FirstOrDefaultAsync(u => u.Id == new Guid(id.ToString()));
        }

        public async Task<Usuario> CriarUsuario(Usuario usuario)
        {
            try
            {
                await _appDbContext.Usuarios.AddAsync(usuario);
                await _appDbContext.SaveChangesAsync();
                return usuario;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.InnerException?.Message);
                throw;
            }
        }

        public async Task<Usuario> AtualizarUsuario(Usuario usuario)
        {
            _appDbContext.Usuarios.Update(usuario);
            await _appDbContext.SaveChangesAsync();
            return usuario;
        }

        public async Task<Usuario> DeletarUsuario(Guid id)
        {
            Usuario usuario = await _appDbContext.Usuarios.FirstOrDefaultAsync(u => u.Id == new Guid(id.ToString()));
            _appDbContext.Usuarios.Remove(usuario);
            await _appDbContext.SaveChangesAsync();
            return usuario;
        }

        public async Task<Usuario> UsuarioPorEmail(string email)
        {
            return await _appDbContext.Usuarios.FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}
