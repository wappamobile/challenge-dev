using System;
using System.Runtime.Serialization;
using Wappa.Framework.Model.Abstrato;
using Wappa.Framework.Model.Veiculo;

namespace Wappa.Framework.Model.Pessoa
{
    /// <summary>
    /// Classe que define os atributos de um Motorista
    /// </summary>
    [Serializable]
    public sealed class Motorista : Abstrato.Pessoa
    {
        public Carro Carro { get; set; }

        [IgnoreDataMember]
        public long VeiculoId { get; set; }

        public void Atualizar(Motorista motorista)
        {
            this.Carro.Atualizar(motorista.Carro);
            base.Atualizar(motorista);
        }
    }
}
