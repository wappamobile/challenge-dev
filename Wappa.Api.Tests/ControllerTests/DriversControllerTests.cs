using AutoFixture;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wappa.Api.Controllers;
using Wappa.Api.DataLayer;
using Wappa.Api.DomainModel;
using Wappa.Api.ExternalServices;
using Wappa.Api.Requests;
using Wappa.Api.Responses;
using Xunit;

namespace Wappa.Api.Tests.ControllerTests
{
	public class DriversControllerTests
	{
		private IGoogleGeocoderWrapper googleGeocoderWrapper;
		private IUnitOfWork unitOfWork;

		private DriversController controller;
		private Fixture fixture;

		static DriversControllerTests()
		{
			AutoMapper.Mapper.Initialize(cfg => cfg.AddProfile<MappingProfile>());
		}

		public DriversControllerTests()
		{
			this.googleGeocoderWrapper = Substitute.For<IGoogleGeocoderWrapper>();
			this.unitOfWork = Substitute.For<IUnitOfWork>();

			this.controller = new DriversController(googleGeocoderWrapper, this.unitOfWork);
			this.fixture = new Fixture();

			RemoveThrowingBehaviorFromFixture();
		}

		private void RemoveThrowingBehaviorFromFixture()
		{
			this.fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
							.ForEach(b => this.fixture.Behaviors.Remove(b));

			this.fixture.Behaviors.Add(new OmitOnRecursionBehavior());
		}

		[Fact]
		public async Task When_POST_a_Driver_should_return_Created_Driver_response_Async()
		{
			//Arrange
			MockGoogleGeocoderGetAddressReturn();

			var request = this.fixture.Create<CreateDriverRequest>();

			//Act
			var response = await this.controller.Post(request);
			var result = response.Result as CreatedResult;

			//Assert
			Assert.Equal(result.StatusCode, StatusCodes.Status201Created);
			Assert.IsType<CreatedDriverResponse>(result.Value);
		}

		private void MockGoogleGeocoderGetAddressReturn()
		{
			var googleAddresses = this.fixture.Create<GoogleAddress>();
			googleGeocoderWrapper.GetAddress(Arg.Any<String>()).Returns(googleAddresses);
		}

		[Fact]
		public async Task When_POST_a_null_Driver_should_return_BadRequest()
		{
			//Arrange -> Act
			var response = await this.controller.Post(null);
			var result = response.Result as BadRequestResult;

			//Assert
			Assert.IsType<BadRequestResult>(result);
		}

		[Fact]
		public async Task When_POST_a_Driver_and_a_problem_occur_should_return_InternalServerError()
		{
			//Arrange
			this.unitOfWork.DriversRepository.When(d => d.Add(Arg.Any<Driver>())).Throw<Exception>();

			var request = this.fixture.Create<CreateDriverRequest>();

			//Act
			var response = await this.controller.Post(request);
			var result = response.Result as ObjectResult;

			//Assert
			Assert.Equal(StatusCodes.Status500InternalServerError, result.StatusCode);
		}

		[Fact]
		public async Task When_POST_a_Driver_should_call_SaveChanges_on_UnitOfWork()
		{
			//Arrange
			MockGoogleGeocoderGetAddressReturn();

			var request = this.fixture.Create<CreateDriverRequest>();

			//Act
			var response = await this.controller.Post(request);

			//Assert
			this.unitOfWork.Received().SaveChanges();
		}

		[Fact]
		public async Task When_POST_a_Driver_must_call_GoogleGeocodeWrapper()
		{
			//Arrange
			MockGoogleGeocoderGetAddressReturn();

			var request = this.fixture.Create<CreateDriverRequest>();

			//Act
			var response = await this.controller.Post(request);

			//Assert
			await googleGeocoderWrapper.Received().GetAddress(Arg.Any<String>());
		}

