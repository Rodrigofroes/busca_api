namespace BackAppPersonal.Domain.Intefaces
{
    public interface ISenhaHash
    {
        string HashSenha(string senha);
        bool VerificarSenha(string hash, string senha);
    }
}
