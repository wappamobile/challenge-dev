using System;

namespace Motoristas.Core
{
    public class Motorista
    {
        public Motorista(string nome,
                         string ultimoNome,
                         Endereco endereco,
                         Veiculo veiculo)
        {
            Nome = nome ?? throw new ArgumentNullException(nameof(nome));
            UtimoNome = ultimoNome ?? throw new ArgumentNullException(nameof(ultimoNome));
            Endereco = endereco;
            Veiculo = veiculo;
        }

        public string Nome { get; }
        public string UtimoNome { get;  }
        public Endereco Endereco { get; }
        public Veiculo Veiculo { get; }
    }
}