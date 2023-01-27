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

namespace FlightBooking.Test.FlightTest
{
    public class FlightRepositoryTest
    {
        private FlightRepository _flightRepository;

        private readonly Mock<IConfiguration> _mockConfiguration;

        public FlightRepositoryTest()
        {
            _mockConfiguration = new Mock<IConfiguration>();

            var optionBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase("FlightBookingDb");

            var context = new ApplicationDbContext(optionBuilder.Options);
            _flightRepository = new FlightRepository(context, _mockConfiguration.Object);
        }

        [Fact]
        private async Task GetAllAsync_OnSuccess_Returns_CompletedResult()
        {
            // Arrange
            var connection = new Mock<DbConnection>();
            var expected = new List<FlightEntity>() {
                new FlightEntity
                {
                    Id = Guid.NewGuid(),
                    CreatedDate = DateTime.Now.ToString("MM-dd-yy"),
                    CreatedTime = DateTime.Now.ToString("HH:m:s")
                },

                new FlightEntity
                {
                    Id = Guid.NewGuid(),
                    CreatedDate = DateTime.Now.ToString("MM-dd-yy"),
                    CreatedTime = DateTime.Now.ToString("HH:m:s")
                },
            };

            connection.SetupDapperAsync(c => c.QueryAsync<FlightEntity>(It.IsAny<string>(), null, null, null, null))
                      .ReturnsAsync(expected);

            // Act
            var result = (await connection.Object.QueryAsync<FlightEntity>("", null, null, null, null)).ToList();

            // Assert
            result.Should().BeOfType<List<FlightEntity>>();
            result.Should().HaveCount(2);
            result[0].Should().BeEquivalentTo(expected[0]);
        }

        [Fact]
        private void GetByIdAsync_OnSuccess_Returns_CompletedResult()
        {
            var connection = new Mock<DbConnection>();

            FlightEntity expected = new FlightEntity();

            connection.SetupDapperAsync(c => c.ExecuteScalarAsync<object>(It.IsAny<string>(), null, null, null, null))
                      .ReturnsAsync(expected);

            var result = connection.Object.ExecuteScalarAsync<object>("")
                                          .GetAwaiter()
                                          .GetResult();

            result.Should().BeOfType<FlightEntity>();
            result.Should().BeEquivalentTo(expected);
        }

        [Fact]
        private async Task CreateAsync_OnSuccess_Returns_CompletedResult()
        {
            // Arrange
            var flightEntity = new FlightEntity()
            {
                Id = Guid.NewGuid(),
                CreatedDate = DateTime.Now.ToString("MM-dd-yy"),
                CreatedTime = DateTime.Now.ToString("HH:m:s")
            };

            // Act
            var result = await _flightRepository.CreateAsync(flightEntity);

            // Assert
            result.Should().NotBeEmpty();
        }

        [Fact]
        private async Task UpdateAsync_OnSuccess_Returns_CompletedResult()
        {
            // Arrange
            var flightEntity = new FlightEntity()
            {
                Id = new Guid("466c45a1-64e2-47a8-a71a-821220ce0a33"),
                CreatedDate = DateTime.Now.ToString("MM-dd-yy"),
                CreatedTime = DateTime.Now.ToString("HH:m:s")
            };

            // Act
            var result = await _flightRepository.UpdateAsync(flightEntity.Id, flightEntity);

            // Assert
            result.Should().NotBeEmpty();
        }

        [Fact]
        private async Task DeleteAsync_OnSuccess_Returns_CompletedResult()
        {
            // Act
            var result = await _flightRepository.DeleteAsync(new Guid("1506eaca-34cc-4ed1-9060-4b6075139e42"));

            // Assert
            result.Should().BeTrue();
        }
    }
}
