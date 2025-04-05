using System.ComponentModel.DataAnnotations;

namespace BackAppPersonal.Domain.Entities
{
    public class Endereco : Entity
    {
        public Endereco()
        {
            
        }
        [StringLength(100)]
        public string Logradouro { get; set; }

        [StringLength(100)]
        public string Numero { get; set; }

        [StringLength(100)]
        public string? Complemento { get; set; }

        [StringLength(100)]
        public string Bairro { get; set; }

        [StringLength(100)]
        public string Cidade { get; set; }

        [StringLength(100)]
        public string UF { get; set; }

        [StringLength(100)]
        public string CEP { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}