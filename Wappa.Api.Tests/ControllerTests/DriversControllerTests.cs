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
using Microsoft.EntityFrameworkCore;

namespace Wappa.Api.Tests.ControllerTests
{
	public class DriversControllerTests
	{
		private static IUnitOfWork unitOfWork;
		private static DriversController controller;
		private static Fixture fixture;

		static DriversControllerTests()
		{
			unitOfWork = Substitute.For<IUnitOfWork>();
			AutoMapper.Mapper.Initialize(cfg => cfg.AddProfile<MappingProfile>());

			controller = new DriversController(unitOfWork);
			fixture = new Fixture();
		}

		[Fact]
		public async Task When_POST_a_Driver_should_return_Created_Driver_response_Async()
		{
			//Arrange
			var request = fixture.Create<CreateDriverRequest>();

			//Act
			var response = await controller.Post(request);
			var result = response.Result as CreatedAtRouteResult;

			//Assert
			Assert.Equal(result.StatusCode, StatusCodes.Status201Created);
			Assert.IsType<Driver>(result.Value);
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
			var request = fixture.Create<CreateDriverRequest>();

			//Act
			var response = await controller.Post(request);

			//Assert
			await unitOfWork.Received().SaveChanges();
		}
	}
}
