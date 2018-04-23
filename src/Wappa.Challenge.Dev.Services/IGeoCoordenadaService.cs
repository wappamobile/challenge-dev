using Wappa.Challenge.Dev.Models;

namespace Wappa.Challenge.Dev.Services
{
    public interface IGeoCoordenadaService
    {
        (decimal? Latitude, decimal? Longitude) ObterGeoCoordenada(Endereco endereco);
    }
}
