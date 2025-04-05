using BackAppPersonal.Application.DTO.InputDto;
using BackAppPersonal.Application.DTO.OuputDto;
using BackAppPersonal.Domain.Entities;
using static BackAppPersonal.Domain.Entities.TipoUsuario;
using static BackAppPersonal.Domain.Entities.TipoSexo;


namespace BackAppPersonal.Application.Map
{
    public static class PersonalMap
    {
        public static PersonalOutput MapPersonal(this Personal personal)
        {
            return new PersonalOutput
            {
                Id = personal.Id,
                Nome = personal.Nome,
                Sobrenome = personal.Sobrenome,
                Telefone = personal.Telefone,
                CREF = personal.CREF,
                ValorHora = personal.ValorHora,
                Especialidades = personal.Especialidades.Select(x => x).ToList(),
                Sexo = personal.Sexo.ToString(),
                Ativo = personal.Ativo,
                Foto = personal.Url
            };
        }
        public static IEnumerable<PersonalOutput> MapPersonal(this IEnumerable<Personal> personal)
        {
            return personal.Select(x => x.MapPersonal()).ToList();
        }
        public static Personal MapPersonal(this PersonalInput personal)
        {
            return new Personal
            {
                Nome = personal.Nome,
                Sobrenome = personal.Sobrenome,
                Telefone = personal.Telefone,
                CREF = personal.CREF,
                ValorHora = personal.ValorHora,
                Especialidades = personal.Especialidades.Select(x => x).ToList(),
                Sexo = Enum.Parse<TipoSexoEnum>(personal.Sexo)
            };
        }
        public static IEnumerable<Personal> MapPersonal(this IEnumerable<PersonalInput> personal)
        {
            return personal.Select(x => x.MapPersonal()).ToList();
        }
    }
}
