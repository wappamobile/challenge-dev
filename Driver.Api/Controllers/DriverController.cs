using Driver.Api.ViewModels;
using Driver.Application.Data.Entities;
using Driver.Application.Data.Repositories.Interfaces;
using Driver.Application.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Driver.Api.Controllers
{
    /// <summary>
    /// Apis de manutenção de motorista
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class DriverController : ControllerBase
    {
        private readonly IDriverRepository _driverRepository;
        private readonly IGoogleApiService _googleApiService;

        /// <summary>
        /// Nova instancia do controller
        /// </summary>
        /// <param name="driverRepository"></param>
        /// <param name="googleApiService"></param>
        public DriverController(IDriverRepository driverRepository, IGoogleApiService googleApiService)
        {
            _driverRepository = driverRepository;
            _googleApiService = googleApiService;
        }

        /// <summary>
        /// Api responsável por lista os motoristas
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<GetDriverViewModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            var result = await _driverRepository.GetAsync();

            return Ok(result.ConvertAll(i => new GetDriverViewModel(i)));
        }

        /// <summary>
        /// Api responsável por buscar um motorista
        /// </summary>
        /// <param name="driverId">Id do motorista</param>
        /// <returns></returns>
        [HttpGet("{driverId}", Name = "GetDriverById")]
        [ProducesResponseType(typeof(GetDriverByIdViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(int driverId)
        {
            var result = await _driverRepository.GetBydIdAsync(driverId);

            if (result == null)
                return NotFound();

            return Ok(new GetDriverByIdViewModel(result));
        }

        /// <summary>
        /// Api responsável por incluir um novo motorista
        /// </summary>
        /// <param name="model">Dados do motorista para incluir</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(GetDriverByIdViewModel), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody] PostDriverViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _driverRepository.InsertAsync(await AddCoordinatesAsync(new DriverEntity
            {
                Address = model.Address.Address,
                AddressNumber = model.Address.Number,
                CarBrand = model.Car.Brand,
                CarLicensePlate = model.Car.LicensePlate,
                CarModel = model.Car.Model,
                City = model.Address.City,
                District = model.Address.District,
                FirstName = model.FirstName,
                LastName = model.LastName,
                State = model.Address.State,
                ZipCode = model.Address.ZipCode
            }));

            return CreatedAtRoute("GetDriverById", new { result.DriverId }, new GetDriverByIdViewModel(result));
        }

        /// <summary>
        /// Api responsável por atualizar um motorista
        /// </summary>
        /// <param name="driverId">Id do motorista</param>
        /// <param name="model">Dados do motorista para alterar</param>
        /// <returns></returns>
        [HttpPut("{driverId}")]
        [ProducesResponseType(typeof(GetDriverByIdViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Put(int driverId, [FromBody] PutDriverViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var curret = await _driverRepository.GetBydIdAsync(driverId);

            if (curret != null)
            {
                curret.Address = model.Address.Address;
                curret.AddressNumber = model.Address.Number;
                curret.CarBrand = model.Car.Brand;
                curret.CarLicensePlate = model.Car.LicensePlate;
                curret.CarModel = model.Car.Model;
                curret.City = model.Address.City;
                curret.District = model.Address.District;
                curret.FirstName = model.FirstName;
                curret.LastName = model.LastName;
                curret.State = model.Address.State;
                curret.ZipCode = model.Address.ZipCode;

                curret = await _driverRepository.UpdateAsync(await AddCoordinatesAsync(curret));

                return Ok(new GetDriverByIdViewModel(curret));
            }

            return NotFound();
        }

        /// <summary>
        /// Método responsável por buscar as coordenadas do endereço informado
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        private async Task<DriverEntity> AddCoordinatesAsync(DriverEntity entity)
        {
            var location = await _googleApiService.SearchAsync(entity);

            if (location.Status == "OK")
            {
                var address = location.Results.FirstOrDefault();

                if (address != null)
                {
                    entity.Latitude = address.Geometry?.Location?.Lat;
                    entity.Longitude = address.Geometry?.Location?.Lng;
                }
            }

            return entity;
        }

        /// <summary>
        /// Api responsásvel por apagar um motorista
        /// </summary>
        /// <param name="driverId">Id do motorista</param>
        /// <returns></returns>
        [HttpDelete("{driverId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int driverId)
        {
            var result = await _driverRepository.DeleteAsync(driverId);

            if (result)
                return Ok();

            return NotFound();
        }
    }
}