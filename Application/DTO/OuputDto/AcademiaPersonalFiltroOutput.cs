namespace BackAppPersonal.Application.DTO.OuputDto
{
    public class AcademiaPersonalFiltroOutput
    {
        public Guid Id { get; set; }
        public AcademiaOutput Academia { get; set; }
        public List<PersonalOutput> Personal { get; set; }
    }
}
