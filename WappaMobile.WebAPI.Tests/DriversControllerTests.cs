using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WappaMobile.Application;
using WappaMobile.Domain;
using WappaMobile.WebAPI.Controllers;
using Xunit;

namespace WappaMobile.WebAPI.Tests
{
    public class DriversControllerTests
    {
        [Fact]
        public async Task ListAsync_WithLastNameSorting_SendsQueryAndReturnsOk()
        {
            // Arrange
            IEnumerable<ViewDriverDto> expectedResult = new ViewDriverDto[] { };
            var mediator = new Mock<IMediator>();
            mediator
                .Setup(m => m.Send(It.Is<ListDriversQuery>(q => q.OrderBy == ListDriversQuery.Sorting.LastName), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(expectedResult))
                .Verifiable();
            var controller = new DriversController(mediator.Object);

            // Act
            var response = await controller.ListAsync("last");

            // Assert
            mediator.Verify();
            Assert.IsType<OkObjectResult>(response.Result);
            Assert.Equal(expectedResult, ((OkObjectResult)response.Result).Value);
        }

        [Fact]
        public async Task ListAsync_WithNoSorting_SendsQueryAndReturnsOk()
        {
            // Arrange
            IEnumerable<ViewDriverDto> expectedResult = new ViewDriverDto[] { };
            var mediator = new Mock<IMediator>();
            mediator
                .Setup(m => m.Send(It.Is<ListDriversQuery>(q => q.OrderBy == ListDriversQuery.Sorting.FirstName), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(expectedResult))
                .Verifiable();
            var controller = new DriversController(mediator.Object);

            // Act
            var response = await controller.ListAsync("");

            // Assert
            mediator.Verify();
            Assert.IsType<OkObjectResult>(response.Result);
            Assert.Equal(expectedResult, ((OkObjectResult)response.Result).Value);
        }

        [Fact]
        public async Task GetAsync_WithExistingGuid_SendsQueryAndReturnsOk()
        {
            // Arrange
            var guid = Guid.NewGuid();
            var expectedResult = new ViewDriverDto { };
            var mediator = new Mock<IMediator>();
            mediator
                .Setup(m => m.Send(It.Is<GetDriverQuery>(q => q.DriverId == guid), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(expectedResult))
                .Verifiable();
            var controller = new DriversController(mediator.Object);

            // Act
            var response = await controller.GetAsync(guid);

            // Assert
            mediator.Verify();
            Assert.IsType<OkObjectResult>(response.Result);
            Assert.Equal(expectedResult, ((OkObjectResult)response.Result).Value);
        }

        [Fact]
        public async Task GetAsync_WithNonExistingGuid_SendsQueryAndReturnsNotFound()
        {
            // Arrange
            var guid = Guid.NewGuid();
            var expectedResult = new ViewDriverDto { };
            var mediator = new Mock<IMediator>();
            mediator
                .Setup(m => m.Send(It.Is<GetDriverQuery>(q => q.DriverId == guid), It.IsAny<CancellationToken>()))
                .Returns(Task.FromException<ViewDriverDto>(new NotFoundException("Just not found")))
                .Verifiable();
            var controller = new DriversController(mediator.Object);

            // Act
            var response = await controller.GetAsync(guid);

            // Assert
            mediator.Verify();
            Assert.IsType<NotFoundObjectResult>(response.Result);
            Assert.Equal("Just not found", ((NotFoundObjectResult)response.Result).Value);
        }

        [Fact]
        public async Task PostAsync_WithData_SendsCommandAndReturnsOk()
        {
            // Arrange
            var data = new ModifyDriverDto { };
            var mediator = new Mock<IMediator>();
            mediator
                .Setup(m => m.Send(It.Is<CreateDriverCommand>(q => q.DriverDto == data), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(Unit.Value))
                .Verifiable();
            var controller = new DriversController(mediator.Object);

            // Act
            var response = await controller.PostAsync(data);

            // Assert
            mediator.Verify();
            Assert.IsType<OkResult>(response);
        }

        [Fact]
        public async Task PutAsync_WithDataAndExistingGuid_SendsCommandAndReturnsOk()
        {
            // Arrange
            var guid = Guid.NewGuid();
            var data = new ModifyDriverDto { };
            var mediator = new Mock<IMediator>();
            mediator
                .Setup(m => m.Send(It.Is<UpdateDriverCommand>(q => q.DriverDto == data && q.DriverId == guid), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(Unit.Value))
                .Verifiable();
            var controller = new DriversController(mediator.Object);

            // Act
            var response = await controller.PutAsync(guid, data);

            // Assert
            mediator.Verify();
            Assert.IsType<OkResult>(response);
        }

        [Fact]
        public async Task PutAsync_WithDataAndNonExistingGuid_SendsCommandAndReturnsNotFound()
        {
            // Arrange
            var guid = Guid.NewGuid();
            var data = new ModifyDriverDto { };
            var mediator = new Mock<IMediator>();
            mediator
                .Setup(m => m.Send(It.Is<UpdateDriverCommand>(q => q.DriverDto == data && q.DriverId == guid), It.IsAny<CancellationToken>()))
                .Returns(Task.FromException<Unit>(new NotFoundException("Just not found")))
                .Verifiable();
            var controller = new DriversController(mediator.Object);

            // Act
            var response = await controller.PutAsync(guid, data);

            // Assert
            mediator.Verify();
            Assert.IsType<NotFoundObjectResult>(response);
            Assert.Equal("Just not found", ((NotFoundObjectResult)response).Value);
        }

        [Fact]
        public async Task DeleteAsync_WithExistingGuid_SendsCommandAndReturnsOk()
        {
            // Arrange
            var guid = Guid.NewGuid();
            var mediator = new Mock<IMediator>();
            mediator
                .Setup(m => m.Send(It.Is<DeleteDriverCommand>(q => q.DriverId == guid), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(Unit.Value))
                .Verifiable();
            var controller = new DriversController(mediator.Object);

            // Act
            var response = await controller.DeleteAsync(guid);

            // Assert
            mediator.Verify();
            Assert.IsType<OkResult>(response);
        }

        [Fact]
        public async Task DeleteAsync_WithNonExistingGuid_SendsCommandAndReturnsNotFound()
        {
            // Arrange
            var guid = Guid.NewGuid();
            var mediator = new Mock<IMediator>();
            mediator
                .Setup(m => m.Send(It.Is<DeleteDriverCommand>(q => q.DriverId == guid), It.IsAny<CancellationToken>()))
                .Returns(Task.FromException<Unit>(new NotFoundException("Just not found")))
                .Verifiable();
            var controller = new DriversController(mediator.Object);

            // Act
            var response = await controller.DeleteAsync(guid);

            // Assert
            mediator.Verify();
            Assert.IsType<NotFoundObjectResult>(response);
            Assert.Equal("Just not found", ((NotFoundObjectResult)response).Value);
        }
    }
}