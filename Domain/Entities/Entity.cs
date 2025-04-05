using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackAppPersonal.Domain.Entities
{
    public abstract class Entity
    {
        [Key]
        public Guid Id { get; private set; }
        public DateTime CreatedAt { get; set; }
        protected Entity()
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTime.UtcNow;
        }
    }
}
