using NSE.Core.DomainObjects;
using System;

namespace Wappa.Motorista.API.Models
{
	public class Motorista: Entity, IAggregateRoot
    {
        public string Nome { get; private set; }
        public string SobreNome { get; private set; }
        public Carro Carro { get; private set; }
        public Endereco Endereco { get; private set; }

        protected Motorista() { }

        public Motorista(Guid id, string nome, string sobreNome)
        {
            Id = id;
            Nome = nome;
            SobreNome = sobreNome;
        }
    }
}
