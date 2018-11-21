using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using Wappa.Application.CommandRequests;
using Wappa.Domain.Messages;
using Wappa.Domain.Models;
using Wappa.Domain.Repositories;

namespace Wappa.Application.CommandHandlers
{
    public class ChangeVehicleHandler : IRequestHandler<ChangeVehicleRequest, Response>
    {
        private readonly IMapper _mapper;
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IDriverRepository _driverRepository;
        private readonly ILogger<ChangeVehicleHandler> _logger;

        public ChangeVehicleHandler(IMapper mapper,
            IVehicleRepository vehicleRepository,
            IDriverRepository driverRepository,
            ILogger<ChangeVehicleHandler> logger)
        {
            _mapper = mapper;
            _vehicleRepository = vehicleRepository;
            _driverRepository = driverRepository;
            _logger = logger;
        }

        public async Task<Response> Handle(ChangeVehicleRequest request,
            CancellationToken cancellationToken)
        {
            var response = new Response();
            var vehicle = _mapper.Map<Vehicle>(request);

            _logger.LogTrace("Atualizando o veículo do motorista", vehicle);

            // Verificando de o motorista existe
            var hasDriver = await _driverRepository.HasDriverAsync(vehicle.DriverId);
            if (!hasDriver)
            {
                _logger.LogInformation("Motorista não encontrado.");
                return response.AddNotification("Motorista não encontrado.");
            }

            // Atualizando os dados do veículo
            var result = await _vehicleRepository.SaveAsync(vehicle);

            if (result == null || result.Id <= 0)
            {
                _logger.LogInformation("Não foi possível alterar o veículo");
                return response.AddNotification("Não foi possível alterar o veículo");
            }

            return response;
        }
    }
}