using Cadastro.Entities;
using Cadastro.Interface;

namespace Cadastro.Api
{
    public class BuscaCoordenadas : IBuscaCoordenadas
    {
        private readonly Endereco _endereco;
        private readonly ConsultaApiGoogle _consulta  = new ConsultaApiGoogle();
        public BuscaCoordenadas( Endereco endereco)
        {
            _endereco = endereco;
        }


        public Endereco RetornaEnderecoComCoordenadas(Endereco endereco)
        {
            return _consulta.Consulta(endereco.Logradouro);
        }
   }
}
