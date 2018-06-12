using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wappa.Api.DataLayer;
using Wappa.Api.DomainModel;
using Wappa.Api.ExternalServices;
using Wappa.Api.Requests;
using Wappa.Api.Responses;

namespace Wappa.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class DriversController : ControllerBase
	{
		private const int DEFAULT_DRIVER_GET_LIMIT = 25;
		private const int DEFAULT_DRIVER_OFFSET = 0;
		private const String DEFAULT_SORT_BY = "firstname";

		private static readonly List<String> AVAILABLE_SORT_BY_FILTERS = new List<String> { "firstname", "lastname" };

		private IGoogleGeocoderWrapper googleGeocoderWrapper;
		private IUnitOfWork unitOfWork;

		public DriversController(IGoogleGeocoderWrapper googleGeocoderWrapper, IUnitOfWork unitOfWork)
		{
			this.googleGeocoderWrapper = googleGeocoderWrapper;
			this.unitOfWork = unitOfWork;
		}

		[HttpPost]
		public async Task<ActionResult<Driver>> Post([FromBody] CreateDriverRequest request)
		{
			if (request == null) { return this.BadRequest(); }

			try
			{
				var driverAddressOnGoogle = await this.googleGeocoderWrapper.GetAddress(request.Address.ToString());

				var driver = CreateDriverFromRequestAndGoogleAddress(request, driverAddressOnGoogle);

				this.unitOfWork.DriversRepository.Add(driver);
				await this.unitOfWork.SaveChanges();

				var response = Mapper.Map<CreatedDriverResponse>(driver);

				return this.Created(nameof(Post), response);
			}
			catch (Exception ex)
			{
				return this.StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
			}
		}

		private Driver CreateDriverFromRequestAndGoogleAddress(CreateDriverRequest request, GoogleAddress driverAddressOnGoogle)
		{
			var driver = Mapper.Map<Driver>(request);

			var address = this.MergeGoogleAddressWithRequestAddress(driverAddressOnGoogle, request.Address);
			driver.Address = address;

			return driver;
		}

		private Address MergeGoogleAddressWithRequestAddress(GoogleAddress driverAddressOnGoogle, Models.Address address)
		{
			var updatedAddress = Mapper.Map<Address>(driverAddressOnGoogle);

			updatedAddress.Id = address.Id;
			updatedAddress.City = address.City;
			updatedAddress.PostalCode = address.PostalCode;
			updatedAddress.State = address.State;

			return updatedAddress;
		}

		[HttpGet]
		public async Task<ActionResult<List<Driver>>> Get([FromQuery] String sortBy = DEFAULT_SORT_BY,
															[FromQuery] int limit = DEFAULT_DRIVER_GET_LIMIT, 
															[FromQuery] int offset = DEFAULT_DRIVER_OFFSET)
		{
			try
			{
				var normalizedSortBy = sortBy.ToLowerInvariant();
				if (AVAILABLE_SORT_BY_FILTERS.Contains(normalizedSortBy) == false) { return BadRequest(sortBy); }

				var drivers = await this.unitOfWork.DriversRepository.GetAll(normalizedSortBy, limit, offset);

				return this.Ok(Mapper.Map<ICollection<DriverResponse>>(drivers));
			}
			catch (Exception ex)
			{
				return this.StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
			}
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<DriverResponse>> Get(Nullable<int> id)
		{
			if (id.HasValue == false || id.Value == 0) { return this.BadRequest(id); }

			try
			{
				var driver = await this.unitOfWork.DriversRepository.Get(id.Value);

				return this.Ok(Mapper.Map<DriverResponse>(driver));
			}
			catch (Exception ex)
			{
				return this.StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
			}
		}

		[HttpGet("{id}/address")]
		public async Task<ActionResult<Models.Address>> GetDriverAddress(int id)
		{
			if (id == 0) { return this.BadRequest(id); }

			try
			{
				var driver = await this.unitOfWork.DriversRepository.Get(id);
				if (driver == null) { return this.NoContent(); }

				var address = Mapper.Map<Models.Address>(driver.Address);

				return this.Ok(address);
			}
			catch (Exception ex)
			{
				return this.StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
			}

		}

		[HttpGet("{id}/cars")]
		public async Task<ActionResult<List<Models.Car>>> GetDriverCars(int id)
		{
			if (id == 0) { return this.BadRequest(id); }

			try
			{
				var driver = await this.unitOfWork.DriversRepository.Get(id);
				if (driver == null || driver.Cars.Count == 0) { return this.NoContent(); }

				var cars = Mapper.Map<List<Models.Car>>(driver.Cars);

				return this.Ok(cars);
			}
			catch (Exception ex)
			{
				return this.StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
			}
		}

		[HttpPut]
		public async Task<ActionResult<DriverResponse>> Put([FromBody] UpdateDriverRequest request)
		{
			if (request == null) { return this.BadRequest(request); }
			if (request.Cars == null || request.Cars.Count() == 0) { return this.BadRequest(request); }

			try
			{
				var driver = Mapper.Map<Driver>(request);

				var driverAddressOnGoogle = await this.googleGeocoderWrapper.GetAddress(request.Address.ToString());
				driver.Address = this.MergeGoogleAddressWithRequestAddress(driverAddressOnGoogle, request.Address);

				await this.unitOfWork.DriversRepository.Update(driver);
				await this.unitOfWork.SaveChanges();

				return this.Ok(Mapper.Map<DriverResponse>(driver));
			}
			catch (Exception ex)
			{
				return this.StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
			}
		}

		[HttpPut("{id}/address")]
		public async Task<ActionResult<Models.Address>> PutAddress([FromBody] UpdateDriverAddressRequest request)
		{
			if (request == null) { return this.BadRequest(request); };

			try
			{
				var address = Mapper.Map<Models.Address>(request);

				var driverAddressOnGoogle = await this.googleGeocoderWrapper.GetAddress(address.ToString());
				var updatedAddress = this.MergeGoogleAddressWithRequestAddress(driverAddressOnGoogle, address);

				await this.unitOfWork.AddressRepository.Update(updatedAddress);
				await this.unitOfWork.SaveChanges();

				return this.Ok(Mapper.Map<Models.Address>(updatedAddress));
			}
			catch (Exception ex)
			{
				return this.StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
			}
		}

		[HttpPut("{id}/cars")]
		public async Task<ActionResult<List<Models.Car>>> PutCar([FromBody] List<UpdateDriverCarRequest> request)
		{
			if (request == null) { return this.BadRequest(request); }
			if (request == null || request.Count() == 0) { return this.BadRequest(request); }

			try
			{
				var cars = Mapper.Map<List<Car>>(request);

				await this.unitOfWork.CarRepository.Update(cars);
				await this.unitOfWork.SaveChanges();

				return this.Ok(Mapper.Map<List<Models.Car>>(cars));
			}
			catch (Exception ex)
			{
				return this.StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
			}
		}

		[HttpDelete("{id}")]
		public async Task<ActionResult<DriverResponse>> Delete(int id)
		{
			if (id == 0) { return this.BadRequest(id); }

			try
			{
				var driver = await this.unitOfWork.DriversRepository.Get(id);
				if (driver == null) { return this.NotFound(id); }

				await this.unitOfWork.DriversRepository.Delete(driver);
				await this.unitOfWork.SaveChanges();

				return this.Ok(Mapper.Map<DriverResponse>(driver));
			}
			catch (Exception ex)
			{
				return this.StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
			}
		}
	}
}
