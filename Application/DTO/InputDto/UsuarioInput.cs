namespace BackAppPersonal.Application.DTO.InputDto
{
    public class UsuarioInput
    {
        public Guid? Id { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public PersonalInput? Personal { get; set; }
        public AcademiaInput? Academia { get; set; }
        public AlunoInput? Aluno { get; set; }
        public Guid TipoUsuario { get; set; }
        //public IFormFile Imagem { get; set; }
        public string Tipo { get; set; }
    }
}
