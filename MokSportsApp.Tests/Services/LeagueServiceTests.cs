using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using Xunit;
using MokSportsApp.Data.Repositories.Interfaces;
using MokSportsApp.Models;
using MokSportsApp.Services.Implementations;

namespace MokSportsApp.Tests
{
    public class LeagueServiceTests
    {
        private readonly Mock<ILeagueRepository> _mockLeagueRepository;
        private readonly Mock<IUserLeagueRepository> _mockUserLeagueRepository;
        private readonly LeagueService _service;

        public LeagueServiceTests()
        {
            _mockLeagueRepository = new Mock<ILeagueRepository>();
            _mockUserLeagueRepository = new Mock<IUserLeagueRepository>();
            _service = new LeagueService(_mockLeagueRepository.Object, _mockUserLeagueRepository.Object);
        }

        [Fact]
        public async Task CreateLeagueAsync_ShouldThrowException_WhenLeagueExists()
        {
            // Arrange
            var league = new League { LeagueName = "Test League", Pin = "123456" };
            _mockLeagueRepository.Setup(repo => repo.GetByPinAndNameAsync(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(league);

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() =>
                _service.CreateLeagueAsync(new League { LeagueName = "Test League", Pin = "123456" }, 1));
        }

        [Fact]
        public async Task CreateLeagueAsync_ShouldCreateLeagueAndAddUser()
        {
            // Arrange
            var league = new League { LeagueName = "New League", Pin = "654321" };
            _mockLeagueRepository.Setup(repo => repo.GetByPinAndNameAsync(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync((League)null);

            // Act
            var createdLeague = await _service.CreateLeagueAsync(league, 1);

            // Assert
            Assert.NotNull(createdLeague);
            Assert.Equal("New League", createdLeague.LeagueName);
            Assert.Equal(1, createdLeague.CreatedBy);
            _mockLeagueRepository.Verify(repo => repo.AddAsync(It.IsAny<League>()), Times.Once);
            _mockUserLeagueRepository.Verify(repo => repo.AddAsync(It.IsAny<UserLeague>()), Times.Once);
            _mockLeagueRepository.Verify(repo => repo.SaveChangesAsync(), Times.Once);
            _mockUserLeagueRepository.Verify(repo => repo.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task JoinLeagueAsync_ShouldThrowException_WhenLeagueNotFound()
        {
            // Arrange
            _mockLeagueRepository.Setup(repo => repo.GetByPinAndNameAsync(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync((League)null);

            // Act & Assert
            await Assert.ThrowsAsync<KeyNotFoundException>(() =>
                _service.JoinLeagueAsync(1, "999999", "Nonexistent League"));
        }

        [Fact]
        public async Task JoinLeagueAsync_ShouldAddUserToLeague_WhenLeagueExists()
        {
            // Arrange
            var league = new League { LeagueId = 1, LeagueName = "Joinable League", Pin = "111111" };
            _mockLeagueRepository.Setup(repo => repo.GetByPinAndNameAsync(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(league);

            // Act
            await _service.JoinLeagueAsync(2, "111111", "Joinable League");

            // Assert
            _mockUserLeagueRepository.Verify(repo => repo.AddAsync(It.Is<UserLeague>(ul => ul.UserId == 2 && ul.LeagueId == league.LeagueId)), Times.Once);
            _mockUserLeagueRepository.Verify(repo => repo.SaveChangesAsync(), Times.Once);
        }
    }
}
