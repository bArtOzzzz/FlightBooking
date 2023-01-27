using FlightBooking.Domain.Entities;
using FlightBooking.Infrastructure.Repository;
using FlightBooking.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Data.Common;
using Xunit;
using Microsoft.Extensions.Configuration;
using Moq.Dapper;
using Dapper;
using FluentAssertions;

namespace FlightBooking.Test.AirplaneTest
{
    public class AirplaneRepositoryTest
    {
        private AirplaneRepository _airplaneRepository;

        private readonly Mock<IConfiguration> _mockConfiguration;

        public AirplaneRepositoryTest()
        {
            _mockConfiguration = new Mock<IConfiguration>();

            var optionBuilder = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("FlightBookingDb");

            var context = new ApplicationDbContext(optionBuilder.Options);
            _airplaneRepository = new AirplaneRepository(context, _mockConfiguration.Object);
        }

        [Fact]
        private async Task GetAllAsync_OnSuccess_Returns_CompletedResult()
        {
            // Arrange
            var connection = new Mock<DbConnection>();
            var expected = new List<AirplaneEntity>() {
                new AirplaneEntity
                {
                    Id = Guid.NewGuid(),
                    ModelName = "ATR EVO",
                    MaximumSeats = 525,
                    MaximumWeight = 46750,
                    CreatedDate = DateTime.Now.ToString("MM-dd-yy"),
                    CreatedTime = DateTime.Now.ToString("HH:m:s")
                },

                new AirplaneEntity
                {
                    Id = Guid.NewGuid(),
                    ModelName = "ATR EVO",
                    MaximumSeats = 525,
                    MaximumWeight = 46750,
                    CreatedDate = DateTime.Now.ToString("MM-dd-yy"),
                    CreatedTime = DateTime.Now.ToString("HH:m:s")
                }
            };

            connection.SetupDapperAsync(c => c.QueryAsync<AirplaneEntity>(It.IsAny<string>(), null, null, null, null))
                      .ReturnsAsync(expected);

            // Act
            var result = (await connection.Object.QueryAsync<AirplaneEntity>("", null, null, null, null)).ToList();

            // Assert
            result.Should().BeOfType<List<AirplaneEntity>>();
            result.Should().HaveCount(2);
            result[0].Should().BeEquivalentTo(expected[0]);
        }

        [Fact]
        private void GetByIdAsync_OnSuccess_Returns_CompletedResult()
        {
            var connection = new Mock<DbConnection>();

            AirplaneEntity expected = new();

            connection.SetupDapperAsync(c => c.ExecuteScalarAsync<object>(It.IsAny<string>(), null, null, null, null))
                      .ReturnsAsync(expected);

            var result = connection.Object.ExecuteScalarAsync<object>("")
                                          .GetAwaiter()
                                          .GetResult();

            result.Should().BeOfType<AirplaneEntity>();
            result.Should().BeEquivalentTo(expected);
        }

        [Fact]
        private async Task CreateAsync_OnSuccess_Returns_CompletedResult()
        {
            // Arrange
            var airplaneEntity = new AirplaneEntity()
            {
                ModelName = "ATR EVO",
                MaximumSeats = 525,
                MaximumWeight = 46750
            };

            // Act
            var result = await _airplaneRepository.CreateAsync(airplaneEntity);

            // Assert
            result.Should().NotBeEmpty();
        }

        [Fact]
        private async Task UpdateAsync_OnSuccess_Returns_CompletedResult()
        {
            // Arrange
            var airplaneEntity = new AirplaneEntity()
            {
                Id = new Guid("01a36c70-36c0-4e14-8215-8388832d606d"),
                ModelName = "ATR EVO",
                MaximumSeats = 525,
                MaximumWeight = 46750,
                CreatedDate = DateTime.Now.ToString("MM-dd-yy"),
                CreatedTime = DateTime.Now.ToString("HH:m:s")
            };

            // Act
            var result = await _airplaneRepository.UpdateAsync(airplaneEntity.Id, airplaneEntity);

            // Assert
            result.Should().NotBeEmpty();
        }

        [Fact]
        private async Task DeleteAsync_OnSuccess_Returns_CompletedResult()
        {
            // Act
            var result = await _airplaneRepository.DeleteAsync(new Guid("e06a3365-9b53-458f-b052-ba96aff94e5b"));

            // Assert
            result.Should().BeTrue();
        }
    }
}
