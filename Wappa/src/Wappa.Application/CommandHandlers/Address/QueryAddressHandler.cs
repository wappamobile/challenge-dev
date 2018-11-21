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
    public class QueryAddressHandler : IRequestHandler<QueryAddressRequest, Response>
    {
        private readonly IAddressRepository _addressRepository;
        private readonly ILogger<QueryAddressHandler> _logger;

        public QueryAddressHandler(IAddressRepository addressRepository,
            ILogger<QueryAddressHandler> logger)
        {
            _addressRepository = addressRepository;
            _logger = logger;
        }

        public async Task<Response> Handle(QueryAddressRequest request,
            CancellationToken cancellationToken)
        {
            var response = new Response();

            _logger.LogTrace("Selecionando motoristas", request);

            // Pesquisando no banco de dados
            var result = await _addressRepository.GetByDriverIdAsync(request.DriverId);

            if (!result.Any())
            {
                return response;
            }

            return response.AddValue(result);
        }
    }
}