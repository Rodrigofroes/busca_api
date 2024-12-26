using BackAppPersonal.Domain.Entities;

namespace BackAppPersonal.Domain.Intefaces
{
    public interface IUsuarioRespository
    {
        Task<IEnumerable<Usuario>> Usuarios();
        Task<Usuario> UsuarioPorId(Guid id);
        Task<Usuario> CriarUsuario(Usuario usuario);
        Task<Usuario> AtualizarUsuario(Usuario usuario);
        Task<Usuario> DeletarUsuario(Guid id);
        Task<TipoUsuario> TipoUsuarioPorId(Guid id);
        Task<Usuario> UsuarioPorEmail(string email);
    }
}