namespace BackAppPersonal.Application.DTO.OuputDto
{
    public class AlunoOutput
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public PersonalOutput? Personal { get; set; }
    }
}
