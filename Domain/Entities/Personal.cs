using System.ComponentModel.DataAnnotations;

namespace BackAppPersonal.Domain.Entities
{
    public class Personal : Entity
    {
        public Personal() { }
        [StringLength(100)]
        public string Nome { get; set; }
        [StringLength(100)]
        public string CREF { get; set; }
        public List<string> Especialidades { get; set; }
    }
}
