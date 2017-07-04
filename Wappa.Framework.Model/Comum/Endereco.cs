namespace Wappa.Framework.Model.Comum
{
    /// <summary>
    /// Classe de definição base para um endereço completo 
    /// </summary>
    public sealed class Endereco
    {
        /// <summary>
        /// Rua
        /// </summary>
        public string Rua { get; set; }

        /// <summary>
        /// Número
        /// </summary>
        public short Numero { get; set; }

        /// <summary>
        /// Complemento
        /// </summary>
        public string Complemento { get; set; }

        /// <summary>
        /// Número que identifica a sua rua, bairro ou cidade
        /// </summary>
        public string CEP { get; set; }

        /// <summary>
        /// Nome da cidade
        /// </summary>
        public string Cidade { get; set; }

        /// <summary>
        /// Sigla do Estado
        /// </summary>
        public string UF { get; set; }
    }
}
