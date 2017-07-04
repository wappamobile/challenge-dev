using Wappa.Framework.Model.Comum;

namespace Wappa.Framework.Model.Abstrato
{
    /// <summary>
    /// Classe base para definicao de uma Pessoa
    /// </summary>
    public abstract class Pessoa
    {
        /// <summary>
        /// Primeiro nome
        /// </summary>
        public string PrimeiroNome { get; set; }

        /// <summary>
        /// Último Nome
        /// </summary>
        public string UltimoNome { get; set; }

        /// <summary>
        /// Dados de onde reside
        /// </summary>
        public Endereco Endereco { get; set; }
    }
}
