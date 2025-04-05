using System.ComponentModel.DataAnnotations;

namespace BackAppPersonal.Domain.Entities
{
    public class Academia : Entity
    {
        public Academia()
        {

        }

        [StringLength(100)]
        public string Nome { get; set; }
        public Guid EnderecoId { get; set; }
        public virtual Endereco Endereco { get; set; }
        [StringLength(999)]
        public string Url { get; set; }
    }
}