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
        /// Dados de onde reside
        /// </summary>
        public Endereco Endereco { get; set; }
    }
}