		[Fact]
		public async Task When_DELETE_a_Driver_should_return_a_DriverResponse_with_OK_status_code()
		{
			//Arrange
			var driver = this.fixture.Create<Driver>();
			this.unitOfWork.DriversRepository.Get(Arg.Any<int>()).Returns(driver);

			//Act
			var response = await this.controller.Delete(driver.Id) as ActionResult<DriverResponse>;
			var result = response.Result as OkObjectResult;

			//Assert
			Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
			Assert.IsType<DriverResponse>(result.Value);
		}

		[Fact]
		public async Task When_DELETE_a_Driver_and_Id_is_invalid_should_return_a_BadRequest()
		{
			//Arrange -> Act
			var response = await this.controller.Delete(0);
			var result = response.Result as BadRequestObjectResult;

			//Assert
			Assert.IsType<BadRequestObjectResult>(result);
		}

		[Fact]
		public async Task When_DELETE_a_Driver_and_a_problem_occur_should_return_InternalServerError()
		{
			//Arrange
			var driverId = this.fixture.Create<int>();

			this.unitOfWork.DriversRepository.When(d => d.Get(Arg.Any<int>())).Throw<Exception>();

			//Act
			var response = await this.controller.Delete(driverId);
			var result = response.Result as ObjectResult;

			//Assert
			Assert.Equal(StatusCodes.Status500InternalServerError, result.StatusCode);
		}

		[Fact]
		public async Task When_try_to_DELETE_an_already_deleted_Driver_should_return_NotFound()
		{
			//Arrange
			var driverId = this.fixture.Create<int>();

			this.unitOfWork.DriversRepository.Get(Arg.Any<int>()).Returns(default(Driver));

			//Act
			var response = await this.controller.Delete(driverId);
			var result = response.Result as ObjectResult;

			//Assert
			Assert.Equal(StatusCodes.Status404NotFound, result.StatusCode);
		}

		[Fact]
		public async Task When_DELETE_a_Driver_should_call_SaveChange_on_UnitOfWork()
		{
			//Arrange
			var driver = this.fixture.Create<Driver>();

			this.unitOfWork.DriversRepository.Get(Arg.Any<int>()).Returns(driver);

			//Act
			var response = await this.controller.Delete(driver.Id);

			//Assert
			this.unitOfWork.Received().SaveChanges();
		}

		[Fact]
		public async Task When_GET_all_Drivers_should_return_a_list_with_OK_status_code()
		{
			//Arrange
			var howManyDriversToCreate = 50;
			var drivers = new List<Driver>(this.fixture.CreateMany<Driver>(howManyDriversToCreate));

			this.unitOfWork.DriversRepository.GetAll(Arg.Any<String>(), Arg.Any<int>(), Arg.Any<int>())
				.Returns(drivers);

			//Act
			var response = await this.controller.Get() as ActionResult<List<Driver>>;
			var result = response.Result as OkObjectResult;

			//Assert
			Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
			Assert.IsType<List<DriverResponse>>(result.Value);
		}

		[Fact]
		public async Task When_GET_all_Drivers_and_a_problem_occur_should_return_InternalServerError()
		{
			//Arrange
			this.unitOfWork.DriversRepository.When(d => d.GetAll(Arg.Any<String>(), Arg.Any<int>(), Arg.Any<int>())).Throw<Exception>();

			//Act
			var response = await this.controller.Get();
			var result = response.Result as ObjectResult;

			//Assert
			Assert.Equal(StatusCodes.Status500InternalServerError, result.StatusCode);
		}

		[Fact]
		public async Task When_GET_all_Drivers_and_SortBy_query_parameter_is_not_FirstName_or_LastName_should_return_BadRequest()
		{
			//Arrange -> Act
			var response = await this.controller.Get(sortBy: this.fixture.Create<String>());
			var result = response.Result as BadRequestObjectResult;

			//Assert
			Assert.IsType<BadRequestObjectResult>(result);
		}

