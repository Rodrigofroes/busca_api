namespace BackAppPersonal.Application.DTO.InputDto
{
    public class AcademiaInput
    {
        public Guid? Id { get; set; }
        public string Nome { get; set; }
        public EnderecoInput Endereco { get; set; }
        public IFormFile Foto { get; set; }
    }
}
