using Driver.Application.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace Driver.Api.ViewModels
{
    /// <summary>
    /// Dados do endereço
    /// </summary>
    public class CarViewModel
    {
        /// <summary>
        /// Construtor padrão
        /// </summary>
        public CarViewModel()
        {
        }

        /// <summary>
        /// Nova instancia baseada na entidade que é estrutura do banco
        /// </summary>
        /// <param name="entity"></param>
        public CarViewModel(DriverEntity entity)
        {
            Brand = entity.CarBrand;
            LicensePlate = entity.CarLicensePlate;
            Model = entity.CarModel;
        }

        /// <summary>
        /// Marca do carro
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string Brand { get; set; }

        /// <summary>
        /// Placa do carro
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string LicensePlate { get; set; }

        /// <summary>
        /// Modelo do carro
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string Model { get; set; }
    }
}