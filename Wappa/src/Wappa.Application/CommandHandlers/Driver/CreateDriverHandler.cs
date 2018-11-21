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
    public class CreateDriverHandler : IRequestHandler<CreateDriverRequest, Response>
    {
        private readonly IMapper _mapper;
        private readonly IDriverRepository _driverRepository;
        private readonly ILogger<CreateDriverHandler> _logger;

        public CreateDriverHandler(IMapper mapper,
            IDriverRepository driverRepository,
            ILogger<CreateDriverHandler> logger)
        {
            _mapper = mapper;
            _driverRepository = driverRepository;
            _logger = logger;
        }

        public async Task<Response> Handle(CreateDriverRequest request,
            CancellationToken cancellationToken)
        {
            var response = new Response();
            var driver = _mapper.Map<Driver>(request);

            _logger.LogTrace("Criando um novo motorista", driver);

            //Verifica se o CPF já existe no banco de dados
            var hasDriver = await _driverRepository.HasDriverAsync(driver.Document);
            if (hasDriver)
            {
                _logger.LogInformation("Motorista já cadastrado.");
                return response.AddNotification("Motorista já cadastrado.");
            }

            //Inserindo no banco de dados
            var result = await _driverRepository.SaveAsync(driver);

            if (result == null || result.Id <= 0)
            {
                _logger.LogInformation("Não foi possível inserir o motorista");
                return response.AddNotification("Não foi possível inserir o motorista");
            }

            return response.AddValue(result);
        }
    }
}