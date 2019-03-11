using Driver.Application.Data.Entities;

namespace Driver.Api.ViewModels
{
    /// <summary>
    /// Estrutura de de motorista quando buscado pelo id
    /// </summary>
    public class GetDriverByIdViewModel
    {
        /// <summary>
        /// Construtor padrão
        /// </summary>
        public GetDriverByIdViewModel()
        {

        }

        /// <summary>
        /// Nova instancia baseada na entidade que é estrutura do banco
        /// </summary>
        /// <param name="entity"></param>
        public GetDriverByIdViewModel(DriverEntity entity)
        {
            DriverId = entity.DriverId;
            FirstName = entity.FirstName;
            LastName = entity.LastName;
            Address = new AddressViewModel(entity);
            Car = new CarViewModel(entity);
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
        /// Informação do endereço
        /// </summary>        
        public AddressViewModel Address { get; set; }

        /// <summary>
        /// Informações do carro
        /// </summary>
        public CarViewModel Car { get; set; }
    }
}