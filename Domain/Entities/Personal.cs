using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackAppPersonal.Domain.Entities
{
    public class Personal : Entity
    {
        public Personal() { }
        [StringLength(100)]
        public string Nome { get; set; }
        [StringLength(100)]
        public string Sobrenome { get; set; }
        [StringLength(100)]
        public string Telefone { get; set; }
        [StringLength(100)]
        public string CREF { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal ValorHora { get; set; }
        public List<string> Especialidades { get; set; }
    }
}