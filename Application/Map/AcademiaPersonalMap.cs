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
                AcademiaId = academiaPersonal.AcademiaId,
                PersonalId = academiaPersonal.PersonalId
            };
        }

        public static List<AcademiaPersonal> MapAcademiaPersonal(this List<AcademiaPersonalInput> academiaPersonal)
        {
            return academiaPersonal.Select(x => x.MapAcademiaPersonal()).ToList();
        }

        public static AcademiaPersonalFiltroOutput AcademiaPersonalFiltroOutput(IEnumerable<AcademiaPersonal> academiaPersonals, Academia academia, Endereco endereco)
        {
            return new AcademiaPersonalFiltroOutput
            {
                Id = academia.Id,
                Academia = AcademiaMap.MapAcademia(academia),
                Personal = academiaPersonals
                    .Select(ap => PersonalMap.MapPersonal(ap.Personal))
                    .ToList()
            };
        }

        public static List<AcademiaPersonalFiltroOutput> AcademiaPersonalFiltroOutput(this List<AcademiaPersonal> academiaPersonal)
        {
            return academiaPersonal.Select(x => AcademiaPersonalFiltroOutput(new List<AcademiaPersonal> { x }, x.Academia, x.Academia.Endereco)).ToList();
        }
    }
}
