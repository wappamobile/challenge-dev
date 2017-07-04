namespace Wappa.Framework.Model.Abstrato
{
    /// <summary>
    /// Classe base para a criação de um veículo
    /// </summary>
    public abstract class Veiculo
    {
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
    }
}
