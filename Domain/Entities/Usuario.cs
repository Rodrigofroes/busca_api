using System.ComponentModel.DataAnnotations;
using static BackAppPersonal.Domain.Entities.TipoUsuario;

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
        public Guid? AcademiaId { get; set; }
        public virtual Academia? Academia { get; set; }
        public Guid? AlunoId { get; set; }
        public virtual Aluno? Aluno { get; set; }
        public TipoUsuarioEnum Tipo { get; set; }
    }
}