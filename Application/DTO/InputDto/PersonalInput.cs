namespace BackAppPersonal.Application.DTO.InputDto
{
    public class PersonalInput
    {
        public Guid? Id { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Telefone { get; set; }
        public string CREF { get; set; }
        public decimal ValorHora { get; set; } 
        public List<string> Especialidades { get; set; }
        public string Sexo { get; set; }
        public IFormFile Foto { get; set; }
    }
}
