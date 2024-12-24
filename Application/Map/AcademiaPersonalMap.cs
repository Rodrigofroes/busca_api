using BackAppPersonal.Application.DTO.InputDto;
using BackAppPersonal.Application.DTO.OuputDto;
using BackAppPersonal.Domain.Entities;

namespace BackAppPersonal.Application.Map
{
    public static class AcademiaPersonalMap
    {
        public static AcademiaPersonalOutput MapAcademiaPersonal(this AcademiaPersonal academiaPersonal)
        {
            return new AcademiaPersonalOutput
            {
                Id = academiaPersonal.Id,
                Academia = AcademiaMap.MapAcademia(academiaPersonal.Academia),
                Personal = PersonalMap.MapPersonal(academiaPersonal.Personal)
            };
        }

        public static IEnumerable<AcademiaPersonalOutput> MapAcademiaPersonal(this IEnumerable<AcademiaPersonal> academiaPersonal)
        {
            return academiaPersonal.Select(x => x.MapAcademiaPersonal()).ToList();
        }

        public static AcademiaPersonal MapAcademiaPersonal(this AcademiaPersonalInput academiaPersonal)
        {
            return new AcademiaPersonal
            {
                Id = academiaPersonal.Id,
                AcademiaId = academiaPersonal.AcademiaId,
                PersonalId = academiaPersonal.PersonalId
            };
        }

        public static List<AcademiaPersonal> MapAcademiaPersonal(this List<AcademiaPersonalInput> academiaPersonal)
        {
            return academiaPersonal.Select(x => x.MapAcademiaPersonal()).ToList();
        }
    }
}
