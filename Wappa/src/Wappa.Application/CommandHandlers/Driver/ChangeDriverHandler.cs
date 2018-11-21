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
    public class ChangeDriverHandler : IRequestHandler<ChangeDriverRequest, Response>
    {
        private readonly IMapper _mapper;
        private readonly IDriverRepository _driverRepository;
        private readonly ILogger<ChangeDriverHandler> _logger;

        public ChangeDriverHandler(IMapper mapper,
            IDriverRepository driverRepository,
            ILogger<ChangeDriverHandler> logger)
        {
            _mapper = mapper;
            _driverRepository = driverRepository;
            _logger = logger;
        }

        public async Task<Response> Handle(ChangeDriverRequest request,
            CancellationToken cancellationToken)
        {
            var response = new Response();
            var driver = _mapper.Map<Driver>(request);

            _logger.LogTrace("Atualiza dados do motorista", driver);

            //Validando dados do motorista
            var hasDriver = await _driverRepository.HasDriverAsync(driver.Id);
            if (!hasDriver)
            {
                _logger.LogInformation("Motorista não encontrado");
                return response.AddNotification("Motorista não encontrado.");
            }

            // Atualiza dados do motorista no banco de dados
            var result = await _driverRepository.SaveAsync(driver);

            if (result == null || result.Id <= 0)
            {
                _logger.LogInformation("Não foi possível alterar o motorista");
                return response.AddNotification("Não foi possível alterar o motorista");
            }

            return response;
        }
    }
}