using Wappa.Core.DomainObjects;
using System;

namespace Wappa.Motoristas.API.Models
{
	public class Motorista: Entity, IAggregateRoot
    {
        public string Nome { get; private set; }
        public string SobreNome { get; private set; }
        public Carro Carro { get; set; }
        public Endereco Endereco { get; set; }

        protected Motorista() { }

        public Motorista(string nome, string sobreNome)
        {
            Nome = nome;
            SobreNome = sobreNome;
        }
    }
}