		[Fact]
		public async Task When_GET_all_Drivers_and_SortBy_is_FirstName_or_LastName_should_return_a_list_with_OK_status_code()
		{
			//Arrange
			var options = new String[] { "FirstName", "LastName" };
			var randomOption = options[new Random().Next(2)];

			var howManyDriversToCreate = 10;
			var drivers = new List<Driver>(this.fixture.CreateMany<Driver>(howManyDriversToCreate));

			this.unitOfWork.DriversRepository.GetAll(Arg.Any<String>(), Arg.Any<int>(), Arg.Any<int>())
				.Returns(drivers);

			//Act
			var response = await this.controller.Get(sortBy: randomOption) as ActionResult<List<Driver>>;
			var result = response.Result as OkObjectResult;

			//Assert
			Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
			Assert.IsType<List<DriverResponse>>(result.Value);
		}

		[Fact]
		public async Task When_GET_all_Drivers_should_not_matter_how_SortBy_is_written_and_must_return_a_list_with_OK_status_code()
		{
			//Arrange
			var options = new String[] { "firStnamE", "lAstnAme" };
			var randomOption = options[new Random().Next(2)];

			var howManyDriversToCreate = 10;
			var drivers = new List<Driver>(this.fixture.CreateMany<Driver>(howManyDriversToCreate));

			this.unitOfWork.DriversRepository.GetAll(Arg.Any<String>(), Arg.Any<int>(), Arg.Any<int>())
				.Returns(drivers);

			//Act
			var response = await this.controller.Get(sortBy: randomOption) as ActionResult<List<Driver>>;
			var result = response.Result as OkObjectResult;

			//Assert
			Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
			Assert.IsType<List<DriverResponse>>(result.Value);
		}

		[Fact]
		public async Task When_GET_a_Driver_should_return_a_DriverResponse()
		{
			//Arrange
			var driver = this.fixture.Create<Driver>();
			this.unitOfWork.DriversRepository.Get(Arg.Any<int>()).Returns(driver);

			//Act
			var response = await this.controller.Get(driver.Id) as ActionResult<DriverResponse>;
			var result = response.Result as OkObjectResult;

			//Assert
			Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
			Assert.IsType<DriverResponse>(result.Value);
		}

		[Fact]
		public async Task When_GET_a_specific_Driver_and_Id_is_invalid_should_return_a_BadRequest()
		{
			//Arrange -> Act
			var response = await this.controller.Get(0);
			var result = response.Result as BadRequestObjectResult;

			//Assert
			Assert.IsType<BadRequestObjectResult>(result);
		}

		[Fact]
		public async Task When_GET_a_specific_Driver_and_a_problem_occur_should_return_a_InternalServerError()
		{
			//Arrange
			this.unitOfWork.DriversRepository.When(d => d.Get(Arg.Any<int>())).Throw<Exception>();

			//Act
			var response = await this.controller.Get(10);
			var result = response.Result as ObjectResult;

			//Assert
			Assert.Equal(StatusCodes.Status500InternalServerError, result.StatusCode);
		}

		[Fact]
		public async Task When_GET_a_Driver_Address_should_return_a_Address_as_response_with_Ok_status_code()
		{
			//Arrange
			var driver = this.fixture.Create<Driver>();
			this.unitOfWork.DriversRepository.Get(Arg.Any<int>()).Returns(driver);

			//Act
			var response = await this.controller.GetDriverAddress(driver.Id) as ActionResult<Models.Address>;
			var result = response.Result as OkObjectResult;

			//Assert
			Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
			Assert.IsType<Models.Address>(result.Value);
		}

		[Fact]
		public async Task When_GET_a_Driver_Address_and_Id_is_invalid_should_return_a_BadRequest()
		{
			//Arrange -> Act
			var response = await this.controller.GetDriverAddress(0);
			var result = response.Result as BadRequestObjectResult;

			//Assert
			Assert.IsType<BadRequestObjectResult>(result);
		}

