using Moq;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using MokSportsApp.Controllers;
using MokSportsApp.Services.Interfaces;
using MokSportsApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MokSportsApp.Tests.Controllers
{
    public class LeagueControllerTests
    {
        private readonly Mock<ILeagueService> _mockLeagueService;
        private readonly Mock<IUserLeagueService> _mockUserLeagueService;
        private readonly LeagueController _controller;

        public LeagueControllerTests()
        {
            _mockLeagueService = new Mock<ILeagueService>();
            _mockUserLeagueService = new Mock<IUserLeagueService>();
            _controller = new LeagueController(_mockLeagueService.Object, _mockUserLeagueService.Object);
        }

        [Fact]
        public async Task CreateLeague_ReturnsCreatedAtAction_WhenLeagueIsCreated()
        {
            // Arrange
            var league = new League { LeagueId = 1, LeagueName = "Test League", Pin = "123456" };
            _mockLeagueService.Setup(service => service.CreateLeagueAsync(It.IsAny<League>(), It.IsAny<int>()))
                .ReturnsAsync(league);

            // Act
            var result = await _controller.CreateLeague(league, 1);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            Assert.Equal("GetLeagueById", createdAtActionResult.ActionName);
            Assert.Equal(league.LeagueId, ((League)createdAtActionResult.Value).LeagueId);
        }

        [Fact]
        public async Task GetLeagueById_ReturnsOk_WhenLeagueExists()
        {
            // Arrange
            var league = new League { LeagueId = 1, LeagueName = "Test League" };
            _mockLeagueService.Setup(service => service.GetLeagueByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(league);

            // Act
            var result = await _controller.GetLeagueById(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(league, okResult.Value);
        }

        [Fact]
        public async Task GetLeagueById_ReturnsNotFound_WhenLeagueDoesNotExist()
        {
            // Arrange
            _mockLeagueService.Setup(service => service.GetLeagueByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((League)null);

            // Act
            var result = await _controller.GetLeagueById(1);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result.Result);
        }

        [Fact]
        public async Task JoinLeague_ReturnsOk_WhenUserJoinsLeagueSuccessfully()
        {
            // Arrange
            var request = new JoinLeagueRequest { LeagueName = "Test League", Pin = "123456" };
            _mockLeagueService.Setup(service => service.JoinLeagueAsync(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.JoinLeague(1, request);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetUsersInLeague_ReturnsOk_WhenUsersExistInLeague()
        {
            // Arrange
            var users = new List<User> {
                new User { UserId = 1, FirstName = "John", LastName = "Doe" },
                new User { UserId = 2, FirstName = "Jane", LastName = "Doe" }
            };
            _mockUserLeagueService.Setup(service => service.GetUsersInLeagueAsync(It.IsAny<int>()))
                .ReturnsAsync(users);

            // Act
            var result = await _controller.GetUsersInLeague(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedUsers = Assert.IsAssignableFrom<IEnumerable<User>>(okResult.Value);
            Assert.Equal(2, returnedUsers.Count());
        }
    }
}
