using WappaChallenge.Dominio.Entidades;
using WappaChallenge.DTO;

namespace WappaChallenge.AppServices.Adapters
{
    public static class MotoristaAdapter
    {
        public static Motorista ParaObjetoDeDominio(this MotoristaDTO dto)
        {
            return new Motorista(
                dto.PrimeiroNome,
                dto.UltimoNome,
                dto.Veiculo.ParaObjetoDeDominio(),
                dto.Endereco.ParaObjetoDeDominio());                
        }
    }
}
