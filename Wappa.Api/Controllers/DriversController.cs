using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

		[HttpGet]
		public async Task<ActionResult<List<Driver>>> Get([FromQuery] String sortBy = DEFAULT_SORT_BY,
															[FromQuery] int limit = DEFAULT_DRIVER_GET_LIMIT, [FromQuery] int offset = DEFAULT_DRIVER_OFFSET)
		{
			try
			{
				if (AVAILABLE_SORT_BY_FILTERS.Contains(sortBy.ToLower()) == false) { return BadRequest(sortBy); }

				var drivers = await this.unitOfWork.DriversRepository.GetAll(sortBy, limit, offset);

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
			if (id.HasValue == false || id.Value == 0 ) { return this.BadRequest(id); }

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

		[HttpPost]
		public async Task<ActionResult<Driver>> Post([FromBody] CreateDriverRequest request)
		{
			if (request == null) { return this.BadRequest(); }

			try
			{
				var googlePossibleAddresses = await this.googleGeocoderWrapper.GetAddress(request.Address.ToString());

				if (this.HasMoreThanOnePossibleAddress(googlePossibleAddresses))
				{
					return this.StatusCode(StatusCodes.Status409Conflict, googlePossibleAddresses);
				}

				var driverAddressOnGoogle = googlePossibleAddresses.FirstOrDefault();
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

			var address = CreateAddress(request, driverAddressOnGoogle);
			driver.Address = address;

			return driver;
		}

		private Address CreateAddress(CreateDriverRequest request, GoogleAddress driverAddressOnGoogle)
		{
			var address = Mapper.Map<Address>(driverAddressOnGoogle);
			address.City = request.Address.City;
			address.PostalCode = request.Address.PostalCode;
			address.State = request.Address.State;

			return address;
		}

		private bool HasMoreThanOnePossibleAddress(IList<GoogleAddress> possibleAddressesOnGoogle)
		{
			return possibleAddressesOnGoogle.Count > 1;
		}
	}
}
