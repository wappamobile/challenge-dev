namespace Wappa.Framework.Model.Comum
{
    /// <summary>
    /// Classe de definição base para um endereço completo 
    /// </summary>
    public sealed class Endereco
    {
        /// <summary>
        /// Rua
        /// <![CDATA[Ex.: Avenida Brasil]]>
        /// </summary>
        public string Rua { get; set; }

        /// <summary>
        /// Número
        /// <![CDATA[Ex.: 128]]>
        /// </summary>
        public short Numero { get; set; }

        /// <summary>
        /// Complemento
        /// <![CDATA[Ex.: Casa, Apartamento, Bloco, etc...]]>
        /// </summary>
        public string Complemento { get; set; }

        /// <summary>
        /// Número que identifica a sua rua, bairro ou cidade
        /// <![CDATA[Ex.: 58900-000]]>
        /// </summary>
        public string CEP { get; set; }

        /// <summary>
        /// Nome da cidade
        /// <![CDATA[Ex.: São Paulo]]>
        /// </summary>
        public string Cidade { get; set; }

        /// <summary>
        /// Sigla do Estado
        /// <![CDATA[Ex.: SP]]>
        /// </summary>
        public string UF { get; set; }
    }
}
