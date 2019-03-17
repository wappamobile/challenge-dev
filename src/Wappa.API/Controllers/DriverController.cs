using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wappa.API.Models;
using Wappa.Infrastructure.Data.Interface;
using Wappa.Infrastructure.Data.Models;
using Wappa.Infrastructure.Service.Interface;

namespace Wappa.API.Controllers
{
    [Produces("application/json")]
    [Route("api/[Controller]")]
    public class DriverController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IDriverRepository _driverRepository;
        private readonly IGoogleMapsService _googleMapsService;

        public DriverController(IDriverRepository driverRepository,
            IGoogleMapsService googleMapsService,
            IMapper mapper)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _driverRepository = driverRepository ?? throw new ArgumentNullException(nameof(driverRepository));
            _googleMapsService = googleMapsService ?? throw new ArgumentNullException(nameof(googleMapsService));
        }

        // GET api/drivers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DriverViewModel>>> Get()
        {
            var result = _mapper.Map<IEnumerable<Driver>>(await _driverRepository.GetAllDrivers());
            return new ObjectResult(result);
        }

        // GET api/driver/1
        [HttpGet("{id}")]
        public async Task<ActionResult<DriverViewModel>> Get(string id)
        {
            var todo = _mapper.Map<DriverViewModel>(await _driverRepository.GetDriver(id));

            if (todo == null)
                return new NotFoundResult();

            return new ObjectResult(todo);
        }

        // POST api/driver
        [HttpPost]
        public async Task<ActionResult<DriverViewModel>> Post([FromBody] DriverViewModel driverViewModel)
        {
            var latlong = await _googleMapsService.GetLatitudeLongitude(driverViewModel.Address);
            driverViewModel.Latitude = latlong.latitude;
            driverViewModel.Longitude = latlong.longitude;

            await _driverRepository.Create(_mapper.Map<Driver>(driverViewModel));
            return new OkObjectResult(driverViewModel);
        }

        // PUT api/driver/1
        [HttpPut("{id}")]
        public async Task<ActionResult<DriverViewModel>> Put(string id, [FromBody] DriverViewModel driverViewModel)
        {
            var driver = await _driverRepository.GetDriver(id);

            if (driver == null)
                return new NotFoundResult();

            driverViewModel.Id = driver.Id;

            await _driverRepository.Update(_mapper.Map<Driver>(driverViewModel));

            return new OkObjectResult(driverViewModel);

        }

        // DELETE api/driver/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var post = await _driverRepository.GetDriver(id);

            if (post == null)
                return new NotFoundResult();

            await _driverRepository.Delete(id);

            return new OkResult();
        }

    }
}
