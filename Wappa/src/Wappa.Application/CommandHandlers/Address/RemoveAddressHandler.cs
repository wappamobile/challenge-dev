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
    public class RemoveAddressHandler : IRequestHandler<RemoveAddressRequest, Response>
    {
        private readonly IMapper _mapper;
        private readonly IAddressRepository _addressRepository;
        private readonly ILogger<RemoveAddressHandler> _logger;

        public RemoveAddressHandler(IMapper mapper,
            IAddressRepository AddressRepository,
            ILogger<RemoveAddressHandler> logger
            )
        {
            _mapper = mapper;
            _addressRepository = AddressRepository;
            _logger = logger;
        }

        public async Task<Response> Handle(RemoveAddressRequest request,
            CancellationToken cancellationToken)
        {
            var response = new Response();
            var Address = _mapper.Map<Address>(request);

            _logger.LogTrace("Remove o endereço do motorista", Address);

            await _addressRepository.DeleteAsync(Address);

            return response;
        }
    }
}