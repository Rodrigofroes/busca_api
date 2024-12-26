using BackAppPersonal.Application.DTO.InputDto;

namespace BackAppPersonal.Application.DTO.OuputDto
{
    public class UsuarioOutput
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Url { get; set; }
        public PersonalOutput? Personal { get; set; }
        public AcademiaOutput? Academia { get; set; }
        public TipoUsuarioOutput TipoUsuario { get; set; }
    }
}
