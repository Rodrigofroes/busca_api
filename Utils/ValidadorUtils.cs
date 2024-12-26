using System.Text.RegularExpressions;

namespace BackAppPersonal.Utils
{
    public class ValidadorUtils
    {
        public bool ValidarEmail(string email)
        {
            Regex regex = new Regex(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$");
            return regex.IsMatch(email);
        }
        public bool ValidarSenha(string senha)
        {
            Regex regex = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$");
            return regex.IsMatch(senha);
        }
        public bool ValidarCREF(string cref)
        {
            Regex regex = new Regex(@"^[0-9]{6}\/[A-Z]{2}$");
            return regex.IsMatch(cref);
        }
        public bool ValidarString(string texto)
        {
            return !string.IsNullOrEmpty(texto);
        }
        public bool ValidarLista(List<string> lista)
        {
            return lista.Count > 0;
        }
        public bool ValidarNumero(int numero)
        {
            return numero > 0;
        }
        public bool ValidarGuid(Guid guid)
        {
            return guid != Guid.Empty;
        }

        public bool ValidarImagem(IFormFile imagem)
        {
            return imagem.ContentType.Contains("image");
        }
    }
}
