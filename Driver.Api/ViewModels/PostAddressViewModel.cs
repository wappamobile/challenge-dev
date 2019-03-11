using Driver.Application.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace Driver.Api.ViewModels
{
    public class PostAddressViewModel
    {
        /// <summary>
        /// Construtor padrão
        /// </summary>
        public PostAddressViewModel()
        {
        }

        /// <summary>
        /// Nova instancia baseada na entidade que é estrutura do banco
        /// </summary>
        /// <param name="entity"></param>
        public PostAddressViewModel(DriverEntity entity)
        {
            Number = entity.AddressNumber;
            Address = entity.Address;
            District = entity.District;
            City = entity.City;
            State = entity.State;
            ZipCode = entity.ZipCode;
        }

        /// <summary>
        /// Número do endereço
        /// </summary>
        [MaxLength(20)]
        public string Number { get; set; }

        /// <summary>
        /// Descrição do endereço
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string Address { get; set; }

        /// <summary>
        /// Bairro do endereço
        /// </summary>
        [MaxLength(100)]
        public string District { get; set; }

        /// <summary>
        /// Cidade do endereço
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string City { get; set; }

        /// <summary>
        /// Estado do endereço
        /// </summary>
        [Required]
        [MaxLength(2)]
        public string State { get; set; }

        /// <summary>
        /// Cep do endereço
        /// </summary>
        [MaxLength(20)]
        public string ZipCode { get; set; }
    }
}