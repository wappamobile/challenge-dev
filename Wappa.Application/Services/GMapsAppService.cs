using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Wappa.Application.Interfaces;
using Wappa.Domain.Interfaces;
using Wappa.Domain.Models;

namespace Wappa.Application.Services
{
    public class GMapsAppService : IGMapsAppService
    {
        private readonly IGMaps _gMapsClient;
        private readonly IMapper _mapper;

        public GMapsAppService(IGMaps gMapsClient, IMapper mapper)
        {
            _gMapsClient = gMapsClient;
            _mapper = mapper;
        }

        public async Task<ValueObjectsGMaps> GetCoordinates(string address)
        {
            return await _gMapsClient.GetCoordinatesAsync(address);

        }
    }
}
