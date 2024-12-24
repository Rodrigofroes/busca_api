using BackAppPersonal.Application.DTO.InputDto;
using BackAppPersonal.Application.DTO.OuputDto;
using BackAppPersonal.Domain.Entities;

namespace BackAppPersonal.Application.Map
{
    public static class TipoUsuarioMap
    {
            public static TipoUsuarioOutput MapTipoUsuario(this TipoUsuario tipoUsuario)
            {
                return new TipoUsuarioOutput
                {
                    Id = tipoUsuario.Id,
                    Tipo = tipoUsuario.TIpo,
                };
            }

            public static List<TipoUsuarioOutput> MapTipoUsuario(this List<TipoUsuario> tipoUsuario)
            {
                return tipoUsuario.Select(x => x.MapTipoUsuario()).ToList();
            }
        }
    }
