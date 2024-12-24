namespace BackAppPersonal.Application.DTO.OuputDto
{
    public class AcademiaPersonalOutput
    {
        public Guid Id { get; set; }
        public AcademiaOutput Academia { get; set; }
        public PersonalOutput Personal{ get; set; }
    }
}
