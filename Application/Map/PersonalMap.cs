using BackAppPersonal.Application.DTO.InputDto;
using BackAppPersonal.Application.DTO.OuputDto;
using BackAppPersonal.Domain.Entities;

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
                CREF = personal.CREF,
                ValorHora = personal.ValorHora,
                Especialidades = personal.Especialidades.Select(x => x).ToList()
            };
        }
        public static List<PersonalOutput> MapPersonal(this List<Personal> personal)
        {
            return personal.Select(x => x.MapPersonal()).ToList();
        }
        public static Personal MapPersonal(this PersonalInput personal)
        {
            return new Personal
            {
                Id = (Guid)personal.Id,
                Nome = personal.Nome,
                CREF = personal.CREF,
                ValorHora = personal.ValorHora,
                Especialidades = personal.Especialidades.Select(x => x).ToList()
            };
        }
        public static List<Personal> MapPersonal(this List<PersonalInput> personal)
        {
            return personal.Select(x => x.MapPersonal()).ToList();
        }
    }
}
