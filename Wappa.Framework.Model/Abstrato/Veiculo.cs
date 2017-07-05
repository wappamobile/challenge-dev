using System;
using System.Runtime.Serialization;

namespace Wappa.Framework.Model.Abstrato
{
    /// <summary>
    /// Classe base para a criação de um veículo
    /// </summary>
    [Serializable]
    public abstract class Veiculo
    {
        /// <summary>
        /// Identificador do veiculo
        /// </summary>
        public long VeiculoId { get; set; }

        /// <summary>
        /// Marca a qual o veículo pertence
        /// </summary>
        public string Marca { get; set; }

        /// <summary>
        /// Modelo que define e especifica o veículo
        /// </summary>
        public string Modelo { get; set; }

        /// <summary>
        /// Placa de registro do veículo
        /// </summary>
        public string Placa { get; set; }

        public void Atualizar(Veiculo veiculo)
        {
            if (!this.Equals(veiculo))
            {
                this.Marca = veiculo.Marca;
                this.Modelo = veiculo.Modelo;
                this.Placa = veiculo.Placa;
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
                Veiculo outro = (Veiculo)obj;

                return this.Marca == outro.Marca
                    && this.Modelo == outro.Modelo
                    && this.Placa == outro.Placa;
            }
        }

        public override int GetHashCode()
        {
            int hashMarca = this.Marca.GetHashCode();
            int hashModelo = this.Modelo.GetHashCode();
            int hashPlaca = this.Placa.GetHashCode();

            return (hashMarca << 5) + hashMarca + hashModelo + hashPlaca;
        }
    }
}
