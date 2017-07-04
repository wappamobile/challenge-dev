using System;
using System.Collections.Generic;
using Wappa.Framework.Model.Comum;

namespace Wappa.Framework.Model.Abstrato
{
    /// <summary>
    /// Classe base para definicao de uma Pessoa
    /// </summary>
    public abstract class Pessoa
    {
        /// <summary>
        /// Primeiro Nome
        /// </summary>
        public string Nome { get; set; }

        /// <summary>
        /// Último Nome
        /// </summary>
        public string Sobrenome { get; set; }

        /// <summary>
        /// Uniao de Nome e Sobrenome
        /// </summary>
        public string NomeCompleto { get { return $"{this.Nome} {this.Sobrenome}"; } }

        /// <summary>
        /// Dados de onde reside
        /// </summary>
        public Endereco Endereco { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null || this.GetType() != obj.GetType())
            {
                return false;
            }
            else
            {
                Pessoa outro = (Pessoa)obj;

                return this.NomeCompleto.Equals(outro.NomeCompleto);
            }

        }

        public override int GetHashCode()
        {
            int hashNome = this.NomeCompleto.GetHashCode();
            
            return (hashNome << 5) + hashNome;
        }
    }
}
