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
    public class RemoveDriverHandler : IRequestHandler<RemoveDriverRequest, Response>
    {
        private readonly IMapper _mapper;
        private readonly IDriverRepository _driverRepository;
        private readonly ILogger<RemoveDriverHandler> _logger;

        public RemoveDriverHandler(IMapper mapper,
            IDriverRepository driverRepository,
            ILogger<RemoveDriverHandler> logger
            )
        {
            _mapper = mapper;
            _driverRepository = driverRepository;
            _logger = logger;
        }

        public async Task<Response> Handle(RemoveDriverRequest request,
            CancellationToken cancellationToken)
        {
            var response = new Response();
            var driver = _mapper.Map<Driver>(request);

            _logger.LogTrace("Removendo o motorista", driver);

            // Verifica se o motorista existe na base de dados
            var hasDriver = await _driverRepository.HasDriverAsync(driver.Id);
            if (!hasDriver)
            {
                _logger.LogInformation("Motorista não encontrado.");
                return response.AddNotification("Motorista não encontrado.");
            }

            // Removendo motorista do banco de dados
            await _driverRepository.DeleteAsync(driver);

            return response;
        }
    }
}