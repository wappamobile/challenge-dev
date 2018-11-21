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
    public class RemoveVehicleHandler : IRequestHandler<RemoveVehicleRequest, Response>
    {
        private readonly IMapper _mapper;
        private readonly IVehicleRepository _VehicleRepository;
        private readonly ILogger<RemoveVehicleHandler> _logger;

        public RemoveVehicleHandler(IMapper mapper,
            IVehicleRepository VehicleRepository,
            ILogger<RemoveVehicleHandler> logger)
        {
            _mapper = mapper;
            _VehicleRepository = VehicleRepository;
            _logger = logger;
        }

        public async Task<Response> Handle(RemoveVehicleRequest request,
            CancellationToken cancellationToken)
        {
            var response = new Response();
            var Vehicle = _mapper.Map<Vehicle>(request);

            _logger.LogTrace("Removendo o veículo do motorista", Vehicle);

            await _VehicleRepository.DeleteAsync(Vehicle);

            return response;
        }
    }
}