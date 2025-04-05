namespace BackAppPersonal.Domain.Entities
{
    public class AcademiaPersonal : Entity
    {
        public AcademiaPersonal()
        {
        }
        public Guid AcademiaId { get; set; }
        public virtual Academia Academia { get; set; }
        public Guid PersonalId { get; set; }
        public virtual Personal Personal { get; set; }
    }
}
