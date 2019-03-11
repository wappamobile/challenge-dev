using Driver.Application.Data.Entities;

namespace Driver.Api.ViewModels
{
    /// <summary>
    /// Dados do endereço
    /// </summary>
    public class AddressViewModel : PostAddressViewModel
    {
        /// <summary>
        /// Construtor padrão
        /// </summary>
        public AddressViewModel()
        {
        }

        /// <summary>
        /// Nova instancia baseada na entidade que é estrutura do banco
        /// </summary>
        /// <param name="entity"></param>
        public AddressViewModel(DriverEntity entity) : base(entity)
        {
            Latitude = entity.Latitude;
            Longitude = entity.Longitude;
        }

        /// <summary>
        /// Latitude do endereço
        /// </summary>
        public double? Latitude { get; set; }

        /// <summary>
        /// Longitude do endereço
        /// </summary>
        public double? Longitude { get; set; }
    }
}