using FlightBooking.Infrastructure.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using FlightBooking.Domain.Entities;
using FlightBooking.Infrastructure;
using System.Data.Common;
using FluentAssertions;
using Moq.Dapper;
using Dapper;
using Xunit;
using Moq;

namespace FlightBooking.Test.AirlineTests
{
    public class AirlineRepositoryTest
    {
        private AirlineRepository _airlineRepository;

        private readonly Mock<IConfiguration> _mockConfiguration;

        public AirlineRepositoryTest()
        {
            _mockConfiguration = new Mock<IConfiguration>();

            var optionBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase("FlightBookingDb");

            var context = new ApplicationDbContext(optionBuilder.Options);
            _airlineRepository = new AirlineRepository(context, _mockConfiguration.Object);
        }

        [Fact]
        private async Task GetAllAsync_OnSuccess_Returns_CompletedResult_With_RightType()
        {
            // Arrange
            var connection = new Mock<DbConnection>();
            var expected = new List<AirlineEntity>() {
                new AirlineEntity
                {
                    Id = Guid.NewGuid(),
                    AirlineName = "Belavia",
                    Rating = 4.3,
                    CreatedDate = DateTime.Now.ToString("MM-dd-yy"),
                    CreatedTime = DateTime.Now.ToString("HH:m:s")
                },

                new AirlineEntity
                {
                    Id = Guid.NewGuid(),
                    AirlineName = "American Airlines",
                    Rating = 4.7,
                    CreatedDate = DateTime.Now.ToString("MM-dd-yy"),
                    CreatedTime = DateTime.Now.ToString("HH:m:s")
                },
            };

            connection.SetupDapperAsync(c => c.QueryAsync<AirlineEntity>(It.IsAny<string>(), null, null, null, null))
                      .ReturnsAsync(expected);

            // Act
            var result = (await connection.Object.QueryAsync<AirlineEntity>("", null, null, null, null)).ToList();

            // Assert
            result.Should().BeOfType<List<AirlineEntity>>();
            result.Should().BeEquivalentTo(expected);
        }

        [Fact]
        private void GetByIdAsync_OnSuccess_Returns_CompletedResult_With_RightType()
        {
            var connection = new Mock<DbConnection>();

            AirlineEntity expected = new AirlineEntity();

            connection.SetupDapperAsync(c => c.ExecuteScalarAsync<object>(It.IsAny<string>(), null, null, null, null))
                      .ReturnsAsync(expected);

            var result = connection.Object.ExecuteScalarAsync<object>("")
                                          .GetAwaiter()
                                          .GetResult();

            result.Should().BeOfType<AirlineEntity>();
            result.Should().BeEquivalentTo(expected);
        }

        [Fact]
        private async Task CreateAsync_OnSuccess_Returns_CompletedResult_NotEmptyResult()
        {
            // Arrange
            var airlineEntity = new AirlineEntity()
            {
                AirlineName = "Belavia",
                Rating = 4.3
            };

            // Act
            var result = await _airlineRepository.CreateAsync(airlineEntity);

            // Assert
            result.Should().NotBeEmpty();
        }

        [Fact]
        private async Task UpdateAsync_OnSuccess_Returns_CompletedResult_NotEmptyResult()
        {
            // Arrange
            var airlineEntity = new AirlineEntity()
            {
                Id = new Guid("b01e92b9-2c8b-4437-b290-eae73e1017f0"),
                AirlineName = "Belavia",
                Rating = 4.3,
                CreatedDate = DateTime.Now.ToString("MM-dd-yy"),
                CreatedTime = DateTime.Now.ToString("HH:m:s")
            };

            // Act
            var result = await _airlineRepository.UpdateAsync(airlineEntity.Id, airlineEntity);

            // Assert
            result.Should().NotBeEmpty();
        }

        [Fact]
        private async Task DeleteAsync_OnSuccess_Returns_CompletedResult()
        {
            // Arrange
            var airlineEntity = new AirlineEntity()
            {
                Id = new Guid("96e1738a-568d-4a23-be7d-f2f3382f1cf9"),
                AirlineName = "Belavia",
                Rating = 4.3,
                CreatedDate = DateTime.Now.ToString("MM-dd-yy"),
                CreatedTime = DateTime.Now.ToString("HH:m:s")
            };

            // Act
            var result = await _airlineRepository.DeleteAsync(airlineEntity.Id);

            // Assert
            result.Should().BeTrue();
        }
    }
}
