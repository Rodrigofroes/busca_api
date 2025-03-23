namespace BackAppPersonal.Application.DTO.OuputDto
{
    public class PersonalOutput
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Telefone { get; set; }
        public string CREF { get; set; }
        public decimal ValorHora { get; set; }
        public List<string> Especialidades { get; set; }
        public string Sexo { get; set; }
        public Boolean Ativo { get; set; }
        public string Foto { get; set; }
    }
}
