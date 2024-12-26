using BackAppPersonal.Domain.Entities;

namespace BackAppPersonal.Domain.Intefaces
{
    public interface IJwtToken
    {
        string GerarToken(Usuario usuario);
        bool ValidarToken(string token);
    }
}
