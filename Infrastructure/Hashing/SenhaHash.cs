using BackAppPersonal.Domain.Intefaces;
using BCrypt.Net;

namespace BackAppPersonal.Infrastructure.Hashing
{
    public class SenhaHash : ISenhaHash
    {
        public string HashSenha(string senha)
        {
            return BCrypt.Net.BCrypt.HashPassword(senha);
        }

        public bool VerificarSenha(string hash, string senha)
        {
            return BCrypt.Net.BCrypt.Verify(senha, hash);
        }
    }
}