		[Fact]
		public async Task When_GET_a_Driver_Address_and_it_not_exist_should_return_a_NoContent()
		{
			//Arrange
			var driverId = this.fixture.Create<int>();

			this.unitOfWork.DriversRepository.Get(Arg.Any<int>()).Returns(default(Driver));

			//Act
			var response = await this.controller.GetDriverAddress(driverId) as ActionResult<Models.Address>;
			var result = response.Result as NoContentResult;

			//Assert
			Assert.Equal(StatusCodes.Status204NoContent, result.StatusCode);
		}

		[Fact]
		public async Task When_GET_a_Driver_Address_and_a_problem_occur_should_return_a_InternalServerError()
		{
			//Arrange
			var driverId = this.fixture.Create<int>();

			this.unitOfWork.DriversRepository.When(d => d.Get(Arg.Any<int>())).Throw<Exception>();

			//Act
			var response = await this.controller.GetDriverAddress(driverId);
			var result = response.Result as ObjectResult;

			//Assert
			Assert.Equal(StatusCodes.Status500InternalServerError, result.StatusCode);
			Assert.IsType<ObjectResult>(result);
		}

		[Fact]
		public async Task When_GET_a_Driver_Cars_should_return_a_CarResponse_as_response_with_Ok_status_code()
		{
			//Arrange
			var driver = this.fixture.Create<Driver>();
			this.unitOfWork.DriversRepository.Get(Arg.Any<int>()).Returns(driver);

			//Act
			var response = await this.controller.GetDriverCars(driver.Id) as ActionResult<List<Models.Car>>;
			var result = response.Result as OkObjectResult;

			//Assert
			Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
			Assert.IsType<List<Models.Car>>(result.Value);
		}

		[Fact]
		public async Task When_GET_a_Driver_Cars_and_Id_is_invalid_should_return_a_BadRequest()
		{
			//Arrange -> Act
			var response = await this.controller.GetDriverAddress(0);
			var result = response.Result as BadRequestObjectResult;

			//Assert
			Assert.IsType<BadRequestObjectResult>(result);
		}

		[Fact]
		public async Task When_GET_a_Driver_Cars_and_it_not_exist_should_return_a_NoContent()
		{
			//Arrange
			var driverId = this.fixture.Create<int>();

			this.unitOfWork.DriversRepository.Get(Arg.Any<int>()).Returns(default(Driver));

			//Act
			var response = await this.controller.GetDriverCars(driverId) as ActionResult<List<Models.Car>>;
			var result = response.Result as NoContentResult;

			//Assert
			Assert.Equal(StatusCodes.Status204NoContent, result.StatusCode);
		}

		[Fact]
		public async Task When_GET_a_Driver_Cars_and_a_problem_occur_should_return_a_InternalServerError()
		{
			//Arrange
			var driverId = this.fixture.Create<int>();

			this.unitOfWork.DriversRepository.When(d => d.Get(Arg.Any<int>())).Throw<Exception>();

			//Act
			var response = await this.controller.GetDriverAddress(driverId);
			var result = response.Result as ObjectResult;

			//Assert
			Assert.Equal(StatusCodes.Status500InternalServerError, result.StatusCode);
			Assert.IsType<ObjectResult>(result);
		}

		[Fact]
		public async Task When_PUT_a_Driver_should_return_an_updated_DriverResponse_with_Ok_status_code()
		{
			//Arrange
			var driver = this.fixture.Create<Driver>();
			var updatedDriver = this.fixture.Create<UpdateDriverRequest>();

			this.unitOfWork.DriversRepository.Get(Arg.Any<int>()).Returns(driver);

			MockGoogleGeocoderGetAddressReturn();

			//Act
			var response = await this.controller.Put(updatedDriver) as ActionResult<DriverResponse>;
			var result = response.Result as OkObjectResult;

			//Assert
			Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
			Assert.IsType<DriverResponse>(result.Value);
		}

