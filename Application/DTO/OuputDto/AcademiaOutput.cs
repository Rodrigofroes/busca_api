using BackAppPersonal.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace BackAppPersonal.Application.DTO.OuputDto
{
    public class AcademiaOutput
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public DateTime DataCriacao { get; set; }
        public EnderecoOutput Endereco { get; set; }
    }
}
