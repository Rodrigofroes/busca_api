using System.ComponentModel.DataAnnotations;

namespace BackAppPersonal.Domain.Entities
{
    public class Aluno : Entity
    {
        public string Nome { get; set; } 
        public string Sobrenome { get; set; }
        [StringLength(999)]
        public string Url { get; set; }
        public Guid? PersonalId { get; set; }
        public virtual Personal? Personal { get; set; }
    }
}
