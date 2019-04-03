using AutoMapper;
using AutoMapper.QueryableExtensions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Wappa.Application.Interfaces;
using Wappa.Application.ViewModels;
using Wappa.Domain.Commands;
using Wappa.Domain.Core.Bus;
using Wappa.Domain.Interfaces;
using Wappa.Domain.Models;

namespace Wappa.Application.Services
{
    public class DriverAppService : IDriverAppService
    {
        private readonly IMapper _mapper;
        private readonly IDriverRepository _driverRepository;
        private readonly IGMapsAppService _gMapsAppService;
        private readonly IMediatorHandler Bus;

        public DriverAppService(IMapper mapper,
                                  IDriverRepository driverRepository,
                                  IGMapsAppService gMapsAppService,
                                  IMediatorHandler bus)
        {
            _mapper = mapper;
            _driverRepository = driverRepository;
            _gMapsAppService = gMapsAppService;
            Bus = bus;
        }

        public IEnumerable<DriverViewModel> GetAll()
        {
            return _driverRepository.GetAll()
                .ProjectTo<DriverViewModel>(_mapper.ConfigurationProvider);
        }

        public DriverViewModel GetById(Guid id)
        {
            return _mapper.Map<DriverViewModel>(_driverRepository.GetById(id));
        }

        public async Task Register(DriverViewModel driverViewModel)
        {
            var coordinatesResults = await _gMapsAppService.GetCoordinates(driverViewModel.Address);
            driverViewModel.Coordinates = coordinatesResults.Coordinates;
            var registerCommand = _mapper.Map<RegisterNewDriverCommand>(driverViewModel);
            await Bus.SendCommand(registerCommand);
        }

        public async Task Update(DriverViewModel driverViewModel)
        {
            var coordinatesResults = await _gMapsAppService.GetCoordinates(driverViewModel.Address);
            driverViewModel.Coordinates = coordinatesResults.Coordinates;
            var updateCommand = _mapper.Map<UpdateDriverCommand>(driverViewModel);
            await Bus.SendCommand(updateCommand);
        }

        public void Remove(Guid id)
        {
            var removeCommand = new RemoveDriverCommand(id);
            Bus.SendCommand(removeCommand);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
