using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using Xunit;
using MokSportsApp.Data.Repositories.Interfaces;
using MokSportsApp.Models;
using MokSportsApp.Services.Implementations;

namespace MokSportsApp.Tests
{
    public class UserLeagueServiceTests
    {
        private readonly Mock<IUserLeagueRepository> _mockUserLeagueRepository;
        private readonly UserLeagueService _service;

        public UserLeagueServiceTests()
        {
            _mockUserLeagueRepository = new Mock<IUserLeagueRepository>();
            _service = new UserLeagueService(_mockUserLeagueRepository.Object);
        }

        [Fact]
        public async Task GetUsersInLeagueAsync_ShouldReturnUsers_WhenLeagueHasUsers()
        {
            // Arrange
            var users = new List<User>
            {
                new User { UserId = 1, FirstName = "John", LastName = "Doe" },
                new User { UserId = 2, FirstName = "Jane", LastName = "Doe" }
            };
            _mockUserLeagueRepository.Setup(repo => repo.GetUsersByLeagueIdAsync(It.IsAny<int>()))
                .ReturnsAsync(users);

            // Act
            var result = await _service.GetUsersInLeagueAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Equal("John", result[0].FirstName);
            Assert.Equal("Jane", result[1].FirstName);
        }

        [Fact]
        public async Task GetUsersInLeagueAsync_ShouldReturnEmptyList_WhenNoUsersInLeague()
        {
            // Arrange
            _mockUserLeagueRepository.Setup(repo => repo.GetUsersByLeagueIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new List<User>());

            // Act
            var result = await _service.GetUsersInLeagueAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }
    }
}
