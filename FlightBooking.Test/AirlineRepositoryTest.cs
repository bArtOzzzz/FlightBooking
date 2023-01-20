using Dapper;
using FlightBooking.Domain.Entities;
using FlightBooking.Infrastructure;
using FlightBooking.Infrastructure.Repository;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;
using Moq.Dapper;
using System.Data;
using Xunit;

namespace FlightBooking.Test
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

        /*[Fact]
        private void GetAllAsync_OnSuccess_Returns_CompletedResult()
        {
            // Arrange
            var connection = new Mock<IDbConnection>();

            List<AirlineEntity> expected = new List<AirlineEntity>();

            connection.SetupDapper(c => c.Query(It.IsAny<string>(), null, null, true, null, null)).Returns(expected);

            // Act
            var result = connection.Object.Query("", null, null, true, null, null).ToList();

            // Assert
            result.Should().BeAssignableTo<List<AirlineEntity>>();
            //result.Should().BeOfType<List<AirlineEntity>>();
            //result.Should().HaveCount(3);
        }*/

        /*[Fact]
        private void GetByIdAsync_OnSuccess_Returns_CompletedResult()
        {

        }*/

        [Fact]
        private async Task CreateAsync_OnSuccess_Returns_CompletedResult()
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
        private async Task UpdateAsync_OnSuccess_Returns_CompletedResult()
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
