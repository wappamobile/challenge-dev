using Driver.Application.Data.Entities;

namespace Driver.Api.ViewModels
{
    /// <summary>
    /// Esrtrutura de busca em caso de listagem
    /// </summary>
    public class GetDriverViewModel
    {
        /// <summary>
        /// Construtor padrão
        /// </summary>
        public GetDriverViewModel()
        {

        }

        /// <summary>
        /// Nova instancia baseada na entidade que é estrutura do banco
        /// </summary>
        /// <param name="entity"></param>
        public GetDriverViewModel(DriverEntity entity)
        {
            DriverId = entity.DriverId;
            FirstName = entity.FirstName;
            LastName = entity.LastName;
            CarBrand = entity.CarBrand;
            CarModel = entity.CarModel;
            City = entity.City;
            State = entity.State;
        }

        /// <summary>
        /// Id do motorista
        /// </summary>
        public int DriverId { get; set; }

        /// <summary>
        /// Primeiro nome do motorista
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Último nome do motorista
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Marca do carro
        /// </summary>
        public string CarBrand { get; set; }

        /// <summary>
        /// Modelo do carro
        /// </summary>
        public string CarModel { get; set; }

        /// <summary>
        /// Cidade do endereço
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// Estado do endereço
        /// </summary>
        public string State { get; set; }
    }
}