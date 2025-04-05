using BackAppPersonal.Domain.Entities;

namespace BackAppPersonal.Domain.Intefaces
{
    public interface IOpenStreetMap
    {
        Task<List<OpenStreetMapResponse>> ConsultarLogradouro(string logradouro);
    }
}