		[Fact]
		public async Task When_PUT_a_Driver_and_request_is_invalid_should_return_a_BadRequest()
		{
			//Arrange -> Act
			var response = await this.controller.Put(null);
			var result = response.Result as BadRequestObjectResult;

			//Assert
			Assert.IsType<BadRequestObjectResult>(result);
		}

		[Fact]
		public async Task When_PUT_a_Driver_and_request_Car_list_is_null_should_return_a_BadRequest()
		{
			//Arrange
			var updatedDriver = this.fixture.Create<UpdateDriverRequest>();
			updatedDriver.Cars = null;

			MockGoogleGeocoderGetAddressReturn();

			//Act
			var response = await this.controller.Put(updatedDriver);
			var result = response.Result as BadRequestObjectResult;

			//Assert
			Assert.IsType<BadRequestObjectResult>(result);
		}

		[Fact]
		public async Task When_PUT_a_Driver_and_request_Car_list_has_Zero_elements_should_return_a_BadRequest()
		{
			//Arrange
			var updatedDriver = this.fixture.Create<UpdateDriverRequest>();
			updatedDriver.Cars = new List<Models.Car>();

			MockGoogleGeocoderGetAddressReturn();

			//Act
			var response = await this.controller.Put(updatedDriver);
			var result = response.Result as BadRequestObjectResult;

			//Assert
			Assert.IsType<BadRequestObjectResult>(result);
		}

		[Fact]
		public async Task When_PUT_a_Driver_and_a_problem_occur_should_return_InternalServerError()
		{
			//Arrange
			var updatedDriver = this.fixture.Create<UpdateDriverRequest>();

			var driver = this.fixture.Create<Driver>();
			this.unitOfWork.DriversRepository.Get(Arg.Any<int>()).Returns(driver);

			this.unitOfWork.DriversRepository.When(d => d.Update(Arg.Any<Driver>())).Throw<Exception>();

			//Act
			var response = await this.controller.Put(updatedDriver);
			var result = response.Result as ObjectResult;

			//Assert
			Assert.Equal(StatusCodes.Status500InternalServerError, result.StatusCode);
		}

		[Fact]
		public async Task When_try_to_PUT_an_already_deleted_Driver_should_return_NotFound()
		{
			//Arrange
			var updatedDriver = this.fixture.Create<UpdateDriverRequest>();

			this.unitOfWork.DriversRepository.Get(Arg.Any<int>()).Returns(default(Driver));
			this.unitOfWork.DriversRepository.Update(Arg.Any<Driver>()).Returns(default(Task));

			//Act
			var response = await this.controller.Put(updatedDriver);
			var result = response.Result as ObjectResult;

			//Assert
			Assert.Equal(StatusCodes.Status404NotFound, result.StatusCode);
		}

		[Fact]
		public async Task When_PUT_a_Driver_should_call_SaveChanges_on_UnitOfWork()
		{
			//Arrange
			var driver = this.fixture.Create<Driver>();
			var updatedDriver = this.fixture.Create<UpdateDriverRequest>();

			this.unitOfWork.DriversRepository.Get(Arg.Any<int>()).Returns(driver);

			MockGoogleGeocoderGetAddressReturn();

			//Act
			var response = await this.controller.Put(updatedDriver);

			//Assert
			this.unitOfWork.Received().SaveChanges();
		}

		[Fact]
		public async Task When_PUT_a_Driver_should_call_GetAddress_on_GoogleGeocoderWrapper()
		{
			//Arrange
			var driver = this.fixture.Create<Driver>();
			var updatedDriver = this.fixture.Create<UpdateDriverRequest>();

			this.unitOfWork.DriversRepository.Get(Arg.Any<int>()).Returns(driver);

			//Act
			var response = await this.controller.Put(updatedDriver);

			//Assert
			await googleGeocoderWrapper.Received().GetAddress(Arg.Any<String>());
		}

