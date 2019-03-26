using Cadastro.Entities;

namespace Cadastro.Interface
{
    public interface IBuscaCoordenadas
    {
        Endereco RetornaEnderecoComCoordenadas(Endereco endereco);
    }
}