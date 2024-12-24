using System.ComponentModel.DataAnnotations;

namespace BackAppPersonal.Domain.Entities
{
    public abstract class Entity
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public Entity()
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTime.UtcNow;
        }
    }
}
