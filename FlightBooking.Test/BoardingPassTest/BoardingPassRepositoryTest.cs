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

namespace FlightBooking.Test.BoardingPassTest
{
    public class BoardingPassRepositoryTest
    {
        private BoardingPassRepository _boardingPassRepository;

        private readonly Mock<IConfiguration> _mockConfiguration;

        public BoardingPassRepositoryTest()
        {
            _mockConfiguration = new Mock<IConfiguration>();

            var optionBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase("FlightBookingDb");

            var context = new ApplicationDbContext(optionBuilder.Options);
            _boardingPassRepository = new BoardingPassRepository(context, _mockConfiguration.Object);
        }

        [Fact]
        private async Task GetAllAsync_OnSuccess_Returns_CompletedResult_With_RightType()
        {
            // Arrange
            var connection = new Mock<DbConnection>();
            var expected = new List<BoardingPassEntity>() {
                new BoardingPassEntity
                {
                    Id = Guid.NewGuid(),
                    FlightId = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    CreatedDate = new DateTime(2023, 5, 12, 12, 17, 03).ToString("MM-dd-yy"),
                    CreatedTime = new DateTime(2023, 5, 12, 12, 17, 03).ToString("HH:m:s"),
                    Prise = 778,
                    isExpired = false,
                    BookingExpireDate = new DateTime(2023, 5, 15),
                },

                new BoardingPassEntity
                {
                    Id = Guid.NewGuid(),
                    FlightId = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    CreatedDate = new DateTime(2023, 2, 6, 8, 43, 17).ToString("MM-dd-yy"),
                    CreatedTime = new DateTime(2023, 2, 6, 8, 43, 17).ToString("HH:m:s"),
                    Prise = 1661,
                    isExpired = false,
                    BookingExpireDate = new DateTime(2023, 2, 9)
                },
            };

            connection.SetupDapperAsync(c => c.QueryAsync<BoardingPassEntity>(It.IsAny<string>(), null, null, null, null))
                      .ReturnsAsync(expected);

            // Act
            var result = (await connection.Object.QueryAsync<BoardingPassEntity>("", null, null, null, null)).ToList();

            // Assert
            result.Should().BeOfType<List<BoardingPassEntity>>();
            result.Should().BeEquivalentTo(expected);
        }

        [Fact]
        private void GetByIdAsync_OnSuccess_Returns_CompletedResult_With_RightType()
        {
            var connection = new Mock<DbConnection>();

            BoardingPassEntity expected = new BoardingPassEntity();

            connection.SetupDapperAsync(c => c.ExecuteScalarAsync<object>(It.IsAny<string>(), null, null, null, null))
                      .ReturnsAsync(expected);

            var result = connection.Object.ExecuteScalarAsync<object>("")
                                          .GetAwaiter()
                                          .GetResult();

            result.Should().BeOfType<BoardingPassEntity>();
            result.Should().BeEquivalentTo(expected);
        }

        [Fact]
        private async Task CreateAsync_OnSuccess_Returns_CompletedResult_NotEmptyResult()
        {
            // Arrange
            var airlineEntity = new BoardingPassEntity()
            {
                Id = Guid.NewGuid(),
                FlightId = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                CreatedDate = new DateTime(2023, 5, 12, 12, 17, 03).ToString("MM-dd-yy"),
                CreatedTime = new DateTime(2023, 5, 12, 12, 17, 03).ToString("HH:m:s"),
                Prise = 778,
                isExpired = false,
                BookingExpireDate = new DateTime(2023, 5, 15),
            };

            // Act
            var result = await _boardingPassRepository.CreateAsync(airlineEntity);

            // Assert
            result.Should().NotBeEmpty();
        }

        [Fact]
        private async Task UpdateAsync_OnSuccess_Returns_CompletedResult_NotEmptyResult()
        {
            // Arrange
            var airlineEntity = new BoardingPassEntity()
            {
                Id = new Guid("65b5e2db-f405-4d62-ae1d-ce57f7590fc6"),
                FlightId = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                CreatedDate = new DateTime(2023, 5, 12, 12, 17, 03).ToString("MM-dd-yy"),
                CreatedTime = new DateTime(2023, 5, 12, 12, 17, 03).ToString("HH:m:s"),
                Prise = 778,
                isExpired = false,
                BookingExpireDate = new DateTime(2023, 5, 15),
            };

            // Act
            var result = await _boardingPassRepository.UpdateAsync(airlineEntity.Id, airlineEntity);

            // Assert
            result.Should().NotBeEmpty();
        }

        [Fact]
        private async Task DeleteAsync_OnSuccess_Returns_CompletedResult()
        {
            // Arrange
            var airlineEntity = new BoardingPassEntity()
            {
                Id = new Guid("0fdd06d1-075c-45f8-8d96-cab92be56031"),
                FlightId = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                CreatedDate = new DateTime(2023, 5, 12, 12, 17, 03).ToString("MM-dd-yy"),
                CreatedTime = new DateTime(2023, 5, 12, 12, 17, 03).ToString("HH:m:s"),
                Prise = 778,
                isExpired = false,
                BookingExpireDate = new DateTime(2023, 5, 15),
            };

            // Act
            var result = await _boardingPassRepository.DeleteAsync(airlineEntity.Id);

            // Assert
            result.Should().BeTrue();
        }
    }
}
