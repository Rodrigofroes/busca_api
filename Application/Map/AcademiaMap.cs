using BackAppPersonal.Application.DTO.InputDto;
using BackAppPersonal.Application.DTO.OuputDto;
using BackAppPersonal.Domain.Entities;

namespace BackAppPersonal.Application.Map
{
    public static class AcademiaMap
    {
        public static AcademiaOutput MapAcademia(this Academia academia)
        {
            return new AcademiaOutput
            {
                Id = academia.Id,
                Nome = academia.Nome,
                DataCriacao = academia.CreatedAt,
                Endereco =  EnderecoMap.MapEndereco(academia.Endereco),
                Foto = academia.Url
            };
        }

        public static IEnumerable<AcademiaOutput> MapAcademia(this IEnumerable<Academia> academia)
        {
            return academia.Select(x => x.MapAcademia()).ToList();
        }

        public static Academia MapAcademia(this AcademiaInput academia)
        {
            return new Academia
            {
                Nome = academia.Nome,
                EnderecoId = academia.Endereco.Id,
                Endereco = EnderecoMap.MapEndereco(academia.Endereco)
            };
        }

        public static IEnumerable<Academia> MapAcademia(this IEnumerable<AcademiaInput> academia)
        {
            return academia.Select(x => x.MapAcademia()).ToList();
        }
    }
}
