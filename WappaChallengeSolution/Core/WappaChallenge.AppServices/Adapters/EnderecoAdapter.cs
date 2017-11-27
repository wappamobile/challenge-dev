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

        public static string ParaJson(this EnderecoDTO dto)
        {
            return string.Empty;
        }
    }
}
