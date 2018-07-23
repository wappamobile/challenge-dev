using Challenge.Dev.Controllers;
using Challenge.Dev.Models;
using Challenge.Dev.Repositories;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Challenge.Dev.Tests
{
    public class UserTests
    {
        private readonly Mock<IUserRepository> _mockRepository;
        public UserTests()
        {
            _mockRepository = new Mock<IUserRepository>();
        }

        private ICollection<User> GetTestUserCollection()
        {
            return new List<User>()
            {
                new User() { FirstName = "Test User 1" },
                new User() { FirstName = "Test User 2" }
            };
        }

        [Fact]
        public void ReturnsAllUsers()
        {
            var sorting = new SortingParams();
            _mockRepository.Setup(r => r.GetUsers(sorting)).Returns(GetTestUserCollection());
            var controller = new UsersController(_mockRepository.Object);

            var result = controller.GetAll(sorting);

            Assert.IsType<List<User>>(result);
            Assert.Equal("Test User 1", result.First().FirstName);
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public void CrateUser()
        {
            _mockRepository.Setup(r => r.GetAll()).Returns(GetTestUserCollection());
            var controller = new UsersController(_mockRepository.Object);

            var result = controller.Create(new User()
            {
                FirstName = "New User",
                LastName = "Teste"
            });

            var res = Assert.IsType<CreatedAtRouteResult>(result);
            Assert.Equal(201, res.StatusCode.Value);
            var user = Assert.IsType<User>(res.Value);
            Assert.Equal("New User", user.FirstName); 
            Assert.Equal("Teste", user.LastName);
        }
    }
}
