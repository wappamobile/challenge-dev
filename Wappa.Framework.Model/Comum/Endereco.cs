using System;
using System.Runtime.Serialization;
using Wappa.Framework.Model.Abstrato;

namespace Wappa.Framework.Model.Comum
{
    /// <summary>
    /// Classe de definição base para um endereço completo 
    /// </summary>
    [Serializable]
    public sealed class Endereco
    {
        /// <summary>
        /// Identificador
        /// </summary>
        public long EnderecoId { get; set; }

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

        /// <summary>
        /// País
        /// </summary>
        public string Pais { get; set; }

        /// <summary>
        /// Coordenada que representa a Latitude do endereço
        /// </summary>
        public double Latitude { get; set; }

        /// <summary>
        /// Coordenada que representa a Longitude do endereço
        /// </summary>
        public double Longitude { get; set; }

        public void Atualizar(Endereco endereco)
        {
            if (!this.Equals(endereco))
            {
                this.CEP = !string.IsNullOrWhiteSpace(endereco.CEP) ? endereco.CEP : this.CEP;
                this.Rua = !string.IsNullOrWhiteSpace(endereco.Rua) ? endereco.Rua : this.Rua;
                this.Numero = endereco.Numero;
                this.Complemento = !string.IsNullOrWhiteSpace(endereco.Complemento) ? endereco.Complemento : this.Complemento;
                this.Cidade = !string.IsNullOrWhiteSpace(endereco.Cidade) ? endereco.Cidade : this.Cidade;
                this.UF = !string.IsNullOrWhiteSpace(endereco.UF) ? endereco.UF : this.UF;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj == null || this.GetType() != obj.GetType())
            {
                return false;
            }
            else
            {
                Endereco outro = (Endereco)obj;

                return this.CEP == outro.CEP 
                    && this.Cidade == outro.Cidade
                    && this.Complemento == outro.Complemento
                    && this.Numero == outro.Numero
                    && this.Rua == outro.Rua
                    && this.UF == outro.UF;
            }
        }

        public override int GetHashCode()
        {
            int hashEndereco = $"{this.CEP}{this.Cidade}{this.Complemento}{this.Numero}{this.Rua}{this.UF}".GetHashCode();

            return (hashEndereco << 5) + hashEndereco;
        }
    }
}
