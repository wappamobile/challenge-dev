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
    public class ChangeAddressHandler : IRequestHandler<ChangeAddressRequest, Response>
    {
        private readonly IMapper _mapper;
        private readonly IAddressRepository _addressRepository;
        private readonly IDriverRepository _driverRepository;
        private readonly ILogger<ChangeAddressHandler> _logger;
        private readonly IGoogleMapsConnector _googleMaps;

        public ChangeAddressHandler(IMapper mapper,
            IAddressRepository addressRepository,
            IDriverRepository driverRepository,
            ILogger<ChangeAddressHandler> logger,
            IGoogleMapsConnector googleMaps)
        {
            _mapper = mapper;
            _addressRepository = addressRepository;
            _driverRepository = driverRepository;
            _logger = logger;
            _googleMaps = googleMaps;
        }

        public async Task<Response> Handle(ChangeAddressRequest request,
            CancellationToken cancellationToken)
        {
            var response = new Response();
            var address = _mapper.Map<Address>(request);

            _logger.LogTrace("Alterando as informações do endereço", address);

            // Verifica se o motorista existe
            var hasDriver = await _driverRepository.HasDriverAsync(address.DriverId);
            if (!hasDriver)
            {
                _logger.LogInformation("Motorista não encontrado");
                return response.AddNotification("Motorista não encontrado.");
            }

            await ApplyLocationAsync(_googleMaps, address);

            // Atualiza no banco de dados
            var result = await _addressRepository.SaveAsync(address);

            // Verifica se o retorno é válido
            if (result == null || result.Id <= 0)
            {
                _logger.LogInformation("Não foi possível alterar o veículo");
                return response.AddNotification("Não foi possível alterar o veículo");
            }

            return response;
        }
    }
}