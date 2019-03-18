
namespace Wappa.Driver.Api.Dtos
{
    public class DriverCarDto
    {
        /// <summary>
        /// Id do carro
        /// </summary>
        public int CarId { get; set; }
        /// <summary>
        /// Id do motorista
        /// </summary>
        public int DriverId { get; set; }
        /// <summary>
        /// Marca do carro
        /// </summary>
        public string Make { get; set; }
        /// <summary>
        /// Modelo do caroo
        /// </summary>
        public string Model { get; set; }
        /// <summary>
        /// Placa do carro
        /// </summary>
        public string Plate { get; set; }
    }
}
