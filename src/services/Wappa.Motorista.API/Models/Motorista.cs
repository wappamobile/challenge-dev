using Wappa.Core.DomainObjects;
using System;

namespace Wappa.Motoristas.API.Models
{
	public class Motorista: Entity, IAggregateRoot
    {
        public string Nome { get; set; }
        public string SobreNome { get;  set; }
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
