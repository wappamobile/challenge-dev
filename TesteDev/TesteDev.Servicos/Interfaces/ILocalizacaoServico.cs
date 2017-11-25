using TesteDev.Servicos.Entidades;

namespace TesteDev.Servicos.Interfaces
{
    public interface ILocalizacaoServico
    {
        Localizacao RetornarLocalizacao(string endereco);
    }
}
