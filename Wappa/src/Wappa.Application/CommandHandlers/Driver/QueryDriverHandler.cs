using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Wappa.Application.CommandRequests;
using Wappa.Domain.Messages;
using Wappa.Domain.Models;
using Wappa.Domain.Repositories;

namespace Wappa.Application.CommandHandlers
{
    public class QueryDriverHandler : IRequestHandler<QueryDriverRequest, Response>
    {
        private readonly IMapper _mapper;
        private readonly IDriverRepository _driverRepository;
        private readonly ILogger<QueryDriverHandler> _logger;

        public QueryDriverHandler(IMapper mapper,
            IDriverRepository driverRepository,
            ILogger<QueryDriverHandler> logger
            )
        {
            _mapper = mapper;
            _driverRepository = driverRepository;
            _logger = logger;
        }

        public async Task<Response> Handle(QueryDriverRequest request,
            CancellationToken cancellationToken)
        {
            var response = new Response();
            var driver = _mapper.Map<Driver>(request);

            _logger.LogTrace("Selecionando motoristas", driver);

            if (driver.Id != null && driver.Id <= 0)
            {
                return response.AddNotification("Código do motorista inválido");
            }

            if (!string.IsNullOrWhiteSpace(driver.Document) &&
                driver.Document.Length != 11)
            {
                return response.AddNotification("CPF inválido");
            }

            //Buscando motoristas no banco de dados
            var result = await _driverRepository.GetSearchAsync(driver);

            // Não retorno nenhum motorista?
            if (!result.Any())
            {
                return response;
            }

            // Verifica se retorna uma lista ou um objeto
            return request.Single
                 ? response.AddValue(result.FirstOrDefault())
                 : response.AddValue(result);
        }
    }
}