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
    }
}
