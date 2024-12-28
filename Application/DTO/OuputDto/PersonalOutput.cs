namespace BackAppPersonal.Application.DTO.OuputDto
{
    public class PersonalOutput
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string CREF { get; set; }
        public decimal ValorHora { get; set; }
        public List<string> Especialidades { get; set; }
    }
}
