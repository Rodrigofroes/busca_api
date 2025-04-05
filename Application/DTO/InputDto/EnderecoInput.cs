using BackAppPersonal.Application.DTO.OuputDto;

namespace BackAppPersonal.Application.DTO.InputDto
{
    public class EnderecoInput
    {
        public Guid Id { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string? Complemento { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string UF { get; set; }
        public string CEP { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
