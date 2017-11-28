using WappaChallenge.Dominio.Entidades;
using WappaChallenge.DTO;

namespace WappaChallenge.AppServices.Adapters
{
    public static class EnderecoAdapter
    {
        public static Endereco ParaObjetoDeDominio(this EnderecoDTO dto)
        {
            return new Endereco(dto.Logradouro,
                dto.Numero,
                dto.Complemento,
                dto.Bairro,
                dto.Cidade,
                dto.Estado,
                dto.CEP,
                dto.CoordenadaGeografica.ParaObjetoDeDominio());
        }

        public static EnderecoDTO ParaDTO(this Endereco entidade)
        {
            return new EnderecoDTO
            {
                Id = entidade.Id,
                Logradouro = entidade.Logradouro,
                Numero = entidade.Numero,
                Complemento = entidade.Complemento,
                Bairro = entidade.Bairro,
                Cidade = entidade.Cidade,
                Estado = entidade.Estado,
                CEP = entidade.CEP,
                CoordenadaGeografica = entidade.CoordenadaGeografica.ParaDTO()
            };
        }

        public static string ParaPadraoGoogleMapsGeoCode(this EnderecoDTO dto)
        {
            return $"{dto.Logradouro}, {dto.Numero} - {dto.Bairro} - {dto.Cidade}, {dto.CEP}";
        }
    }
}
