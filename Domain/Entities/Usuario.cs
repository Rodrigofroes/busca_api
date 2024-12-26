using System.ComponentModel.DataAnnotations;

namespace BackAppPersonal.Domain.Entities
{
    public class Usuario : Entity
    {
        public Usuario() { }
        [StringLength(100)]
        public string Email { get; set; }
        [StringLength(100)]
        public string Senha { get; set; }
        [StringLength(999)]
        public string Url { get; set; }
        public Guid? PersonalId { get; set; }
        public virtual Personal? Personal { get; set; }
        public Guid TipoUsuarioId { get; set; }
        public virtual TipoUsuario TipoUsuario { get; set; }
        public Guid? AcademiaId { get; set; }
        public virtual Academia? Academia { get; set; }
    }
}