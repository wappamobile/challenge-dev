using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using Wappa.Application.CommandRequests;
using Wappa.Domain.Interfaces.Connectors;
using Wappa.Domain.Messages;
using Wappa.Domain.Models;
using Wappa.Domain.Repositories;
using static Wappa.Application.Service.AddressService;

namespace Wappa.Application.CommandHandlers
{
    public class CreateAddressHandler : IRequestHandler<CreateAddressRequest, Response>
    {
        private readonly IMapper _mapper;
        private readonly IAddressRepository _addressRepository;
        private readonly IDriverRepository _driverRepository;
        private readonly IGoogleMapsConnector _googleMaps;
        private readonly ILogger<CreateAddressHandler> _logger;

        public CreateAddressHandler(IMapper mapper,
            IAddressRepository addressRepository,
            IDriverRepository driverRepository,
            ILogger<CreateAddressHandler> logger,
            IGoogleMapsConnector googleMaps)
        {
            _mapper = mapper;
            _addressRepository = addressRepository;
            _driverRepository = driverRepository;
            _logger = logger;
            _googleMaps = googleMaps;
        }

        public async Task<Response> Handle(CreateAddressRequest request,
            CancellationToken cancellationToken)
        {
            var response = new Response();
            var address = _mapper.Map<Address>(request);

            _logger.LogTrace("Criando um novo endereço", address);

            // Verifica se o CEP já existe no banco de dados
            var hasDriver = await _driverRepository.HasDriverAsync(address.DriverId);
            if (!hasDriver)
            {
                _logger.LogInformation("Motorista não encontrado");
                return response.AddNotification("Motorista não encontrado.");
            }

            // Executa a API do Google Maps
            await ApplyLocationAsync(_googleMaps, address);

            //Insere o endereço na base de dados
            var result = await _addressRepository.SaveAsync(address);

            if (result == null || result.Id <= 0)
            {
                _logger.LogInformation("Não foi possível inserir o endereço");
                return response.AddNotification("Não foi possível inserir o endereço");
            }

            return response.AddValue(result);
        }
    }
}