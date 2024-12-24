using BackAppPersonal.Application.DTO.InputDto;
using BackAppPersonal.Application.DTO.OuputDto;
using BackAppPersonal.Domain.Entities;

namespace BackAppPersonal.Application.Map
{
    public static class EnderecoMap
    {
        public static EnderecoOutput MapEndereco(this Endereco endereco)
        {
            return new EnderecoOutput
            {
                Id = endereco.Id,
                Logradouro = endereco.Logradouro,
                Numero = endereco.Numero,
                Complemento = endereco.Complemento,
                Bairro = endereco.Bairro,
                Cidade = endereco.Cidade,
                UF = endereco.UF,
                CEP = endereco.CEP,
                Latitude = endereco.Latitude,
                Longitude = endereco.Longitude
            };
        }

        public static List<EnderecoOutput> MapEndereco(this List<Endereco> endereco)
        {
            return endereco.Select(x => x.MapEndereco()).ToList();
        }

        public static Endereco MapEndereco(this EnderecoInput endereco)
        {
            return new Endereco
            {
                Id = endereco.Id,
                Logradouro = endereco.Logradouro,
                Numero = endereco.Numero,
                Complemento = endereco.Complemento,
                Bairro = endereco.Bairro,
                Cidade = endereco.Cidade,
                UF = endereco.UF,
                CEP = endereco.CEP,
                Latitude = endereco.Latitude,
                Longitude = endereco.Longitude
            };
        }

        public static List<Endereco> MapEndereco(this List<EnderecoInput> endereco)
        {
            return endereco.Select(x => x.MapEndereco()).ToList();
        }
    }
}
