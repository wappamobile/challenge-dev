using System;
using Wappa.Application.Interfaces;
using Wappa.Application.ViewModels;
using Wappa.Domain.Core.Bus;
using Wappa.Domain.Core.Notifications;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace Wappa.Services.Api.Controllers
{
    public class DriverController : ApiController
    {
        private readonly IDriverAppService _driverAppService;

        public DriverController(
            IDriverAppService driverAppService,
            INotificationHandler<DomainNotification> notifications,
            IMediatorHandler mediator) : base(notifications, mediator)
        {
            _driverAppService = driverAppService;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("driver-management")]
        public IActionResult Get(string order)
        {
            var drivers = _driverAppService.GetAll();

            if (!string.IsNullOrEmpty(order) && order.ToLower().Equals("lastname"))
                drivers.OrderBy(x => x.LastName);
            else
                drivers.OrderBy(x => x.Name);

            return Response(drivers);
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("driver-management/{id:guid}")]
        public IActionResult Get(Guid id)
        {
            var customerViewModel = _driverAppService.GetById(id);

            return Response(customerViewModel);
        }     

        [HttpPost]        
        [Route("driver-management")]
        public async Task<IActionResult> Post([FromBody]DriverViewModel driverViewModel)
        {
            if (!ModelState.IsValid)
            {
                NotifyModelStateErrors();
                return Response(driverViewModel);
            }

            await _driverAppService.Register(driverViewModel);

            return Response(driverViewModel);
        }
        
        [HttpPut]        
        [Route("driver-management")]
        public async Task<IActionResult> Put([FromBody]DriverViewModel driverViewModel)
        {
            if (!ModelState.IsValid)
            {
                NotifyModelStateErrors();
                return Response(driverViewModel);
            }

            await _driverAppService.Update(driverViewModel);

            return Response(driverViewModel);
        }

        [HttpDelete]        
        [Route("driver-management")]
        public IActionResult Delete(Guid id)
        {
            _driverAppService.Remove(id);
            
            return Response();
        }
    }
}
