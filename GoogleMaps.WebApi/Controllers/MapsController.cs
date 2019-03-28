using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GoogleMaps.WebApi.Models;
using GoogleMaps.WebApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GoogleMaps.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MapsController : ControllerBase
    {
        IMapsService _mapsService;

        public MapsController(IMapsService mapsService)
        {
            _mapsService = mapsService;
        }
        
        [HttpGet]
        public async Task<ActionResult<GeocodeResponse>> Get(string address)
        {
            return await _mapsService.GetGeocode(address);
        }
    }
}