		[Fact]
		public async Task When_PUT_a_Driver_Address_should_return_an_updated_Address_with_Ok_status_code()
		{
			//Arrange
			var driver = this.fixture.Create<Driver>();
			var updatedDriverAddress = this.fixture.Create<UpdateDriverAddressRequest>();

			MockGoogleGeocoderGetAddressReturn();

			this.unitOfWork.DriversRepository.Get(Arg.Any<int>()).Returns(driver);

			//Act
			var response = await this.controller.PutAddress(driver.Id, updatedDriverAddress) as ActionResult<Models.Address>;
			var result = response.Result as OkObjectResult;

			//Assert
			Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
			Assert.IsType<Models.Address>(result.Value);
		}

		[Fact]
		public async Task When_PUT_a_Driver_Address_and_request_is_invalid_should_return_a_BadRequest()
		{
			//Arrange
			var driverId = this.fixture.Create<int>();

			//Act
			var response = await this.controller.PutAddress(driverId, null);
			var result = response.Result as BadRequestObjectResult;

			//Assert
			Assert.IsType<BadRequestObjectResult>(result);
		}

		[Fact]
		public async Task When_PUT_a_Driver_Address_and_a_problem_occur_should_return_InternalServerError()
		{
			//Arrange
			var driverId = this.fixture.Create<int>();
			var updatedDriverAddress = this.fixture.Create<UpdateDriverAddressRequest>();

			this.unitOfWork.AddressRepository.When(d => d.Update(Arg.Any<int>(), Arg.Any<Address>())).Throw<Exception>();

			//Act
			var response = await this.controller.PutAddress(driverId, updatedDriverAddress);
			var result = response.Result as ObjectResult;

			//Assert
			Assert.Equal(StatusCodes.Status500InternalServerError, result.StatusCode);
		}

		[Fact]
		public async Task When_PUT_a_Driver_Address_for_a_Driver_that_dont_exist_should_return_NotFound()
		{
			//Arrange
			var driverId = this.fixture.Create<int>();
			var updatedDriverAddress = this.fixture.Create<UpdateDriverAddressRequest>();

			MockGoogleGeocoderGetAddressReturn();

			this.unitOfWork.DriversRepository.Get(Arg.Any<int>()).Returns(default(Driver));
			this.unitOfWork.AddressRepository.When(d => d.Update(Arg.Any<int>(), Arg.Any<Address>())).Throw<Exception>();

			//Act
			var response = await this.controller.PutAddress(driverId, updatedDriverAddress);
			var result = response.Result as NotFoundObjectResult;

			//Assert
			Assert.Equal(StatusCodes.Status404NotFound, result.StatusCode);
		}

		[Fact]
		public async Task When_PUT_a_Driver_Address_should_call_SaveChanges_on_UnitOfWork()
		{
			//Arrange
			var driver = this.fixture.Create<Driver>();
			var updatedDriverAddress = this.fixture.Create<UpdateDriverAddressRequest>();

			MockGoogleGeocoderGetAddressReturn();

			this.unitOfWork.DriversRepository.Get(Arg.Any<int>()).Returns(driver);

			//Act
			var response = await this.controller.PutAddress(driver.Id, updatedDriverAddress);

			//Assert
			this.unitOfWork.Received().SaveChanges();
		}

		[Fact]
		public async Task When_PUT_a_Driver_Address_should_call_GetAddress_GoogleGeocoderWrapper()
		{
			//Arrange
			var driverId = this.fixture.Create<int>();

			var updatedDriverAddress = this.fixture.Create<UpdateDriverAddressRequest>();

			//Act
			var response = await this.controller.PutAddress(driverId, updatedDriverAddress);

			//Assert
			await googleGeocoderWrapper.Received().GetAddress(Arg.Any<String>());
		}

