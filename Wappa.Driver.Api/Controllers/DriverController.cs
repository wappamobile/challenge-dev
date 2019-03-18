using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Wappa.Driver.Api.Consts;
using Wappa.Driver.Api.Data.Models;
using Wappa.Driver.Api.Dtos;
using Wappa.Driver.Api.Repositories.Interfaces;
using Wappa.Driver.Api.Services.Interfaces;

namespace Wappa.Driver.Api.Controllers
{
    /// <summary>
    /// Controller para gerenciar cadastro de motorista
    /// </summary>
    [Produces("application/json")]
    [Route("Driver")]
    public class DriverController : Controller
    {
        #region Private fields
        private readonly IDriverRepository<DriverDto> _driverRepository;
        private readonly IMapsService _map;
        #endregion

        #region Constructor
        public DriverController(IDriverRepository<DriverDto> driverRepository, IMapsService map)
        {
            _driverRepository = driverRepository;
            _map = map;
        }
        #endregion

        #region Methods
        [HttpPost]
        [Route("/InsertDriver")]
        public async Task<IActionResult> InsertDriver([FromBody]DriverDto driver)
        {
            var validation = ValidateDriver(driver, true);
            if (!validation.valid)
                return (StatusCode(StatusCodes.Status400BadRequest, validation.errorMessage));

            MapDto map = await _map.GetGeometry(driver.DriverAddressDto);
            if (map == null)
                return (StatusCode(StatusCodes.Status400BadRequest, ErrorMessage.AddressNotFound));

            driver.DriverAddressDto.Latitude = map.Latitude;
            driver.DriverAddressDto.Longitude = map.Longitude;

            var result = await _driverRepository.Insert(driver);
            return Ok(result);
        }

        [HttpPut]
        [Route("/UpdateDriver")]
        public async Task<IActionResult> UpdateDriver([FromBody]DriverDto driver)
        {
            var validation = ValidateDriver(driver, false);
            if (!validation.valid)
                return (StatusCode(StatusCodes.Status400BadRequest, validation.errorMessage));

            //Verifica se o motorista está cadastrado na base de dados
            var exists = await _driverRepository.Exists(driver.DriverId);
            if (!exists)
                return (StatusCode(StatusCodes.Status400BadRequest, ErrorMessage.DriverNotFound));

            MapDto map = await _map.GetGeometry(driver.DriverAddressDto);
            if (map == null)
                return (StatusCode(StatusCodes.Status400BadRequest, ErrorMessage.AddressNotFound));

            driver.DriverAddressDto.Latitude = map.Latitude;
            driver.DriverAddressDto.Longitude = map.Longitude;

            var result = await _driverRepository.Update(driver);
            return Ok(result);
        }

        [HttpGet]
        [Route("/GetDrivers")]
        public async Task<ActionResult> GetDrivers()
        {
            var result = await _driverRepository.GetDrivers();
            return Ok(result);
        }

        [HttpDelete]
        [Route("/DeleteDriver")]
        public async Task<ActionResult> DeleteDriver([FromQuery] int id)
        {
            //Verifica se o motorista está cadastrado na base de dados
            var exists = await _driverRepository.Exists(id);
            if (!exists)
                return (StatusCode(StatusCodes.Status400BadRequest, ErrorMessage.DriverNotFound));

            await _driverRepository.Delete(id);
            return Ok();
        }

        /// <summary>
        /// Valida os dados do motorista
        /// </summary>
        /// <param name="driver">Dados do motorista</param>
        /// <param name="insert">Indica se a validação é para insert ou update</param>
        /// <returns></returns>
        private (string errorMessage, bool valid) ValidateDriver(DriverDto driver, bool insert)
        {
            //Valida os dados do motorista
            if (driver == null)
                return (string.Format(ErrorMessage.FieldRequired, nameof(driver)), false);

            if (driver.DriverCarDto == null)
                return (string.Format(ErrorMessage.FieldRequired, nameof(driver.DriverCarDto)), false);

            if (driver.DriverAddressDto == null)
                return (string.Format(ErrorMessage.FieldRequired, nameof(driver.DriverAddressDto)), false);

            if (!insert)
            {
                if (driver.DriverId== 0)
                    return (string.Format(ErrorMessage.FieldRequired,  nameof(driver.DriverId)), false);
            }

            if (string.IsNullOrEmpty(driver.FirstName))
                return (string.Format(ErrorMessage.FieldRequired, nameof(driver.FirstName)), false);

            if (string.IsNullOrEmpty(driver.LastName))
                return (string.Format(ErrorMessage.FieldRequired, nameof(driver.LastName)), false);

            //Valida os dados do carro do motorista
            if (string.IsNullOrEmpty(driver.DriverCarDto.Make))
                return (string.Format(ErrorMessage.FieldRequired, nameof(driver.DriverCarDto.Make)), false);

            if (string.IsNullOrEmpty(driver.DriverCarDto.Model))
                return (string.Format(ErrorMessage.FieldRequired, nameof(driver.DriverCarDto.Model)), false);

            if (string.IsNullOrEmpty(driver.DriverCarDto.Plate))
                return (string.Format(ErrorMessage.FieldRequired, nameof(driver.DriverCarDto.Plate)), false);

            //Valida os dados do endereço do motorista
            if (string.IsNullOrEmpty(driver.DriverAddressDto.Address))
                return (string.Format(ErrorMessage.FieldRequired, nameof(driver.DriverAddressDto.Address)), false);

            if (string.IsNullOrEmpty(driver.DriverAddressDto.City))
                return (string.Format(ErrorMessage.FieldRequired, nameof(driver.DriverAddressDto.City)), false);

            if (string.IsNullOrEmpty(driver.DriverAddressDto.Country))
                return (string.Format(ErrorMessage.FieldRequired, nameof(driver.DriverAddressDto.Country)), false);

            if (string.IsNullOrEmpty(driver.DriverAddressDto.Neighborhood))
                return (string.Format(ErrorMessage.FieldRequired, nameof(driver.DriverAddressDto.Neighborhood)), false);

            if (string.IsNullOrEmpty(driver.DriverAddressDto.Number))
                return (string.Format(ErrorMessage.FieldRequired, nameof(driver.DriverAddressDto.Number)), false);

            if (string.IsNullOrEmpty(driver.DriverAddressDto.State))
                return (string.Format(ErrorMessage.FieldRequired, nameof(driver.DriverAddressDto.State)), false);

            return (null, true);
        }

        #endregion
    }
}

