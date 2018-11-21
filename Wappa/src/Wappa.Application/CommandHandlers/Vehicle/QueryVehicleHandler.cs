using MediatR;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Wappa.Application.CommandRequests;
using Wappa.Domain.Messages;
using Wappa.Domain.Repositories;

namespace Wappa.Application.CommandHandlers
{
    public class QueryVehicleHandler : IRequestHandler<QueryVehicleRequest, Response>
    {
        private readonly IVehicleRepository _VehicleRepository;
        private readonly ILogger<QueryVehicleHandler> _logger;

        public QueryVehicleHandler(IVehicleRepository VehicleRepository,
            ILogger<QueryVehicleHandler> logger)
        {
            _VehicleRepository = VehicleRepository;
            _logger = logger;
        }

        public async Task<Response> Handle(QueryVehicleRequest request,
            CancellationToken cancellationToken)
        {
            var response = new Response();

            _logger.LogTrace("Selecionando os veículos do motorista", request);

            var result = await _VehicleRepository.GetByDriverIdAsync(request.DriverId);

            if (!result.Any())
            {
                return response;
            }

            return response.AddValue(result);
        }
    }
}