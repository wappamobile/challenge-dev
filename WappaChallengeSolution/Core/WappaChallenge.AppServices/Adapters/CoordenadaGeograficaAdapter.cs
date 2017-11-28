using WappaChallenge.Dominio.Entidades;
using WappaChallenge.DTO;

namespace WappaChallenge.AppServices.Adapters
{
    public static class CoordenadaGeograficaAdapter
    {
        public static CoordenadaGeografica ParaObjetoDeDominio(this CoordenadaGeograficaDTO dto)
        {
            return new CoordenadaGeografica(dto.Latitude, dto.Longitude);
        }

        public static CoordenadaGeograficaDTO ParaDTO(this CoordenadaGeografica entidade)
        {
            return new CoordenadaGeograficaDTO
            {
                Id = entidade.Id,
                Latitude = entidade.Latitude,
                Longitude = entidade.Longitude
            };
        }
    }
}
