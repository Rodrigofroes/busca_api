namespace BackAppPersonal.Application.DTO.InputDto
{
    public class PersonalInput
    {
        public Guid? Id { get; set; }
        public string Nome { get; set; }
        public string CREF { get; set; }
        public List<string> Especialidades { get; set; }

    }
}
