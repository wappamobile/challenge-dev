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
    public class CreateVehicleHandler : IRequestHandler<CreateVehicleRequest, Response>
    {
        private readonly IMapper _mapper;
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IDriverRepository _driverRepository;

        private readonly ILogger<CreateVehicleHandler> _logger;

        public CreateVehicleHandler(IMapper mapper,
            IVehicleRepository VehicleRepository,
            IDriverRepository driverRepository,
            ILogger<CreateVehicleHandler> logger)
        {
            _mapper = mapper;
            _vehicleRepository = VehicleRepository;
            _driverRepository = driverRepository;
            _logger = logger;
        }

        public async Task<Response> Handle(CreateVehicleRequest request,
            CancellationToken cancellationToken)
        {
            var response = new Response();
            var vehicle = _mapper.Map<Vehicle>(request);

            _logger.LogTrace("Criando um novo veículo", vehicle);

            // Verificando se o motorista existe
            var hasDriver = await _driverRepository.HasDriverAsync(vehicle.DriverId);
            if (!hasDriver)
            {
                _logger.LogInformation("Motorista não encontrado.");
                return response.AddNotification("Motorista não encontrado.");
            }

            //Criando um novo veículo para o motorista
            var result = await _vehicleRepository.SaveAsync(vehicle);

            if (result == null || result.Id <= 0)
            {
                _logger.LogInformation("Não foi possível inserir o veículo");
                return response.AddNotification("Não foi possível inserir o veículo");
            }

            return response.AddValue(result);
        }
    }
}