		[Fact]
		public async Task When_PUT_a_Driver_Cars_should_return_an_updated_CarList_with_Ok_status_code()
		{
			//Arrange
			var driver = this.fixture.Create<Driver>();

			var updatedDriverCars = this.fixture.CreateMany<UpdateDriverCarRequest>().ToList();

			this.unitOfWork.DriversRepository.Get(Arg.Any<int>()).Returns(driver);

			//Act
			var response = await this.controller.PutCars(driver.Id, updatedDriverCars) as ActionResult<List<Models.Car>>;
			var result = response.Result as OkObjectResult;

			//Assert
			Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
			Assert.IsType<List<Models.Car>>(result.Value);
		}

		[Fact]
		public async Task When_PUT_a_Driver_Cars_and_request_is_invalid_should_return_a_BadRequest()
		{
			//Arrange 
			var driverId = this.fixture.Create<int>();

			//Act
			var response = await this.controller.PutCars(driverId, null);
			var result = response.Result as BadRequestObjectResult;

			//Assert
			Assert.IsType<BadRequestObjectResult>(result);
		}

		[Fact]
		public async Task When_PUT_a_Driver_Cars_and_request_Cars_is_null_should_return_a_BadRequest()
		{
			//Arrange
			var driverId = this.fixture.Create<int>();

			var updatedDriverCars = default(List<UpdateDriverCarRequest>);

			MockGoogleGeocoderGetAddressReturn();

			//Act
			var response = await this.controller.PutCars(driverId, updatedDriverCars);
			var result = response.Result as BadRequestObjectResult;

			//Assert
			Assert.IsType<BadRequestObjectResult>(result);
		}

		[Fact]
		public async Task When_PUT_a_Driver_Cars_and_a_problem_occur_should_return_InternalServerError()
		{
			//Arrange
			var driver = this.fixture.Create<Driver>();

			var updatedDriverCars = this.fixture.CreateMany<UpdateDriverCarRequest>().ToList();

			this.unitOfWork.DriversRepository.Get(Arg.Any<int>()).Returns(driver);
			this.unitOfWork.CarRepository.When(d => d.Update(Arg.Any<int>(), Arg.Any<List<Car>>())).Throw<Exception>();

			//Act
			var response = await this.controller.PutCars(driver.Id, updatedDriverCars);
			var result = response.Result as ObjectResult;

			//Assert
			Assert.Equal(StatusCodes.Status500InternalServerError, result.StatusCode);
		}

		[Fact]
		public async Task When_PUT_a_Driver_Cars_for_a_Driver_that_dont_exist_should_return_NotFound()
		{
			//Arrange
			var driverId = this.fixture.Create<int>();
			var driverCars = this.fixture.CreateMany<UpdateDriverCarRequest>().ToList();
			MockGoogleGeocoderGetAddressReturn();

			this.unitOfWork.DriversRepository.Get(Arg.Any<int>()).Returns(default(Driver));
			this.unitOfWork.AddressRepository.When(d => d.Update(Arg.Any<int>(), Arg.Any<Address>())).Throw<Exception>();

			//Act
			var response = await this.controller.PutCars(driverId, driverCars);
			var result = response.Result as NotFoundObjectResult;

			//Assert
			Assert.Equal(StatusCodes.Status404NotFound, result.StatusCode);
		}

		[Fact]
		public async Task When_PUT_a_Driver_Cars_should_call_SaveChanges_on_UnitOfWork()
		{
			//Arrange
			var driver = this.fixture.Create<Driver>();

			var updatedDriverCars = this.fixture.CreateMany<UpdateDriverCarRequest>().ToList();

			this.unitOfWork.DriversRepository.Get(Arg.Any<int>()).Returns(driver);

			//Act
			var response = await this.controller.PutCars(driver.Id, updatedDriverCars);

			//Assert
			this.unitOfWork.Received().SaveChanges();
		}
	}
}
