using System.Collections.Generic;
using System.Linq;
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

        public static MotoristaDTO ParaDTO(this Motorista entidade)
        {
            return new MotoristaDTO
            {
                Id = entidade.Id,
                PrimeiroNome = entidade.PrimeiroNome,
                UltimoNome = entidade.UltimoNome,
                Veiculo = entidade.Veiculo.ParaDTO(),
                Endereco = entidade.Endereco.ParaDTO()
            };
        }

        public static ICollection<Motorista> ParaListaDeObjetoDeDominio(this IEnumerable<MotoristaDTO> dtos)
        {
            return dtos.Select(c => c.ParaObjetoDeDominio()).ToList();
        }

        public static ICollection<MotoristaDTO> ParaListaDeDTO(this IEnumerable<Motorista> entidades)
        {
            return entidades.Select(c => c.ParaDTO()).ToList();
        }
    }
}
