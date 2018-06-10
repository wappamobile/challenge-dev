using AutoFixture;
using System;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Wappa.Api.Controllers;
using Wappa.Api.DomainModel;
using Wappa.Api.Requests;
using Xunit;
using Microsoft.AspNetCore.Http;
using NSubstitute;
using Wappa.Api.DataLayer;
using Microsoft.AspNetCore.Mvc.Core;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Wappa.Api.ExternalServices;
using Wappa.Api.Responses;

namespace Wappa.Api.Tests.ControllerTests
{
	public class DriversControllerTests
	{
		private static IGoogleGeocoderWrapper googleGeocoderWrapper;
		private static IUnitOfWork unitOfWork;

		private static DriversController controller;
		private static Fixture fixture;

		static DriversControllerTests()
		{
			googleGeocoderWrapper = Substitute.For<IGoogleGeocoderWrapper>();
			unitOfWork = Substitute.For<IUnitOfWork>();

			AutoMapper.Mapper.Initialize(cfg => cfg.AddProfile<MappingProfile>());


			controller = new DriversController(googleGeocoderWrapper, unitOfWork);
			fixture = new Fixture();
			RemoveThrowingBehaviorFromFixture();
		}

		private static void RemoveThrowingBehaviorFromFixture()
		{
			fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
							.ForEach(b => fixture.Behaviors.Remove(b));
			fixture.Behaviors.Add(new OmitOnRecursionBehavior());
		}

		[Fact]
		public async Task When_POST_a_Driver_should_return_Created_Driver_response_Async()
		{
			//Arrange
			MockGoogleGeocoderGetAddressReturn();

			var request = fixture.Create<CreateDriverRequest>();

			//Act
			var response = await controller.Post(request);
			var result = response.Result as CreatedResult;

			//Assert
			Assert.Equal(result.StatusCode, StatusCodes.Status201Created);
			Assert.IsType<CreatedDriverResponse>(result.Value);
		}

		private static void MockGoogleGeocoderGetAddressReturn(int howManyAddressesToMock = 1)
		{
			var googleAddresses = fixture.CreateMany<GoogleAddress>(howManyAddressesToMock);
			googleGeocoderWrapper.GetAddress(Arg.Any<String>()).Returns(googleAddresses.ToList());
		}

		[Fact]
		public async Task When_POST_a_null_Driver_should_return_BadRequest_response_Async()
		{
			//Arrange -> Act
			var response = await controller.Post(null);
			var result = response.Result as BadRequestResult;

			//Assert
			Assert.IsType<BadRequestResult>(result);
		}

		[Fact]
		public async Task When_POST_a_Driver_and_a_problem_occur_should_return_InternalServerError_response_Async()
		{
			//Arrange
			unitOfWork.DriversRepository.When(d => d.Add(Arg.Any<Driver>())).Throw<Exception>();
			var request = fixture.Create<CreateDriverRequest>();

			//Act
			var response = await controller.Post(request);
			var result = response.Result as ObjectResult;

			//Assert
			Assert.Equal(StatusCodes.Status500InternalServerError, result.StatusCode);
		}

		[Fact]
		public async Task After_POST_a_Driver_should_have_call_SaveChanges_on_UnitOfWork()
		{
			//Arrange
			MockGoogleGeocoderGetAddressReturn();

			var request = fixture.Create<CreateDriverRequest>();

			//Act
			var response = await controller.Post(request);

			//Assert
			await unitOfWork.Received().SaveChanges();
		}

		[Fact]
		public async Task When_POST_a_Driver_must_call_GoogleGeocodeWrapper()
		{
			//Arrange
			MockGoogleGeocoderGetAddressReturn();

			var request = fixture.Create<CreateDriverRequest>();

			//Act
			var response = await controller.Post(request);

			//Assert
			await googleGeocoderWrapper.Received().GetAddress(Arg.Any<String>());
		}

		[Fact]
		public async Task When_POST_a_Driver_and_GoogleGeocodeWrapper_returns_more_than_one_address_shoul_return_Conflict_Status()
		{
			//Arrange
			var numberOfAddressesToMock = 5;
			MockGoogleGeocoderGetAddressReturn(numberOfAddressesToMock);

			var request = fixture.Create<CreateDriverRequest>();

			//Act
			var response = await controller.Post(request);
			var result = response.Result as ObjectResult;

			//Assert
			Assert.Equal(StatusCodes.Status409Conflict, result.StatusCode);
		}

	}
}
